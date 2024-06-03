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
        object _lock = new object();

        public void Init(List<ClientSession> sessions)
        {
            // 플레이어 등록
            foreach (ClientSession session in sessions)
            {
                _sessions.Add(session.Player.PlayerId, session);
                session.BattleRoom = this;
            }

            // 타이머 시작
            BindJobTimer(Flush, Config.FLUSH_BATTLE_JOB_INTERVAL);
            // 미사일 이동 시작
            BindJobTimer(BroadcastMoveFireball, Config.MOVE_FIREBALL_JOB_INTERVAL);

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
            Push(job);
            JobTimer.Instance.Push(() => { BindJobTimer(job, interval); }, interval);
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
            }
        }

        public ClientSession GetAnotherSession(ushort anotherPlayerId)
        {
            ClientSession anotherSession = null;
            if (!_sessions.TryGetValue(anotherPlayerId, out anotherSession))
            {
                Console.WriteLine($"Cant find session (playerId : {anotherPlayerId})");
            }

            return anotherSession;
        }

        public Fireball GetFireBall(int fireballId)
        {
            Fireball fireball = null;
            if (!_fireballs.TryGetValue(fireballId, out fireball))
            {
                Console.WriteLine($"Cant find fireball (fireballId : {fireballId})");
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
                rotZ = move.rotZ,
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
                fireBall.RotZ = shot.rotZ;

                _fireballs.Add(fireballId, fireBall);
            }

            // 새로운 미사일 생성을 알림
            S_Shot newShot = new S_Shot()
            {
                fireballId = fireballId,
                posX = shot.posX,
                posY = shot.posY,
                rotZ = shot.rotZ,
            };
            session.Send(newShot.Write());

            // 새로운 미사일 생성을 적에게 알림
            S_EnemyShot newEnemyShot = new S_EnemyShot()
            {
                fireballId = fireballId,
                posX = shot.posX,
                posY = shot.posY,
                rotZ = shot.rotZ,
            };
            anotherSession.Send(newEnemyShot.Write());

            Console.WriteLine($"shoot (fireballId : {fireballId}, posX : {newShot.posX}, posY : {newShot.posY}, rotZ : {newShot.rotZ})");
        }

        // 미사일 이동 처리
        public void MoveFireball()
        {
            lock (_lock)
            {
                foreach (Fireball fire in _fireballs.Values)
                {
                    fire.Move();
                }
            }
        }

        // 미사일 이동 패킷 전송
        public void BroadcastMoveFireball()
        {
            // 미사일을 이동 시킴
            MoveFireball();

            // 이동 정보 전체 전송
            foreach (Fireball fire in _fireballs.Values)
            {
                S_FireballMove fireMove = new S_FireballMove()
                {
                    fireballId = fire.FireballId,
                    posX = fire.PosX,
                    posY = fire.PosY,
                    rotZ = fire.RotZ,
                };
                Broadcast(fireMove.Write());
            }
        }

        // 피격을 알림
        public void HandleHit(ClientSession session, C_Hit hit)
        {
            ClientSession anotherSession = GetAnotherSession(session.Player.EnemyPlayerId);
            if (anotherSession == null)
                return;

            Fireball fireball = GetFireBall(hit.fireballId);
            if (fireball == null)
                return;
        }
    }
}
