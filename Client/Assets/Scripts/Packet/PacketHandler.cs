using ServerCore;
using System;
using UnityEngine;

public class PacketHandler
{
    internal static void S_BroadcastEnterGameHandler(PacketSession arg1, IPacket arg2)
    {
        throw new NotImplementedException();
    }

    internal static void S_BroadcastLeaveGameHandler(PacketSession arg1, IPacket arg2)
    {
        throw new NotImplementedException();
    }

    internal static void S_PlayerListHandler(PacketSession arg1, IPacket arg2)
    {
        throw new NotImplementedException();
    }

    internal static void S_BroadcastMoveHandler(PacketSession packetSession, IPacket packet)
    {
        S_BroadcastMove broadcastMove = packet as S_BroadcastMove;
        ServerSession session = packetSession as ServerSession;

        Debug.Log($"S_BroadcastMove : {broadcastMove.posX}, {broadcastMove.posY}");
    }
}