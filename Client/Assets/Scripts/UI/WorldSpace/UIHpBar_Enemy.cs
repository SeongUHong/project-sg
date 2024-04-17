using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHpBar_Enemy : UIBase
{

    //대상 오브젝트
    GameObject _parent;

    //대상 오브젝트 스텟
    Stat _stat;

    Text _nick;

    enum GameObjects
    {
        UIHPBar_Enemy,
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
        if (Managers.Game.IsLeft)
        {
            _stat = Managers.Game.Enemy.GetComponent<Stat>();
            _parent = Managers.Game.Enemy;
        }
        else
        {
            _stat = Managers.Game.Player_Right.GetComponent<Stat>();
            _parent = Managers.Game.Player_Right;
        }


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
        //transform.SetPositionAndRotation(_parent.position + Vector3.up * _parentHeight + _hpPos, Camera.main.transform.rotation);

        float ratio = _stat.Hp / (float)_stat.MaxHp;
        SetHpRatio(ratio);
    }

    public void SetHpRatio(float ratio)
    {
        GetGameObject((int)GameObjects.HpBar).GetComponent<Slider>().value = ratio;
    }

}
