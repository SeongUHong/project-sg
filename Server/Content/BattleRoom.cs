using ServerCore;
using System;
using System.Collections.Generic;

namespace Server
{
    class BattleRoom : IJobQueue
    {
        Dictionary<ushort, ClientSession> _sessions = new Dictionary<ushort, ClientSession>();
        JobQueue _jobQueue = new JobQueue();
        List<ArraySegment<byte>> _pendingList = new List<ArraySegment<byte>>();
        object _lock = new object();

        public void Init(List<ClientSession> sessions)
        {
            // 플레이어 등록
            foreach (ClientSession session in sessions)
                _sessions.Add(session.Player.PlayerId, session);
            // 타이머 시작
            Push(() => { FlushTimer(); });

            Console.WriteLine($"Matching completed. (playerId : {sessions[0].Player.PlayerId}, with playerId : {sessions[1].Player.PlayerId})");
        }

        // 작업을 대기시킴
        public void Push(Action job)
        {
            _jobQueue.Push(job);
        }

        // Flush()를 일정 간격으로 등록
        void FlushTimer()
        {
            Push(() => { Flush(); });
            JobTimer.Instance.Push(FlushTimer, Config.FLUSH_BATTLE_JOB_INTERVAL);
        }

        // 대기중인 작업들을 진행시킴
        public void Flush()
        {
            foreach (ClientSession s in _sessions.Values)
                s.Send(_pendingList);

            _pendingList.Clear();
        }

        // 정보 전송
        public void Broadcast(ArraySegment<byte> segment)
        {
            _pendingList.Add(segment);
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

        // 플레이어 입장
        //public void Enter(ClientSession session)
        //{
        //    // 플레이어 추가
        //    _sessions.Add(session);
        //    // 세션에 룸 바인드
        //    session.Room = this;

        //    // 입장한 플레이어에게 전체 플레이어 정보 전송
        //    S_PlayerList players = new S_PlayerList();
        //    foreach (ClientSession s in _sessions)
        //    {
        //        players.players.Add(new S_PlayerList.Player()
        //        {
        //            isSelf = (s == session),
        //            playerId = s.SessionId,
        //            posX = s.PosX,
        //            posY = s.PosY,
        //            posZ = s.PosZ,
        //        });
        //    }
        //    session.Send(players.Write());

        //    // 입장한 플레이어에 대한 정보를 전체 전송
        //    S_BroadcastEnterGame enter = new S_BroadcastEnterGame();
        //    enter.playerId = session.SessionId;
        //    enter.posX = 0;
        //    enter.posY = 0;
        //    enter.posZ = 0;
        //    Broadcast(enter.Write());
        //}
    }
}
