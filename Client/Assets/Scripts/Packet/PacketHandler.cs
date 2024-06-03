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

        if (Managers.Game.EnemyNick != null)
        {
            Conf.Main._maching.Awake();
            Conf.Main._loading.Show();
        }

        // Class.Method(test);

    }

    internal static void S_FireballMoveHandler(PacketSession packetSession, IPacket packet)
    {
        S_FireballMove fireballMove = packet as S_FireballMove;

        GameObject fireball = Managers.Skill.GetFireBall(fireballMove.fireballId);

        //fireball.GetComponent<LaunchSkillController>().FixPosition(fireballMove.posX, fireballMove.posY, fireballMove.rotZ);
    }

    internal static void S_EnemyShotHandler(PacketSession packetSession, IPacket packet)
    {
        S_EnemyShot enemyShot = packet as S_EnemyShot;
        Vector3 _enemyShot = new Vector3();
        _enemyShot.x = enemyShot.posX;
        _enemyShot.y = enemyShot.posY;
        _enemyShot.z = enemyShot.rotZ;

        Managers.Skill.EnemyShot();

        GameObject fireball = Managers.Skill.SkillInitiate("fireballbluebig", enemyShot.fireballId, new Vector2(_enemyShot.x, _enemyShot.y));

        Managers.Skill.SetFireBallID(enemyShot.fireballId);
        Managers.Skill.AddFireBall(fireball, enemyShot.fireballId);



    }

    internal static void S_ShotHandler(PacketSession packetSession, IPacket packet)
    {
        S_Shot shot = packet as S_Shot;
        Vector3 playerShot = new Vector3();
        playerShot.x = shot.posX;
        playerShot.y = shot.posY;
        playerShot.z = shot.rotZ;

        //발사허가
        Managers.Game.CanShoot = true;

        Debug.Log($"S_ShotHandler");

        if (Managers.Game.CanShoot)
        {
            GameObject fireball = Managers.Skill.SkillInitiate("fireballredbig",shot.fireballId, new Vector2(playerShot.x,playerShot.y));
            
            Managers.Skill.SetFireBallID(shot.fireballId);
            Managers.Skill.AddFireBall(fireball, shot.fireballId);

            Managers.Game.CanShoot = false;
        }

        

    }

    internal static void S_AttackedHandler(PacketSession packetSession, IPacket packet)
    {
        
    }

    internal static void S_HitHandler(PacketSession packetSession, IPacket packet)
    {
        
    }

    internal static void S_GameoverHandler(PacketSession packetSession, IPacket packet)
    {
        
    }

    internal static void S_BroadcastGameStartHandler(PacketSession packetSession, IPacket packet)
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