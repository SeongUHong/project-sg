using Server;
using ServerCore;
using System;

class PacketHandler
{
    internal static void C_LeaveGameHandler(PacketSession session, IPacket packet)
    {
        C_LeaveGame leaveGame = packet as C_LeaveGame;
        ClientSession clientSession = session as ClientSession;
    }

    internal static void C_MoveHandler(PacketSession session, IPacket packet)
    {
        C_Move move = packet as C_Move;
        ClientSession clientSession = session as ClientSession;

        Console.WriteLine($"{move.posX}, {move.posY}, {move.posZ}");
    }
}