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

    internal static void S_EnemyMoveHandler(PacketSession packetSession, IPacket packet)
    {
    }

    internal static void S_EnemyShotHandler(PacketSession arg1, IPacket arg2)
    {
    }

    internal static void S_ShotHandler(PacketSession packetSession, IPacket packet)
    {
        ServerSession session = packetSession as ServerSession;
        S_Shot shot = packet as S_Shot;

        session.AddFireballId(shot.fireballId);

        Console.WriteLine($"shoot fireball (fireballId : {shot.fireballId})");
    }

    internal static void S_HitHandler(PacketSession packetSession, IPacket packet)
    {
        ServerSession session = packetSession as ServerSession;
        session.IsHit = true;
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

    internal static void S_EnemyHitHandler(PacketSession arg1, IPacket arg2)
    {
    }

    internal static void S_CountTimeHandler(PacketSession packetSession, IPacket packet)
    {
        S_CountTime countTime = packet as S_CountTime;

        Console.WriteLine($"Elapsed time : {countTime.elapsedSec}");
    }
}