using ServerCore;
using System;
using System.Collections.Generic;

namespace Server
{
    class BattleRoom : IJobQueue
    {
        Dictionary<ushort, ClientSession> _sessions = new Dictionary<ushort, ClientSession>();
        Dictionary<int, Fireball> _fireballs = new Dictionary<int, Fireball>();
        JobQueue _jobQueue = new JobQueue();
        List<ArraySegment<byte>> _pendingList = new List<ArraySegment<byte>>();
        ushort _fireballId = 0;
        int _time = Config.GAME_TIME_LIMIT;
        bool _isInBattle = false;
        object _lock = new object();

        public ushort BattleRoomId { get; set; }
        public bool IsInBattle { get { return _isInBattle; } }

        public void Init(List<ClientSession> sessions)
        {
            // 플레이어 등록
            foreach (ClientSession session in sessions)
            {
                _sessions.Add(session.Player.PlayerId, session);
                session.BattleRoom = this;
            }

            Console.WriteLine($"Matching completed. (playerId : {sessions[0].Player.PlayerId}, with playerId : {sessions[1].Player.PlayerId})");
        }

        // 작업을 대기시킴
        public void Push(Action job)
        {
            _jobQueue.Push(job);
        }

        // 잡을 일정 간격으로 등록
        void BindJobTimer(Action job, int interval)
        {
            if (!_isInBattle)
                return;

            Push(job);
            JobTimer.Instance.Push(() => { BindJobTimer(job, interval); }, interval);
        }

        // 잡을 일정 시간 뒤에 실행
        void ExecAfterDelay(Action job, int time)
        {
            JobTimer.Instance.Push(job, time);

        }

        // 게임 제한시간 타이머
        void RunTimer()
        {
            if (!_isInBattle)
                return;

            int time = 0;

            lock (_lock)
            {
                _time -= 1;
                time = _time;
            }

            S_CountTime countTime = new S_CountTime()
            {
                remainSec = time
            };

            Broadcast(countTime.Write());

            if (time > 0) 
                return;

            // 시간 초과
            Timeover();
        }


        // 대기중인 작업들을 진행시킴
        public void Flush()
        {
            foreach (ClientSession s in _sessions.Values)
                s.Send(_pendingList);

            lock (_lock) {
                _pendingList.Clear();
            }
        }

        // 정보 전송
        public void Broadcast(ArraySegment<byte> segment)
        {
            lock (_lock)
            {
                _pendingList.Add(segment);
            }
        }

        // 반복적으로 실행하는 잡들 시작
        public void StartJob()
        {
            // 작업 등록
            BindJobTimer(Flush, Config.FLUSH_BATTLE_JOB_INTERVAL);
            BindJobTimer(DeleteFireball, Config.REMOVE_FIREBALL_JOB_INTERVAL);

            // 타이머 시작
            BindJobTimer(RunTimer, Config.GAME_TIME_COUNT_INTERVAL);
        }

        // 배틀 준비 완료 처리
        public void ReadyBattle(ClientSession session)
        {
            lock (_lock)
            {
                if (session.Player.IsReady)
                    return;

                session.Player.IsReady = true;

                // 전체 준비가 되었는지 확인
                foreach (ClientSession s in _sessions.Values)
                {
                    if (!s.Player.IsReady)
                        return;
                }

                // 게임 시작이 가능하면 패킷 전송
                Broadcast(new S_BroadcastGameStart().Write());

                // 게임 시작 플래그 온
                _isInBattle = true;
            }

            StartJob();
        }

        public List<ClientSession> GetSessions()
        {
            List<ClientSession> sessions = new List<ClientSession>();

            foreach (ClientSession s in _sessions.Values)
                sessions.Add(s);

            return sessions;
        }

        public ClientSession GetAnotherSession(ushort playerId)
        {
            ClientSession anotherSession = null;
            if (!_sessions.TryGetValue(playerId, out anotherSession))
            {
                Console.WriteLine($"[ERROR]Cant find session (playerId : {playerId})");
            }

            return anotherSession;
        }

        public Fireball GetFireBall(int fireballId)
        {
            Fireball fireball = null;
            if (!_fireballs.TryGetValue(fireballId, out fireball))
            {
                Console.WriteLine($"[WARN]Cant find fireball (fireballId : {fireballId})");
            }

            return fireball;
        }

        // 플레이어의 이동을 상대에게 알림
        public void HandleMove(ClientSession session, C_Move move)
        {
            ClientSession anotherSession = GetAnotherSession(session.Player.EnemyPlayerId);
            if (anotherSession == null)
                return;

            S_EnemyMove enemyMove = new S_EnemyMove()
            {
                posX = move.posX,
                posY = move.posY,
                angle = move.angle,
            };
            anotherSession.Send(enemyMove.Write());
        }

