using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : ManagerBase
{
    //탄환정보
    GameObject _fireball;
    //탄환위치
    Vector2 _locate;
    //파이어볼id
    int _fireballid;

    //발사시 불꽃방향
    Vector3 _dir;

    //ID딕셔너리
    Dictionary<int, GameObject> _fireBalls = new Dictionary<int, GameObject>();

    public override void Init()
    {
        throw new System.NotImplementedException();
    }

    public void Start()
    {

    }

    public void Update()
    {
    }

    //인수:스킬이름, 좌표, 방향, 레이어, 스킬 타입
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

    public GameObject SkillInitiate(string skillName, int fireballid, Vector2 pos, Vector3 angle)
    {
        
        GameObject skill = Managers.Resource.Instantiate($"Effects/{skillName}", null);
        skill.name = fireballid.ToString();
        LaunchSkillController skillController = Util.GetOrAddComponent<LaunchSkillController>(skill);
        Stat _stat = Managers.Game.Player.GetComponent<Stat>();
        if (skill.layer != 9)
        {
            Managers.Game.Enemy.transform.rotation = Quaternion.Euler(0, 0, angle.z);
            _dir = Managers.Game.Enemy.transform.up;
        }
        skillController.SetSkillStatus(pos, _dir, angle ,_stat.AttackDistance, _stat.ProjectileSpeed, _stat.Offence);

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

    public void DestroyFireBall(GameObject fireball)
    {
        Destroy(fireball);
    }

    internal void SpawnSkill(string sKILL_NAME, object position, Vector2 dir, object attackDistance, float projectileSpeed, int offence)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {

    }
}
