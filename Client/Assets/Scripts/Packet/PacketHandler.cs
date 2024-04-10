using ServerCore;
using System;
using UnityEngine;

public class PacketHandler
{
    internal static void S_EnemyMoveHandler(PacketSession packetSession, IPacket packet)
    {
        throw new NotImplementedException();
    }

    internal static void S_EnemyFireballMoveHandler(PacketSession packetSession, IPacket packet)
    {
        throw new NotImplementedException();
    }

    internal static void S_BroadcastEnemyShotHandler(PacketSession packetSession, IPacket packet)
    {
        throw new NotImplementedException();
    }

    internal static void S_EnemyStatHandler(PacketSession packetSession, IPacket packet)
    {
        throw new NotImplementedException();
    }

    internal static void S_StatHandler(PacketSession packetSession, IPacket packet)
    {
        throw new NotImplementedException();
    }

    internal static void S_DestroyFireballHandler(PacketSession packetSession, IPacket packet)
    {
        throw new NotImplementedException();
    }

    internal static void S_GameoverHandler(PacketSession packetSession, IPacket packet)
    {
        throw new NotImplementedException();
    }

    internal static void S_MatchedHandler(PacketSession packetSession, IPacket packet)
    {
        S_Matched matched = packet as S_Matched;
        Conf.Main.ATTACK_GAGE = matched.attackGage;
        Conf.Main.IS_LEFT = matched.isLeft;
        Conf.Main.HP = matched.hp;
        Conf.Main.ENEMY_NICK = matched.enemyNickname;
        // Class.Method(test);
        //코드여따가

    }


    // 참고용
    //IEnumerator SendMovePacket()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(0.25f);
    //        C_Move move = new C_Move();
    //        move.posX = 1;
    //        move.posY = 1;
    //        move.rotZ = 1;
    //        Managers.Network.Send(move.Write());
    //    }
    //}
}