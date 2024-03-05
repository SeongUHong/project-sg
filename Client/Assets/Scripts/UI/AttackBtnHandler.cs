using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBtnHandler : MonoBehaviour
{
    //����
    protected Vector2 _dir = Vector2.up;
    //����
    protected Stat _stat;
    //���� �̸�
    string SKILL_NAME = "fireballredbig";

    //�� �����̸�(�׽�Ʈ��)
    //string SKILL_NAME = "fireballbluebig";

    public void OnAttack()
    {
        Managers.Skill.SpawnSkill(SKILL_NAME, Managers.Game.Player.transform.position, _dir, _stat.AttackDistance, _stat.ProjectileSpeed, _stat.Offence, Define.Skill.Launch, Managers.Game.Player.transform);

        //������(�׽�Ʈ��)
        //Managers.Skill.SpawnSkill(SKILL_NAME, Managers.Game.Enemy.transform.position, _dir, _stat.AttackDistance, _stat.ProjectileSpeed, _stat.Offence, Define.Skill.Launch, Managers.Game.Enemy.transform);

    }
}