        // 미사일을 생성함
        public void CreateFireball(ClientSession session, C_Shot shot)
        {
            ClientSession anotherSession = GetAnotherSession(session.Player.EnemyPlayerId);
            if (anotherSession == null)
                return;

            ushort playerId = session.Player.PlayerId;
            ushort fireballId;

            // 미사일 생성
            lock (_lock)
            {
                fireballId = _fireballId++;

                Fireball fireBall = new Fireball();
                fireBall.FireballId = fireballId;
                fireBall.PlayerId = playerId;
                fireBall.PosX = shot.posX;
                fireBall.PosY = shot.posY;
                fireBall.RotZ = shot.angle;

                _fireballs.Add(fireballId, fireBall);
            }

            // 새로운 미사일 생성을 알림
            S_Shot newShot = new S_Shot()
            {
                fireballId = fireballId,
                posX = shot.posX,
                posY = shot.posY,
                angle = shot.angle,
            };
            session.Send(newShot.Write());

            // 새로운 미사일 생성을 적에게 알림
            S_EnemyShot newEnemyShot = new S_EnemyShot()
            {
                fireballId = fireballId,
                posX = shot.posX,
                posY = shot.posY,
                angle = shot.angle,
            };
            anotherSession.Send(newEnemyShot.Write());

            Console.WriteLine($"shoot (fireballId : {fireballId}, posX : {newShot.posX}, posY : {newShot.posY}, rotZ : {newShot.angle})");
        }

        // 미사일 삭제 처리
        public void DeleteFireball()
        {
            lock (_lock)
            {
                foreach (Fireball fire in _fireballs.Values)
                {
                    if (fire.CanRemove(_time))
                    {
                        _fireballs.Remove(fire.FireballId);
                        Console.WriteLine($"Removed fireball (fireballId : {fire.FireballId})");
                    }
                }
            }
        }

        // 피격을 알림
        public void HandleHit(ClientSession session, C_Hit hit)
        {
            lock (_lock) {
                ClientSession anotherSession = GetAnotherSession(session.Player.EnemyPlayerId);
                if (anotherSession == null)
                    return;

                Fireball fireball = GetFireBall(hit.fireballId);
                if (fireball == null)
                    return;

                // 미사일 삭제
                _fireballs.Remove(hit.fireballId);

                // 피격 횟수 증가
                session.Player.HitCount++;

                // 본인과 상대에게 피격 사실을 알림
                session.Send(new S_Hit()
                {
                    fireballId = hit.fireballId,
                }.Write());

                anotherSession.Send(new S_EnemyHit()
                {
                    fireballId = hit.fireballId,
                }.Write());

                Console.WriteLine($"Hit (playerId : {session.SessionId}, fireballId : {hit.fireballId})");
            }
        }

        // 플레이어 기체 파괴 처리
        public void DestroyPlayer(ClientSession session)
        {
            lock (_lock)
            {
                if (!_isInBattle)
                    return;

                ClientSession anotherSession = GetAnotherSession(session.Player.EnemyPlayerId);
                if (anotherSession == null)
                    return;

                // 승리
                anotherSession.Send(new S_Gameover()
                {
                    status = (int)Config.GAMEOVER_STATUS.WIN
                }.Write());

                // 패배
                session.Send(new S_Gameover()
                {
                    status = (int)Config.GAMEOVER_STATUS.LOSE
                }.Write());

                EndBattle();

                Console.WriteLine($"Player destroyed (playerId : {session.SessionId})");
            }

            ExecAfterDelay(Clear, Config.DISCONNECT_SESSION_DELAY);
        }

        // 시간 초과로 인한 드로우 처리
        public void Timeover()
        {
            // 시간 초과로 Flush가 동작하지 않으므로 Broadcast 사용 불가
            foreach (ClientSession session in _sessions.Values)
            {
                session.Send(new S_Gameover()
                {
                    status = (int)Config.GAMEOVER_STATUS.DROW
                }.Write());
            }

            EndBattle();

            Console.WriteLine($"Time over");

            ExecAfterDelay(Clear, Config.DISCONNECT_SESSION_DELAY);
        }

        // 플레이어의 비정상적 종료
        public void Giveup(ClientSession session)
        {
            lock (_lock)
            {
                if (!_isInBattle)
                    return;

                ClientSession anotherSession = GetAnotherSession(session.Player.EnemyPlayerId);
                if (anotherSession == null)
                    return;

                // 승리 패킷 전송
                // 이미 세션이 종료됐기 때문에 패배 패킷은 보내지 않음
                anotherSession.Send(new S_Gameover()
                {
                    status = (int)Config.GAMEOVER_STATUS.WIN
                }.Write());

                EndBattle();
            }

            Console.WriteLine($"Palyer Gave up (playerId : {session.SessionId})");

            ExecAfterDelay(Clear, Config.DISCONNECT_SESSION_DELAY);
        }

        // 배틀 종료 처리
        public void EndBattle()
        {
            lock (_lock)
            {
                _isInBattle = false;
            }
        }

        // 세션을 종료 시킴
        public void DisconnetSessions()
        {
            lock (_lock)
            {
                foreach (ClientSession s in _sessions.Values)
                {
                    s.Clear();
                    s.Disconnect();
                }
            }
        }

        // 참조 정리
        // Gameover패킷을 보내면 클라이언트에서 세션을 종료해버리기 때문에
        // 이 메서드를 실행하는 시점에는 이미 세션이 종료되어 있을 수 있음
        public void Clear()
        {
            // 배틀룸 참조 해제
            MatchManager.Instance.RemoveBattleRoom(this);

            // 각 세션 참조 해제 후 종료
            DisconnetSessions();

            // 배틀룸 내부 참조 해제
            lock (_lock)
            {
                _sessions.Clear();
                _fireballs.Clear();
                _pendingList.Clear();
            }
        }
    }
}
