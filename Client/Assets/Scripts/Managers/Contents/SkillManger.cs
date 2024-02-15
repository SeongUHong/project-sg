using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : ManagerBase
{

    public override void Init()
    {
        throw new System.NotImplementedException();
    }

    //인수:스킬이름, 좌표, 방향, 레이어, 스킬 타입
    public GameObject SpawnSkill(
        string skillName, Vector2 pos, Vector2 dir, float distance, float speed, int damage,
        Define.Skill skillType = Define.Skill.Launch, Transform parent = null)
    {
        

        GameObject skill = Managers.Resource.Instantiate($"Effects/{skillName}", null);

        if (skill == null)
        {
            Debug.Log($"Not Exist Skill:{skillName}");
            return null;
        }


        skill.transform.position = pos;

        switch (skillType)
        {
            case Define.Skill.Launch:
                LaunchSkillController skillController = Util.GetOrAddComponent<LaunchSkillController>(skill);
                skillController.SetSkillStatus(pos, dir, distance, speed, damage);
                break;
        }

        return skill;
    }
    internal void SpawnSkill(string sKILL_NAME, object position, Vector2 dir, object attackDistance, float projectileSpeed, int offence)
    {
        throw new NotImplementedException();
    }
}
