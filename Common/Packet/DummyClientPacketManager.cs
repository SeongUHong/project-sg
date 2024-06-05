using ServerCore;
using System;
using System.Collections.Generic;

public class ClientPacketManager
{
	#region Singleton
	static ClientPacketManager _instance = new ClientPacketManager();
	public static ClientPacketManager Instance { get { return _instance; } }
	#endregion

	ClientPacketManager()
	{
		Init();
	}

	Dictionary<ushort, Func<PacketSession, ArraySegment<byte>, IPacket>> _makeFunc = new Dictionary<ushort, Func<PacketSession, ArraySegment<byte>, IPacket>>();
	Dictionary<ushort, Action<PacketSession, IPacket>> _handler = new Dictionary<ushort, Action<PacketSession, IPacket>>();
		
	public void Init()
	{
		_makeFunc.Add((ushort)PacketID.S_EnemyMove, MakePacket<S_EnemyMove>);
		_handler.Add((ushort)PacketID.S_EnemyMove, PacketHandler.S_EnemyMoveHandler);
		_makeFunc.Add((ushort)PacketID.S_EnemyShot, MakePacket<S_EnemyShot>);
		_handler.Add((ushort)PacketID.S_EnemyShot, PacketHandler.S_EnemyShotHandler);
		_makeFunc.Add((ushort)PacketID.S_Shot, MakePacket<S_Shot>);
		_handler.Add((ushort)PacketID.S_Shot, PacketHandler.S_ShotHandler);
		_makeFunc.Add((ushort)PacketID.S_Hit, MakePacket<S_Hit>);
		_handler.Add((ushort)PacketID.S_Hit, PacketHandler.S_HitHandler);
		_makeFunc.Add((ushort)PacketID.S_EnemyHit, MakePacket<S_EnemyHit>);
		_handler.Add((ushort)PacketID.S_EnemyHit, PacketHandler.S_EnemyHitHandler);
		_makeFunc.Add((ushort)PacketID.S_Matched, MakePacket<S_Matched>);
		_handler.Add((ushort)PacketID.S_Matched, PacketHandler.S_MatchedHandler);
		_makeFunc.Add((ushort)PacketID.S_BroadcastGameStart, MakePacket<S_BroadcastGameStart>);
		_handler.Add((ushort)PacketID.S_BroadcastGameStart, PacketHandler.S_BroadcastGameStartHandler);
		_makeFunc.Add((ushort)PacketID.S_Gameover, MakePacket<S_Gameover>);
		_handler.Add((ushort)PacketID.S_Gameover, PacketHandler.S_GameoverHandler);
		_makeFunc.Add((ushort)PacketID.S_CountTime, MakePacket<S_CountTime>);
		_handler.Add((ushort)PacketID.S_CountTime, PacketHandler.S_CountTimeHandler);

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