using Server;
using ServerCore;
using System;

class PacketHandler
{
    internal static void C_MoveHandler(PacketSession packetSession, IPacket packet)
    {
        // 멀티쓰레드 대비하여 꺼내서 사용
        // BattleRoom null로 바뀌어도 무방함
        //BattleRoom room = session.BattleRoom;
        //if (room == null)
        //    return;
    }

    internal static void C_ShotHandler(PacketSession packetSession, IPacket packet)
    {
    }

    internal static void C_FireballMoveHandler(PacketSession packetSession, IPacket packet)
    {
    }

    internal static void C_AttackedHandler(PacketSession packetSession, IPacket packet)
    {
    }

    internal static void C_DestroyFireballHandler(PacketSession packetSession, IPacket packet)
    {
    }

    internal static void C_StartMatchHandler(PacketSession packetSession, IPacket packet)
    {
        ClientSession session = packetSession as ClientSession;
        C_StartMatch startMatch = packet as C_StartMatch;

        MatchManager.Instance.AddNewPlayer(session, startMatch);
        Console.WriteLine($"Matching start. (sessionId : {session.SessionId}, nickname : {startMatch.nickname})");
    }

    internal static void C_ReadyBattleHandler(PacketSession packetSession, IPacket packet)
    {
    }
}