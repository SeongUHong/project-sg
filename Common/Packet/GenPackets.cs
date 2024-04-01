using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using ServerCore;

public enum PacketID
{
	S_EnemyMove = 1,
	S_EnemyFireballMove = 2,
	S_BroadcastEnemyShot = 3,
	S_EnemyStat = 4,
	S_Stat = 5,
	S_DestroyFireball = 6,
	S_Gameover = 7,
	S_Matched = 8,
	C_Move = 9,
	C_Shot = 10,
	C_FireballMove = 11,
	C_Attacked = 12,
	C_DestroyFireball = 13,
	C_StartMatch = 14,
	C_ReadyBattle = 15,
	
}

public interface IPacket
{
	ushort Protocol { get; }
	void Read(ArraySegment<byte> segment);
	ArraySegment<byte> Write();
}


public class S_EnemyMove : IPacket
{
	public float posX;
	public float posY;
	public float rotZ;

	public ushort Protocol { get { return (ushort)PacketID.S_EnemyMove; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		this.posX = BitConverter.ToSingle(segment.Array, segment.Offset + count);
		count += sizeof(float);
		this.posY = BitConverter.ToSingle(segment.Array, segment.Offset + count);
		count += sizeof(float);
		this.rotZ = BitConverter.ToSingle(segment.Array, segment.Offset + count);
		count += sizeof(float);
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.S_EnemyMove), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes(this.posX), 0, segment.Array, segment.Offset + count, sizeof(float));
		count += sizeof(float);
		Array.Copy(BitConverter.GetBytes(this.posY), 0, segment.Array, segment.Offset + count, sizeof(float));
		count += sizeof(float);
		Array.Copy(BitConverter.GetBytes(this.rotZ), 0, segment.Array, segment.Offset + count, sizeof(float));
		count += sizeof(float);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class S_EnemyFireballMove : IPacket
{
	public int fireballId;
	public float posX;
	public float posY;

	public ushort Protocol { get { return (ushort)PacketID.S_EnemyFireballMove; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		this.fireballId = BitConverter.ToInt32(segment.Array, segment.Offset + count);
		count += sizeof(int);
		this.posX = BitConverter.ToSingle(segment.Array, segment.Offset + count);
		count += sizeof(float);
		this.posY = BitConverter.ToSingle(segment.Array, segment.Offset + count);
		count += sizeof(float);
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.S_EnemyFireballMove), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes(this.fireballId), 0, segment.Array, segment.Offset + count, sizeof(int));
		count += sizeof(int);
		Array.Copy(BitConverter.GetBytes(this.posX), 0, segment.Array, segment.Offset + count, sizeof(float));
		count += sizeof(float);
		Array.Copy(BitConverter.GetBytes(this.posY), 0, segment.Array, segment.Offset + count, sizeof(float));
		count += sizeof(float);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class S_BroadcastEnemyShot : IPacket
{
	public int fireballId;
	public float posX;
	public float posY;

	public ushort Protocol { get { return (ushort)PacketID.S_BroadcastEnemyShot; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		this.fireballId = BitConverter.ToInt32(segment.Array, segment.Offset + count);
		count += sizeof(int);
		this.posX = BitConverter.ToSingle(segment.Array, segment.Offset + count);
		count += sizeof(float);
		this.posY = BitConverter.ToSingle(segment.Array, segment.Offset + count);
		count += sizeof(float);
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.S_BroadcastEnemyShot), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes(this.fireballId), 0, segment.Array, segment.Offset + count, sizeof(int));
		count += sizeof(int);
		Array.Copy(BitConverter.GetBytes(this.posX), 0, segment.Array, segment.Offset + count, sizeof(float));
		count += sizeof(float);
		Array.Copy(BitConverter.GetBytes(this.posY), 0, segment.Array, segment.Offset + count, sizeof(float));
		count += sizeof(float);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class S_EnemyStat : IPacket
{
	public int hp;
	public float attackGage;

	public ushort Protocol { get { return (ushort)PacketID.S_EnemyStat; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		this.hp = BitConverter.ToInt32(segment.Array, segment.Offset + count);
		count += sizeof(int);
		this.attackGage = BitConverter.ToSingle(segment.Array, segment.Offset + count);
		count += sizeof(float);
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.S_EnemyStat), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes(this.hp), 0, segment.Array, segment.Offset + count, sizeof(int));
		count += sizeof(int);
		Array.Copy(BitConverter.GetBytes(this.attackGage), 0, segment.Array, segment.Offset + count, sizeof(float));
		count += sizeof(float);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class S_Stat : IPacket
{
	public int hp;
	public float attackGage;

	public ushort Protocol { get { return (ushort)PacketID.S_Stat; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		this.hp = BitConverter.ToInt32(segment.Array, segment.Offset + count);
		count += sizeof(int);
		this.attackGage = BitConverter.ToSingle(segment.Array, segment.Offset + count);
		count += sizeof(float);
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.S_Stat), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes(this.hp), 0, segment.Array, segment.Offset + count, sizeof(int));
		count += sizeof(int);
		Array.Copy(BitConverter.GetBytes(this.attackGage), 0, segment.Array, segment.Offset + count, sizeof(float));
		count += sizeof(float);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class S_DestroyFireball : IPacket
{
	public int fireballId;

	public ushort Protocol { get { return (ushort)PacketID.S_DestroyFireball; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		this.fireballId = BitConverter.ToInt32(segment.Array, segment.Offset + count);
		count += sizeof(int);
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.S_DestroyFireball), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes(this.fireballId), 0, segment.Array, segment.Offset + count, sizeof(int));
		count += sizeof(int);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class S_Gameover : IPacket
{
	public bool isWin;

	public ushort Protocol { get { return (ushort)PacketID.S_Gameover; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		this.isWin = BitConverter.ToBoolean(segment.Array, segment.Offset + count);
		count += sizeof(bool);
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.S_Gameover), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes(this.isWin), 0, segment.Array, segment.Offset + count, sizeof(bool));
		count += sizeof(bool);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class S_Matched : IPacket
{
	public string enemyNickname;
	public bool isLeft;
	public int hp;
	public float attackGage;

	public ushort Protocol { get { return (ushort)PacketID.S_Matched; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		ushort enemyNicknameLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.enemyNickname = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, enemyNicknameLen);
		count += enemyNicknameLen;
		this.isLeft = BitConverter.ToBoolean(segment.Array, segment.Offset + count);
		count += sizeof(bool);
		this.hp = BitConverter.ToInt32(segment.Array, segment.Offset + count);
		count += sizeof(int);
		this.attackGage = BitConverter.ToSingle(segment.Array, segment.Offset + count);
		count += sizeof(float);
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.S_Matched), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		ushort enemyNicknameLen = (ushort)Encoding.Unicode.GetBytes(this.enemyNickname, 0, this.enemyNickname.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(enemyNicknameLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += enemyNicknameLen;
		Array.Copy(BitConverter.GetBytes(this.isLeft), 0, segment.Array, segment.Offset + count, sizeof(bool));
		count += sizeof(bool);
		Array.Copy(BitConverter.GetBytes(this.hp), 0, segment.Array, segment.Offset + count, sizeof(int));
		count += sizeof(int);
		Array.Copy(BitConverter.GetBytes(this.attackGage), 0, segment.Array, segment.Offset + count, sizeof(float));
		count += sizeof(float);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class C_Move : IPacket
{
	public float posX;
	public float posY;
	public float rotZ;

	public ushort Protocol { get { return (ushort)PacketID.C_Move; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		this.posX = BitConverter.ToSingle(segment.Array, segment.Offset + count);
		count += sizeof(float);
		this.posY = BitConverter.ToSingle(segment.Array, segment.Offset + count);
		count += sizeof(float);
		this.rotZ = BitConverter.ToSingle(segment.Array, segment.Offset + count);
		count += sizeof(float);
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.C_Move), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes(this.posX), 0, segment.Array, segment.Offset + count, sizeof(float));
		count += sizeof(float);
		Array.Copy(BitConverter.GetBytes(this.posY), 0, segment.Array, segment.Offset + count, sizeof(float));
		count += sizeof(float);
		Array.Copy(BitConverter.GetBytes(this.rotZ), 0, segment.Array, segment.Offset + count, sizeof(float));
		count += sizeof(float);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class C_Shot : IPacket
{
	public bool isShot;

	public ushort Protocol { get { return (ushort)PacketID.C_Shot; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		this.isShot = BitConverter.ToBoolean(segment.Array, segment.Offset + count);
		count += sizeof(bool);
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.C_Shot), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes(this.isShot), 0, segment.Array, segment.Offset + count, sizeof(bool));
		count += sizeof(bool);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class C_FireballMove : IPacket
{
	public int fireballId;
	public float posX;
	public float posY;

	public ushort Protocol { get { return (ushort)PacketID.C_FireballMove; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		this.fireballId = BitConverter.ToInt32(segment.Array, segment.Offset + count);
		count += sizeof(int);
		this.posX = BitConverter.ToSingle(segment.Array, segment.Offset + count);
		count += sizeof(float);
		this.posY = BitConverter.ToSingle(segment.Array, segment.Offset + count);
		count += sizeof(float);
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.C_FireballMove), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes(this.fireballId), 0, segment.Array, segment.Offset + count, sizeof(int));
		count += sizeof(int);
		Array.Copy(BitConverter.GetBytes(this.posX), 0, segment.Array, segment.Offset + count, sizeof(float));
		count += sizeof(float);
		Array.Copy(BitConverter.GetBytes(this.posY), 0, segment.Array, segment.Offset + count, sizeof(float));
		count += sizeof(float);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class C_Attacked : IPacket
{
	public int fireballId;

	public ushort Protocol { get { return (ushort)PacketID.C_Attacked; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		this.fireballId = BitConverter.ToInt32(segment.Array, segment.Offset + count);
		count += sizeof(int);
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.C_Attacked), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes(this.fireballId), 0, segment.Array, segment.Offset + count, sizeof(int));
		count += sizeof(int);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class C_DestroyFireball : IPacket
{
	public int fireballId;

	public ushort Protocol { get { return (ushort)PacketID.C_DestroyFireball; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		this.fireballId = BitConverter.ToInt32(segment.Array, segment.Offset + count);
		count += sizeof(int);
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.C_DestroyFireball), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes(this.fireballId), 0, segment.Array, segment.Offset + count, sizeof(int));
		count += sizeof(int);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class C_StartMatch : IPacket
{
	public string nickname;

	public ushort Protocol { get { return (ushort)PacketID.C_StartMatch; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		ushort nicknameLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.nickname = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, nicknameLen);
		count += nicknameLen;
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.C_StartMatch), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		ushort nicknameLen = (ushort)Encoding.Unicode.GetBytes(this.nickname, 0, this.nickname.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(nicknameLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += nicknameLen;

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class C_ReadyBattle : IPacket
{
	

	public ushort Protocol { get { return (ushort)PacketID.C_ReadyBattle; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.C_ReadyBattle), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

