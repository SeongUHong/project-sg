using DummyClient;
using ServerCore;
using System;

class PacketHandler
{

    internal static void S_MatchedHandler(PacketSession packetSession, IPacket packet)
    {
        ServerSession session = packetSession as ServerSession;
        S_Matched matched = packet as S_Matched;

        session.IsMatched = true;
        Console.WriteLine($"Matched With : {matched.enemyNickname}");
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

    internal static void S_BroadcastGameStartHandler(PacketSession packetSession, IPacket packet)
    {
        ServerSession session = packetSession as ServerSession;
        session.IsReady = true;
        Console.WriteLine("GameStart");
    }

    internal static void S_GameoverHandler(PacketSession arg1, IPacket arg2)
    {
    }
}