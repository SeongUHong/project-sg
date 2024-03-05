using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBtnHandler : MonoBehaviour
{
    //방향
    protected Vector2 _dir = Vector2.up;
    //스텟
    protected Stat _stat;
    //공격 이름
    string SKILL_NAME = "fireballredbig";

    //적 공격이름(테스트용)
    //string SKILL_NAME = "fireballbluebig";

    public void OnAttack()
    {
        Managers.Skill.SpawnSkill(SKILL_NAME, Managers.Game.Player.transform.position, _dir, _stat.AttackDistance, _stat.ProjectileSpeed, _stat.Offence, Define.Skill.Launch, Managers.Game.Player.transform);

        //적공격(테스트용)
        //Managers.Skill.SpawnSkill(SKILL_NAME, Managers.Game.Enemy.transform.position, _dir, _stat.AttackDistance, _stat.ProjectileSpeed, _stat.Offence, Define.Skill.Launch, Managers.Game.Enemy.transform);

    }
}
