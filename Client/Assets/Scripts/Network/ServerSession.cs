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
        // 유니티 오브젝트는 메인 스레드만 수정이 가능함
        // 따라서 큐에 쌓아놓고 메인 스레드가 처리하도록 함
        Managers.Packet.OnRecvPacket(this, buffer, (session, packet) => PacketQueue.Instance.Push(packet));
    }

    public override void OnSend(int byteNum)
    {
    }
}