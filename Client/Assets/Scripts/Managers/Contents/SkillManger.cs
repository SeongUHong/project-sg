using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : ManagerBase
{
    //�÷��̾� ��ġ
    GameObject _player;
    //źȯ��ġ
    Vector2 _locate;
    //���̾id
    int _fireballid;

    //�߻�� �Ҳɹ���
    Vector2 _dir;

    //ID��ųʸ�
    Dictionary<int, GameObject> _fireBalls = new Dictionary<int, GameObject>();

    public override void Init()
    {
        throw new System.NotImplementedException();
    }

    public void Start()
    {
        _player = Managers.Game.Player;
    }

    //�μ�:��ų�̸�, ��ǥ, ����, ���̾�, ��ų Ÿ��
    public GameObject SpawnSkill(
        string skillName, Vector2 pos, Vector2 dir, float distance, float speed, int damage,
        Define.Skill skillType = Define.Skill.Launch, Transform parent = null)
    {
        _dir = dir;

        /*GameObject skill = SkillInitiate(skillName);

        skill.transform.position = pos;
        _locate = pos;

        switch (skillType)
        {
            case Define.Skill.Launch:
                LaunchSkillController skillController = Util.GetOrAddComponent<LaunchSkillController>(skill);
                skillController.SetSkillStatus(pos, dir, distance, speed, damage);
                break;
        }*/

        return null;
    }

    public void EnemyShot()
    {
        EnemyController enemyController = Managers.Game.Enemy.GetComponent<EnemyController>();
        enemyController.OnAttack();
    }

    public GameObject SkillInitiate(string skillName, int fireballid, Vector2 pos)
    {
        
        GameObject skill = Managers.Resource.Instantiate($"Effects/{skillName}", null);
        skill.name = fireballid.ToString();
        LaunchSkillController skillController = Util.GetOrAddComponent<LaunchSkillController>(skill);
        Stat _stat = Managers.Game.Player.GetComponent<Stat>();
        skillController.SetSkillStatus(pos, _dir, _stat.AttackDistance, _stat.ProjectileSpeed, _stat.Offence);

        Debug.Log($"skillName : {skillName}");
        Debug.Log($"pos : {pos.x},{pos.y}");

        skill.transform.position = pos;

        if (skill == null)
        {
            Debug.Log($"Not Exist Skill:{skillName}");
            return null;
        }
        

        return skill;
    }

    public void SetFireBallID(int fireballid)
    {
        _fireballid = fireballid;

    }
    public int GetFireBallID()
    {
        return _fireballid;
    }
    public void AddFireBall(GameObject fireball,int fireballid)
    {
        _fireBalls.Add(fireballid, fireball);
    }

    public GameObject GetFireBall(int fireballid)
    {

        GameObject fireball;

        if (!_fireBalls.TryGetValue(fireballid, out fireball))
        {
            return null;
        }

        return fireball;    

    }

    public void DeleteFireBall(int fireballid)
    {
        _fireBalls.Remove(fireballid);
    }

    internal void SpawnSkill(string sKILL_NAME, object position, Vector2 dir, object attackDistance, float projectileSpeed, int offence)
    {
        throw new NotImplementedException();
    }
}
