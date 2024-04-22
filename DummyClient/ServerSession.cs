using ServerCore;
using System;
using System.Net;

namespace DummyClient
{
    public class ServerSession : PacketSession
    {
        public int Id { get; set; }
        public bool IsMatched { get; set; }
        public bool IsReady { get; set; }

        public override void OnConnected(EndPoint endPoint)
        {
            Console.WriteLine($"ServerSession OnConnected : {endPoint}");
        }

        public override void OnDisconnected(EndPoint endPoint)
        {
            Console.WriteLine($"ServerSession OnDisconnected : {endPoint}");
        }

        public override void OnRecvPacket(ArraySegment<byte> buffer)
        {
            ClientPacketManager.Instance.OnRecvPacket(this, buffer);
        }

        public override void OnSend(int byteNum)
        {
        }
    }
}