using ServerCore;
using System;
using System.Collections.Generic;
using System.Net;

namespace DummyClient
{
    public class ServerSession : PacketSession
    {
        List<int> _fireballIds = new List<int>();

        public int Id { get; set; }
        public bool IsMatched { get; set; }
        public bool IsReady { get; set; }
        public bool IsHit { get; set; }
        public ushort ShootCount { get; set; }
        public int FirstFireballId { 
            get 
            {
                if (_fireballIds.Count > 0)
                    return _fireballIds[0];
                else
                    return -1;
            }
        }

        public void AddFireballId(int fireballId)
        {
            _fireballIds.Add(fireballId);
        }

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