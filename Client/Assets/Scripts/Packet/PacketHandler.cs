﻿using ServerCore;
using System;
using UnityEngine;

public class PacketHandler
{
    internal static void S_EnemyMoveHandler(PacketSession packetSession, IPacket packet)
    {
        S_EnemyMove move = packet as S_EnemyMove;
        Vector3 enemyPosition = new Vector3();
        enemyPosition.x = move.posX;
        enemyPosition.y = move.posY;
        Managers.Game.EnemyPosition = enemyPosition;
        Vector3 enemyRotation = new Vector3();
        enemyRotation.z = move.rotZ;
        Managers.Game.EnemyRotate = enemyRotation;
    }

    internal static void S_MatchedHandler(PacketSession packetSession, IPacket packet)
    {
        S_Matched matched = packet as S_Matched;
        Managers.Game.IsLeft = matched.isLeft;
        Managers.Game.EnemyNick = matched.enemyNickname;

        Debug.Log("S_MatchedHandler");

        BaseScene scene = Managers.Scene.CurrentScene;

        Matching_Panel matchingPanel = scene.getUI<Matching_Panel>() as Matching_Panel;
        matchingPanel.Hide();

        Loading_Panel loadingPanel = scene.getUI<Loading_Panel>() as Loading_Panel;
        loadingPanel.SetNickName();
        loadingPanel.Show();
    }

    internal static void S_FireballMoveHandler(PacketSession arg1, IPacket arg2)
    {
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

        Debug.Log("S_BroadcastGameStartHandler");
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