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
        Queue<ushort> _waitingQueue = new Queue<ushort>();
        JobQueue _jobQueue = new JobQueue();
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

                ClientSession session1 = sessions[0];
                ClientSession session2 = sessions[1];
                // 매칭된 플레이어가 서로의 ID를 가짐
                session1.Player.EnemyPlayerId = session2.Player.PlayerId;
                session2.Player.EnemyPlayerId = session1.Player.PlayerId;

                // 매칭 완료 패킷 전송
                foreach (ClientSession session in sessions)
                {
                    S_Matched matched = new S_Matched();
                    matched.enemyNickname = session.Player.Nickname;
                    session.Send(matched.Write());
                }

                // 배틀룸 생성
                BattleRoom battle = new BattleRoom();
                // 배틀 초기화
                battle.Init(sessions);
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
        // 대기 목록에서 삭제하면 배틀중인 플레이어ID로도 매칭을 진행할 수 있게 됨
        // 때문에 배틀 종료 후 대기목록에서 지울 것
        public void RemoveWaitPlayer(ClientSession session)
        {
            lock (_lock)
            {
                // 대기 목록에 없다면 패스
                if (!_waitingSessions.ContainsKey(session.SessionId))
                    return;

                _waitingSessions.Remove(session.SessionId);
            }
        }
    }
}
