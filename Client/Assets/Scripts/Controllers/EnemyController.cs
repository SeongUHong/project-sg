using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController
{
    public override void Init()
    {


        //스텟 초기화
        _stat = gameObject.GetComponent<Stat>();
        if (_stat == null)
        {
            Debug.Log("Can't Load Stat Component");
        }
        _stat.SetStat(Managers.Data.GetStatByLevel("EnemyStat", 1));

        //HP바 추가
        if (gameObject.GetComponentInChildren<UIHpBar_Enemy>() == null)
        {
            Managers.UI.MakeWorldUI_Enemy<UIHpBar_Enemy>(transform);
        }   
    }
}
