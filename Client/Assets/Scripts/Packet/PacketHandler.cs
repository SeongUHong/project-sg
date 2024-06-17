using ServerCore;
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

        Vector3 newRotation = Managers.Game.EnemyRotate; // 현재 회전 값을 가져옴
        newRotation.z = move.angle; // z축 회전 값을 변경
        Managers.Game.EnemyRotate = newRotation;
    }

    internal static void S_MatchedHandler(PacketSession packetSession, IPacket packet)
    {
        S_Matched matched = packet as S_Matched;
        Managers.Game.IsLeft = matched.isLeft;
        Managers.Game.EnemyNick = matched.enemyNickname;

        BaseScene scene = Managers.Scene.CurrentScene;

        Matching_Panel matchingPanel = scene.GetUI<Matching_Panel>() as Matching_Panel;
        matchingPanel.Hide();

        Loading_Panel loadingPanel = scene.GetUI<Loading_Panel>() as Loading_Panel;
        loadingPanel.SetNickName();
        loadingPanel.Show();
    }

    internal static void S_FireballMoveHandler(PacketSession packetSession, IPacket packet)
    {
        //S_FireballMove fireballMove = packet as S_FireballMove;

        //GameObject fireball = Managers.Skill.GetFireBall(fireballMove.fireballId);

        //fireball.GetComponent<LaunchSkillController>().FixPosition(fireballMove.posX, fireballMove.posY, fireballMove.rotZ);

    }

    internal static void S_EnemyHitHandler(PacketSession packetSession, IPacket packet)
    {
        S_EnemyHit enemyHit = packet as S_EnemyHit;
        int fireballid = enemyHit.fireballId;

        Stat _enemyStat = Managers.Game.Enemy.GetComponent<Stat>();
        _enemyStat.OnAttacked(5);


        GameObject fireball = Managers.Skill.GetFireBall(fireballid);
        //파이어볼 오브젝트 삭제
        Managers.Skill.DestroyFireBall(fireball); //객체를 삭제한다
        //번호로 저장된 탄환이름을 리스트에서 삭제
        Managers.Skill.DeleteFireBall(fireballid);
    }

    internal static void S_EnemyShotHandler(PacketSession packetSession, IPacket packet)
    {
        Debug.Log("S_Enemy");

        S_EnemyShot enemyShot = packet as S_EnemyShot;
        Vector3 _enemyShot = new Vector3();
        _enemyShot.x = enemyShot.posX;
        _enemyShot.y = enemyShot.posY;
        _enemyShot.z = enemyShot.angle;

        Managers.Skill.EnemyShot();

        GameObject fireball = Managers.Skill.SkillInitiate("fireballbluebig", enemyShot.fireballId, new Vector2(_enemyShot.x, _enemyShot.y));

        Managers.Skill.SetFireBallID(enemyShot.fireballId);
        Managers.Skill.AddFireBall(fireball, enemyShot.fireballId);

       
    }

    internal static void S_CountTimeHandler(PacketSession packetSession, IPacket packet)
    {
        S_CountTime count = packet as S_CountTime;
        int second = count.remainSec;

        Managers.Game.RemainSec = second;
    }

    internal static void S_ShotHandler(PacketSession packetSession, IPacket packet)
    {
        S_Shot shot = packet as S_Shot;
        Vector3 playerShot = new Vector3();
        playerShot.x = shot.posX;
        playerShot.y = shot.posY;
        playerShot.z = shot.angle;

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

    internal static void S_HitHandler(PacketSession packetSession, IPacket packet)
    {
        S_Hit hit = packet as S_Hit;
        int fireballid = hit.fireballId;

        Stat _playerStat = Managers.Game.Player.GetComponent<Stat>();
        _playerStat.OnAttacked(5);


        GameObject fireball = Managers.Skill.GetFireBall(fireballid);
        //파이어볼 오브젝트 삭제
        Managers.Skill.DestroyFireBall(fireball); //객체를 삭제한다
        //번호로 저장된 탄환이름을 리스트에서 삭제
        Managers.Skill.DeleteFireBall(fireballid);
        
    }

    internal static void S_GameoverHandler(PacketSession packetSession, IPacket packet)
    {
        //status =1 윈, status = 2 루즈 ,stauts = 3 드로우
        S_Gameover gameover = packet as S_Gameover;
        int status = gameover.status;

        Animator p_animator = Managers.Game.Player.GetComponent<Animator>();
        Animator e_animator = Managers.Game.Enemy.GetComponent<Animator>();

        Debug.Log($"{status}");

        if (status == 1)
        {
            Managers.Game.EnemyDeadFlag = true;

            if (Managers.Game.IsLeft)
            {
                if (Managers.Game.EnemyDeadFlag && Managers.Game.Enemy != null)
                {
                    e_animator.SetBool("expl", true);
                    Conf.Main._result.SetText();
                    Conf.Main._result.Show();
                }
            }
            else
            {
                if (Managers.Game.EnemyDeadFlag && Managers.Game.Enemy_Left != null)
                {
                    e_animator.SetBool("expl", true);
                    Conf.Main._result.SetText();
                    Conf.Main._result.Show();
                }
            }
        }
        else if (status == 2)
        {
            Managers.Game.PlayerDeadFlag = true;

            if (Managers.Game.IsLeft)
            {
                if (Managers.Game.PlayerDeadFlag && Managers.Game.Player != null)
                {
                    p_animator.SetBool("expl", true);
                    Conf.Main._result.SetText();
                    Conf.Main._result.Show();
                }
            }
            else
            {
                if (Managers.Game.PlayerDeadFlag && Managers.Game.Player_Right != null)
                {
                    p_animator.SetBool("expl", true);
                    Conf.Main._result.SetText();
                    Conf.Main._result.Show();
                }
            }
        }
        else if(status == 3)
        {
            Managers.Game.PlayerDeadFlag = true;
            Managers.Game.EnemyDeadFlag = true;

            if (Managers.Game.IsLeft)
            {
                if (Managers.Game.PlayerDeadFlag && Managers.Game.Player != null)
                {
                    p_animator.SetBool("expl", true);
                    Conf.Main._result.SetText();
                    Conf.Main._result.Show();
                }
                if (Managers.Game.EnemyDeadFlag && Managers.Game.Enemy != null)
                {
                    e_animator.SetBool("expl", true);
                    Conf.Main._result.SetText();
                    Conf.Main._result.Show();
                }
            }
            else
            {
                if (Managers.Game.PlayerDeadFlag && Managers.Game.Player_Right != null)
                {
                    p_animator.SetBool("expl", true);
                    Conf.Main._result.SetText();
                    Conf.Main._result.Show();
                }
                if (Managers.Game.EnemyDeadFlag && Managers.Game.Enemy_Left != null)
                {
                    e_animator.SetBool("expl", true);
                    Conf.Main._result.SetText();
                    Conf.Main._result.Show();
                }
            }

                Managers.Game.IsPause = true;
        }

        

        
    }

    internal static void S_BroadcastGameStartHandler(PacketSession packetSession, IPacket packet)
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