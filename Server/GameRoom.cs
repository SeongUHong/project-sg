using ServerCore;
using System;
using System.Collections.Generic;

namespace Server
{
    class GameRoom : IJobQueue
    {
        List<ClientSession> _sessions = new List<ClientSession>();
        JobQueue _jobQueue = new JobQueue();
        List<ArraySegment<byte>> _pendingList = new List<ArraySegment<byte>>();

        public void Push(Action job)
        {
            _jobQueue.Push(job);
        }

        // 대기중인 작업들을 진행시킴
        public void Flush()
        {
            foreach (ClientSession s in _sessions)
                s.Send(_pendingList);

            _pendingList.Clear();
        }

        // 정보 전송
        public void Broadcast(ArraySegment<byte> segment)
        {
            _pendingList.Add(segment);
        }

        // 플레이어 입장
        public void Enter(ClientSession session)
        {
            // 플레이어 추가
            _sessions.Add(session);
            // 세션에 룸 바인드
            session.Room = this;

            // 입장한 플레이어에게 전체 플레이어 정보 전송
            S_PlayerList players = new S_PlayerList();
            foreach (ClientSession s in _sessions)
            {
                players.players.Add(new S_PlayerList.Player()
                {
                    isSelf = (s == session),
                    playerId = s.SessionId,
                    posX = s.PosX,
                    posY = s.PosY,
                    posZ = s.PosZ,
                });
            }
            session.Send(players.Write());

            // 입장한 플레이어에 대한 정보를 전체 전송
            S_BroadcastEnterGame enter = new S_BroadcastEnterGame();
            enter.playerId = session.SessionId;
            enter.posX = 0;
            enter.posY = 0;
            enter.posZ = 0;
            Broadcast(enter.Write());
        }

        // 플레이어 퇴장
        public void Leave(ClientSession session)
        {
            _sessions.Remove(session);

            // 퇴장 정보를 전체 전송
            S_BroadcastLeaveGame leave = new S_BroadcastLeaveGame();
            leave.playerId = session.SessionId;
            Broadcast(leave.Write());
        }

        // 플레이어 이동
        public void Move(ClientSession session, C_Move c_move)
        {
            session.PosX = c_move.posX;
            session.PosY = c_move.posY;
            session.PosZ = c_move.posZ;

            // 전체 전송
            S_BroadcastMove s_move = new S_BroadcastMove();
            s_move.playerId = session.SessionId;
            s_move.posX = session.PosX;
            s_move.posY = session.PosY;
            s_move.posZ = session.PosZ;
            Broadcast(s_move.Write());
        }
    }
}
