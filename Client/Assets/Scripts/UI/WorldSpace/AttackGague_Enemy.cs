using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackGague_Enemy : UIBase
{
    //��� ������Ʈ
    GameObject _parent;

    //��� ������Ʈ ����
    Stat _stat;


    enum GameObjects
    {
        UIHPBar,
        AttackGague,

    }

    enum Images
    {
        Gague_Fill,
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));
        _stat = Managers.Game.Enemy.GetComponent<Stat>();

        _parent = Managers.Game.Enemy;

        GameObject gague = GetImage((int)Images.Gague_Fill).gameObject;

        //hp�� ����
        if  (_parent.gameObject.layer == (int)Define.Layer.Enemy)
        {

            GetImage((int)Images.Gague_Fill).GetComponent<Image>().color = new Color(84 / 255f, 153 / 255f, 1);

        }
    }

    private void Update()
    {

        float ratio = _stat.AttackGague / (float)_stat.MaxAttackGague;
        SetGagueRatio(ratio);

        _stat.AttackGagueUp();


    }
    public void SetGagueRatio(float ratio)
    {
        GetGameObject((int)GameObjects.AttackGague).GetComponent<Slider>().value = ratio;
    }
}