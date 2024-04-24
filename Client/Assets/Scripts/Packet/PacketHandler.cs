﻿using ServerCore;
using System;
using UnityEngine;

public class PacketHandler
{
    internal static void S_EnemyMoveHandler(PacketSession packetSession, IPacket packet)
    {
        throw new NotImplementedException();
        C_Move move = packet as C_Move;
        Vector3 enemuRocation = new Vector3();
        enemuRocation.x = move.posX;
        enemuRocation.y = move.posY;
        enemuRocation.z = move.rotZ;
        Managers.Game.EnemyRocation = enemuRocation;

        Debug.Log($"EnemyMove x : {Managers.Game.EnemyRocation.x}");
        Debug.Log($"EnemyMove y : {Managers.Game.EnemyRocation.y}");
        Debug.Log($"EnemyMove z : {Managers.Game.EnemyRocation.z}");
    }

    internal static void S_MatchedHandler(PacketSession packetSession, IPacket packet)
    {
        S_Matched matched = packet as S_Matched;
        Managers.Game.IsLeft = matched.isLeft;
        Managers.Game.EnemyNick = matched.enemyNickname;

        if (Managers.Game.EnemyNick != null)
        {
            Conf.Main._maching.Awake();
            Conf.Main._loading.Show();
        }
        

        // Class.Method(test);

    }

    internal static void S_EnemyShotHandler(PacketSession arg1, IPacket arg2)
    {
        
    }

    internal static void S_ShotHandler(PacketSession arg1, IPacket arg2)
    {
        
    }

    internal static void S_AttackedHandler(PacketSession arg1, IPacket arg2)
    {
        
    }

    internal static void S_HitHandler(PacketSession arg1, IPacket arg2)
    {
        
    }

    internal static void S_GameoverHandler(PacketSession arg1, IPacket arg2)
    {
        
    }

    internal static void S_BroadcastGameStartHandler(PacketSession arg1, IPacket arg2)
    {
        //게임씬로드시 C_ReadyBattle를 보내면 서버에서 이패킷이 옴 이걸 받으면 게임시작하도록 멈춰놓기
        Managers.Game.IsPause = true;
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