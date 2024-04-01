using ServerCore;
using System;
using System.Net;
using UnityEngine;

public class ServerSession : PacketSession
{
    public override void OnConnected(EndPoint endPoint)
    {
        Managers.Network.OnConnected();
        Debug.Log($"ServerSession OnConnected : {endPoint}");
    }

    public override void OnDisconnected(EndPoint endPoint)
    {
        Managers.Network.OnDisconnected();
        Debug.Log($"ServerSession OnDisconnected : {endPoint}");
    }

    public override void OnRecvPacket(ArraySegment<byte> buffer)
    {
        Managers.Packet.OnRecvPacket(this, buffer);
    }

    public override void OnSend(int byteNum)
    {
    }
}