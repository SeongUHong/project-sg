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
        
    }

    internal static void C_ShotHandler(PacketSession arg1, IPacket arg2)
    {
    }

    internal static void C_AttackedHandler(PacketSession arg1, IPacket arg2)
    {
    }

    internal static void C_HitHandler(PacketSession arg1, IPacket arg2)
    {
    }
}