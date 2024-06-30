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
        string host = Dns.GetHostName();
        IPHostEntry ipHost = Dns.GetHostEntry(host);
        IPAddress ipAddr = ipHost.AddressList[0];
        IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

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

    // ��Ŷ ó��
    public void HandlePackets()
    {
        List<IPacket> list = PacketQueue.Instance.PapAll();
        foreach (IPacket packet in list)
            Managers.Packet.HandlePacket(_session, packet);
    }

    // ���� ����
    public void Send(ArraySegment<byte> sendBuff)
    {
        _session.Send(sendBuff);
    }

    // ���� ���� ������
    public void OnConnected()
    {
        _isConnect = true;
    }

    // ���� ���� �����
    public void OnDisconnected()
    {
        _isConnect = false;
    }
}
