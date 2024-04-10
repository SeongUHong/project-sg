using ServerCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Server
{
    class ClientSession : PacketSession
    {
        public ushort SessionId { get; set; }
        public BattleRoom BattleRoom { get; set; }
        public Player Player { get; set; }

        public override void OnConnected(EndPoint endPoint)
        {
            Console.WriteLine($"ClientSession OnConnected : {endPoint}, SessionId : {SessionId}");
        }

        public override void OnDisconnected(EndPoint endPoint)
        {
            // 대기 목록에서 삭제
            MatchManager.Instance.RemoveWaitPlayer(this);
            Console.WriteLine($"ClientSession OnDisconnected : {endPoint}, SessionId : {SessionId}");
        }

        public override void OnRecvPacket(ArraySegment<byte> buffer)
        {
            ServerPacketManager.Instance.OnRecvPacket(this, buffer);
        }

        public override void OnSend(int byteNum)
        {
        }
    }
}
