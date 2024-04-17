using ServerCore;
using System;
using UnityEngine;

public class PacketHandler
{
    internal static void S_EnemyMoveHandler(PacketSession packetSession, IPacket packet)
    {
        throw new NotImplementedException();
        C_Move move = packet as C_Move;
        Conf.Main.ENEMY_ROCATION.x = move.posX;
        Conf.Main.ENEMY_ROCATION.y = move.posY;
        Conf.Main.ENEMY_ROCATION.z = move.rotZ;

    }

    internal static void S_MatchedHandler(PacketSession packetSession, IPacket packet)
    {
        S_Matched matched = packet as S_Matched;
        Conf.Main.IS_LEFT = matched.isLeft;
        Conf.Main.ENEMY_NICK = matched.enemyNickname;
        // Class.Method(test);
        //코드여따가

    }

    internal static void S_GameoverHandler(PacketSession arg1, IPacket arg2)
    {
        throw new NotImplementedException();
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