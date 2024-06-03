using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Loading_Panel : UIBase
{
    enum Buttons
    {
        Load_Button,
    }

    enum Texts
    {
        Player_Nick,
        Enemy_Nick,
    }

    public void Hide()
    {
        transform.gameObject.SetActive(false);
    }

    public void Show()
    {
        new WaitForSeconds(Define.NEXT_DELAY_TIME);
        transform.gameObject.SetActive(true);
    }

    public void SetNickName()
    {
        GetText((int)Texts.Player_Nick).text = Managers.Game.PlayerNick;
        GetText((int)Texts.Enemy_Nick).text = Managers.Game.EnemyNick;
    }

    public void OnClick_Button()
    {
        SceneLoader.Instance.LoadScene("GameScene");

    }


    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));

        BindEvent(GetButton((int)Buttons.Load_Button).gameObject, (PointerEventData data) => OnClick_Button());

    }

    public void SetText()
    {





    }
}
