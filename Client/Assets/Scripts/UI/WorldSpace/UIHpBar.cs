using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHpBar : UIBase
{

    //대상 오브젝트
    GameObject _parent;

    //대상 오브젝트 스텟
    Stat _stat;


    enum GameObjects
    {
        UIHPBar,
        HpBar,
        
    }

    enum Images
    {
        Fill,
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));
        _stat = Managers.Game.Player.GetComponent<Stat>();

        _parent = Managers.Game.Player;

        GameObject go = GetImage((int)Images.Fill).gameObject;

        //hp색 변경
        if (_parent.gameObject.layer == (int)Define.Layer.Player)
        {

            GetImage((int)Images.Fill).GetComponent<Image>().color = new Color(117 / 255f, 1, 84 / 255f);

        }
        else if (_parent.gameObject.layer == (int)Define.Layer.Enemy)
        {

            GetImage((int)Images.Fill).GetComponent<Image>().color = new Color(84 / 255f, 153 / 255f, 1);

        }
    }

    private void Update()
    {

        float ratio = _stat.Hp / (float)_stat.MaxHp;
        SetHpRatio(ratio);
    }

    public void SetHpRatio(float ratio)
    {
        GetGameObject((int)GameObjects.HpBar).GetComponent<Slider>().value = ratio;
    }

}
