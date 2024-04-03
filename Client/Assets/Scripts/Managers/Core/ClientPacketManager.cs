using ServerCore;
using System;
using System.Collections.Generic;

public class ClientPacketManager
{
	Dictionary<ushort, Func<PacketSession, ArraySegment<byte>, IPacket>> _makeFunc = new Dictionary<ushort, Func<PacketSession, ArraySegment<byte>, IPacket>>();
	Dictionary<ushort, Action<PacketSession, IPacket>> _handler = new Dictionary<ushort, Action<PacketSession, IPacket>>();
		
	public void Register()
	{
		_makeFunc.Add((ushort)PacketID.S_EnemyMove, MakePacket<S_EnemyMove>);
		_handler.Add((ushort)PacketID.S_EnemyMove, PacketHandler.S_EnemyMoveHandler);
		_makeFunc.Add((ushort)PacketID.S_EnemyFireballMove, MakePacket<S_EnemyFireballMove>);
		_handler.Add((ushort)PacketID.S_EnemyFireballMove, PacketHandler.S_EnemyFireballMoveHandler);
		_makeFunc.Add((ushort)PacketID.S_BroadcastEnemyShot, MakePacket<S_BroadcastEnemyShot>);
		_handler.Add((ushort)PacketID.S_BroadcastEnemyShot, PacketHandler.S_BroadcastEnemyShotHandler);
		_makeFunc.Add((ushort)PacketID.S_EnemyStat, MakePacket<S_EnemyStat>);
		_handler.Add((ushort)PacketID.S_EnemyStat, PacketHandler.S_EnemyStatHandler);
		_makeFunc.Add((ushort)PacketID.S_Stat, MakePacket<S_Stat>);
		_handler.Add((ushort)PacketID.S_Stat, PacketHandler.S_StatHandler);
		_makeFunc.Add((ushort)PacketID.S_DestroyFireball, MakePacket<S_DestroyFireball>);
		_handler.Add((ushort)PacketID.S_DestroyFireball, PacketHandler.S_DestroyFireballHandler);
		_makeFunc.Add((ushort)PacketID.S_Gameover, MakePacket<S_Gameover>);
		_handler.Add((ushort)PacketID.S_Gameover, PacketHandler.S_GameoverHandler);
		_makeFunc.Add((ushort)PacketID.S_Matched, MakePacket<S_Matched>);
		_handler.Add((ushort)PacketID.S_Matched, PacketHandler.S_MatchedHandler);

	}

	public void OnRecvPacket(PacketSession session, ArraySegment<byte> buffer, Action<PacketSession, IPacket> onRecvCallback = null)
	{
		ushort count = 0;

		ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
		count += 2;
		ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
		count += 2;

		Func<PacketSession, ArraySegment<byte>, IPacket> func = null;
		if (_makeFunc.TryGetValue(id, out func))
        {
			IPacket packet = func.Invoke(session, buffer);
			if (onRecvCallback != null)
				onRecvCallback.Invoke(session, packet);
			else
				HandlePacket(session, packet);
        }
	}

	T MakePacket<T>(PacketSession session, ArraySegment<byte> buffer) where T : IPacket, new()
	{
		T pkt = new T();
		pkt.Read(buffer);
		return pkt;
	}

	public void HandlePacket(PacketSession session, IPacket packet)
    {
		Action<PacketSession, IPacket> action = null;
		if (_handler.TryGetValue(packet.Protocol, out action))
			action.Invoke(session, packet);
	}
}