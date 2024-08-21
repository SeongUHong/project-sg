using ServerCore;
using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class NetworkManager : ManagerBase
{
    ServerSession _session;
    bool _isConnect = false;

    public bool IsConnet { get { return _isConnect; } }

    public override void Init()
    {
        // Local
        string host = Dns.GetHostName();
        IPHostEntry ipHost = Dns.GetHostEntry(host);
        IPAddress ipAddr = ipHost.AddressList[0];
        IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

        // surviveinjapan.com
        //IPAddress ipAddr = IPAddress.Parse(Conf.Main.SERVER_IP);
        //IPEndPoint endPoint = new IPEndPoint(ipAddr, Conf.Main.SERVER_PORT);

        Connector connector = new Connector();
        _session = new ServerSession();

        connector.Connect(
            endPoint,
            () => { return _session; },
            1
        );
    }

    //void Update()
    //{
    //    if (!_isConnect)
    //        return;

    //    HandlePackets();
    //}

    // 패킷 처리
    public void HandlePackets()
    {
        List<IPacket> list = PacketQueue.Instance.PapAll();
        foreach (IPacket packet in list)
            Managers.Packet.HandlePacket(_session, packet);
    }

    // 버퍼 전송
    public void Send(ArraySegment<byte> sendBuff)
    {
        _session.Send(sendBuff);
    }

    // 서버 연결 성공시
    public void OnConnected()
    {
        _isConnect = true;
    }

    // 서버 연결 종료시
    public void OnDisconnected()
    {
        _isConnect = false;
    }
}
