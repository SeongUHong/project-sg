using Server;
using ServerCore;
using System;

class PacketHandler
{
    internal static void C_StartMatchHandler(PacketSession packetSession, IPacket packet)
    {
        ClientSession session = packetSession as ClientSession;
        C_StartMatch startMatch = packet as C_StartMatch;

        MatchManager.Instance.AddNewPlayer(session, startMatch);
        Console.WriteLine($"Matching start. (sessionId : {session.SessionId}, nickname : {startMatch.nickname})");
    }

    internal static void C_ReadyBattleHandler(PacketSession packetSession, IPacket packet)
    {
        ClientSession session = packetSession as ClientSession;

        // 멀티쓰레드 대비하여 꺼내서 사용
        // BattleRoom이 null로 바뀌어도 무방함
        BattleRoom room = session.BattleRoom;
        if (room == null)
            return;

        room.ReadyBattle(session);
        Console.WriteLine($"Ready for battle. (sessionId : {session.SessionId})");
    }

    internal static void C_MoveHandler(PacketSession packetSession, IPacket packet)
    {
        ClientSession session = packetSession as ClientSession;
        C_Move move = packet as C_Move;

        BattleRoom room = session.BattleRoom;
        if (room == null)
            return;

        room.HandleMove(session, move);
    }

    internal static void C_ShotHandler(PacketSession packetSession, IPacket packet)
    {
        ClientSession session = packetSession as ClientSession;
        C_Shot shot = packet as C_Shot;

        BattleRoom room = session.BattleRoom;
        if (room == null)
            return;

        room.CreateFireball(session, shot);
    }

    internal static void C_HitHandler(PacketSession packetSession, IPacket packet)
    {
        ClientSession session = packetSession as ClientSession;
        C_Hit hit = packet as C_Hit;

        BattleRoom room = session.BattleRoom;
        if (room == null)
            return;

        room.HandleHit(session, hit);
    }
}