using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackGague_Player : UIBase
{ 
    //대상 오브젝트
    GameObject _parent;

    //대상 오브젝트 스텟
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
        _stat = Managers.Game.Player.GetComponent<Stat>();

        _parent = Managers.Game.Player;

        GameObject gague = GetImage((int)Images.Gague_Fill).gameObject;

        //hp색 변경
        if (_parent.gameObject.layer == (int)Define.Layer.Player)
        {

            GetImage((int)Images.Gague_Fill).GetComponent<Image>().color = new Color(117 / 255f, 1, 84 / 255f);

        }
        else if (_parent.gameObject.layer == (int)Define.Layer.Enemy)
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
