using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHpBar : UIBase
{

    //��� ������Ʈ
    GameObject _parent;

    //��� ������Ʈ ����
    Stat _stat;

    Text _nick;

    enum GameObjects
    {
        UIHPBar,
        HpBar,
        NickName,

    }

    enum Images
    {
        Fill,
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));
        if (Conf.Main.IS_LEFT)
        {
            _stat = Managers.Game.Player.GetComponent<Stat>();
            _parent = Managers.Game.Player;
        }
        else
        {
            _stat = Managers.Game.Enemy_Left.GetComponent<Stat>();
            _parent = Managers.Game.Enemy_Left;
        }

        GameObject go = GetImage((int)Images.Fill).gameObject;

        //hp�� ����
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
