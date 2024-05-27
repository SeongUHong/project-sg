using ServerCore;
using System;
using System.Collections.Generic;

public class ServerPacketManager
{
	#region Singleton
	static ServerPacketManager _instance = new ServerPacketManager();
	public static ServerPacketManager Instance { get { return _instance; } }
	#endregion

	ServerPacketManager()
	{
		Init();
	}

	Dictionary<ushort, Func<PacketSession, ArraySegment<byte>, IPacket>> _makeFunc = new Dictionary<ushort, Func<PacketSession, ArraySegment<byte>, IPacket>>();
	Dictionary<ushort, Action<PacketSession, IPacket>> _handler = new Dictionary<ushort, Action<PacketSession, IPacket>>();
		
	public void Init()
	{
		_makeFunc.Add((ushort)PacketID.C_Move, MakePacket<C_Move>);
		_handler.Add((ushort)PacketID.C_Move, PacketHandler.C_MoveHandler);
		_makeFunc.Add((ushort)PacketID.C_Shot, MakePacket<C_Shot>);
		_handler.Add((ushort)PacketID.C_Shot, PacketHandler.C_ShotHandler);
		_makeFunc.Add((ushort)PacketID.C_Hit, MakePacket<C_Hit>);
		_handler.Add((ushort)PacketID.C_Hit, PacketHandler.C_HitHandler);
		_makeFunc.Add((ushort)PacketID.C_StartMatch, MakePacket<C_StartMatch>);
		_handler.Add((ushort)PacketID.C_StartMatch, PacketHandler.C_StartMatchHandler);
		_makeFunc.Add((ushort)PacketID.C_ReadyBattle, MakePacket<C_ReadyBattle>);
		_handler.Add((ushort)PacketID.C_ReadyBattle, PacketHandler.C_ReadyBattleHandler);

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