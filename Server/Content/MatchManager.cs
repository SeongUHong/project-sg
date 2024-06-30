using ServerCore;
using System;
using System.Collections.Generic;

namespace Server
{
    class MatchManager : IJobQueue
    {
        static MatchManager _instance = new MatchManager();
        public static MatchManager Instance { get { return _instance; } }

        // 대기중인 플레이어들
        Dictionary<ushort, ClientSession> _waitingSessions = new Dictionary<ushort, ClientSession>();
        // 배틀중인 플레이어들
        Dictionary<ushort, ClientSession> _matchedSessions = new Dictionary<ushort, ClientSession>();
        // 배틀룸
        Dictionary<ushort, BattleRoom> _battleRooms = new Dictionary<ushort, BattleRoom>();
        Queue<ushort> _waitingQueue = new Queue<ushort>();
        JobQueue _jobQueue = new JobQueue();
        ushort _battleRoomId = 0;
        
        object _lock = new object();

        const int MATCH_INTERVAL = 2000;
        const int BATTLE_PLAYER_NUM = Config.BATTLE_PLAYER_NUM;

        public MatchManager()
        {
            Push(() => { MatchTimer(); });
        }

        // 작업을 대기시킴
        public void Push(Action job)
        {
            _jobQueue.Push(job);
        }

        // 잡타이머에 일정 간격으로 등록
        void MatchTimer()
        {
            Push(() => { Match(); });
            JobTimer.Instance.Push(MatchTimer, MATCH_INTERVAL);
        }

        public void Match()
        {
            while (true)
            {
                List<ClientSession> sessions = PopMatchPlayers();
                if (sessions == null)
                    return;

                // 배틀룸 생성
                BattleRoom battle = CreateBattleRoom();
                // 배틀 초기화
                battle.Init(sessions);

                ClientSession session1 = sessions[0];
                ClientSession session2 = sessions[1];

                // 매칭 완료 패킷 전송
                // 매칭된 플레이어가 서로의 ID를 가짐
                session1.Player.EnemyPlayerId = session2.Player.PlayerId;
                S_Matched matched = new S_Matched();
                matched.enemyNickname = session2.Player.Nickname;
                matched.isLeft = true;
                session1.Send(matched.Write());
                ChangeWaitToBattle(session1);

                session2.Player.EnemyPlayerId = session1.Player.PlayerId;
                matched = new S_Matched();
                matched.enemyNickname = session1.Player.Nickname;
                matched.isLeft = false;
                session2.Send(matched.Write());
                ChangeWaitToBattle(session2);
            }
        }

        // 매칭 가능한 플레이어 그룹을 반환
        List<ClientSession> PopMatchPlayers()
        {
            lock (_lock)
            {
                if (_waitingQueue.Count < BATTLE_PLAYER_NUM)
                    return null;

                List<ClientSession> sessions = new List<ClientSession>();
                for (int i = 0; i < BATTLE_PLAYER_NUM; i++)
                {
                    // 대기중인 플레이어ID
                    ushort playerId = _waitingQueue.Dequeue();
                    ClientSession session = null;
                    // 세션 취득
                    if (!_waitingSessions.TryGetValue(playerId, out session))
                        return null;

                    sessions.Add(session);
                }

                return sessions;
            }
        }

        // 매칭 대기 플레이어 추가
        public void AddNewPlayer(ClientSession session, C_StartMatch startMatch)
        {
            lock (_lock)
            {
                // 이미 매칭중인 플레이어는 제외
                if (_waitingSessions.ContainsKey(session.SessionId))
                    return;
                if (_matchedSessions.ContainsKey(session.SessionId))
                    return;

                ushort sessionId = session.SessionId;

                // 플레이어 정보 작성
                Player player = new Player();
                player.PlayerId = sessionId;
                player.Nickname = startMatch.nickname;
                session.Player = player;

                // 대기 목록에 추가
                _waitingSessions.Add(sessionId, session);
                _waitingQueue.Enqueue(sessionId);
            }
        }

        // 대기목록에서 유저 삭제
        public void RemoveWaitPlayer(ClientSession session)
        {
            lock (_lock)
            {
                // 대기 목록에 없다면 세션 종료
                if (!_waitingSessions.ContainsKey(session.SessionId))
                {
                    Console.WriteLine($"Failed removing waitting player (playerId : {session.SessionId})");
                    session.Disconnect();
                    return;
                }

                _waitingSessions.Remove(session.SessionId);
                Console.WriteLine($"waitting player removed (playerId : {session.SessionId})");
            }
        }

        public bool IsWaittingPlayer(ClientSession session)
        {
            if (_waitingSessions.ContainsKey(session.SessionId))
                return true;

            return false;
        }

        // 배틀 중인 유저 목록에 유저 추가
        void AddBattlePlayer(ClientSession session)
        {
            lock (_lock)
            {
                _matchedSessions.Add(session.SessionId, session);
            }
        }

        // 배틀 중인 유저 목록에서 유저 삭제
        public void RemoveBattlePlayer(ClientSession session)
        {
            lock (_lock)
            {
                if (!_matchedSessions.ContainsKey(session.SessionId))
                {
                    Console.WriteLine($"Failed removing battle player (playerId : {session.SessionId})");
                    return;
                }

                _matchedSessions.Remove(session.SessionId);
                Console.WriteLine($"battle player removed (playerId : {session.SessionId})");
            }
        }

        // 유저를 대기 목록에서 배틀 목록으로 옮김
        void ChangeWaitToBattle(ClientSession session)
        {
            RemoveWaitPlayer(session);
            AddBattlePlayer(session);
        }

        // 배틀룸 생성
        BattleRoom CreateBattleRoom()
        {
            BattleRoom battleRoom = null;

            lock (_lock)
            {
                ushort battleRoomId = _battleRoomId++;

                battleRoom = new BattleRoom();
                battleRoom.BattleRoomId = battleRoomId;

                _battleRooms.Add(battleRoomId, battleRoom);
            }

            return battleRoom;
        }

        // 배틀룸 삭제
        public void RemoveBattleRoom(BattleRoom battleRoom)
        {
            foreach (ClientSession session in battleRoom.GetSessions())
                RemoveBattlePlayer(session);

            lock (_lock)
            {
                if (!_battleRooms.ContainsKey(battleRoom.BattleRoomId))
                {
                    Console.WriteLine($"Failed removing battle room (BattleRoomId : {battleRoom.BattleRoomId})");
                    return;
                }

                _battleRooms.Remove(battleRoom.BattleRoomId);
                Console.WriteLine($"battle room removed (BattleRoomId : {battleRoom.BattleRoomId})");
            }
        }
    }
}
