using DummyClient;
using ServerCore;
using System;

class PacketHandler
{

    internal static void S_MatchedHandler(PacketSession packetSession, IPacket packet)
    {
        ServerSession session = packetSession as ServerSession;
        S_Matched matched = packet as S_Matched;
        SessionManager.Instance.HandleMatched(session, matched);
    }

    internal static void S_EnemyMoveHandler(PacketSession arg1, IPacket arg2)
    {
    }

    internal static void S_EnemyShotHandler(PacketSession arg1, IPacket arg2)
    {
    }

    internal static void S_ShotHandler(PacketSession arg1, IPacket arg2)
    {
    }

    internal static void S_AttackedHandler(PacketSession arg1, IPacket arg2)
    {
    }

    internal static void S_HitHandler(PacketSession arg1, IPacket arg2)
    {
    }

    internal static void S_BroadcastGameStartHandler(PacketSession arg1, IPacket arg2)
    {
    }

    internal static void S_GameoverHandler(PacketSession arg1, IPacket arg2)
    {
    }
}