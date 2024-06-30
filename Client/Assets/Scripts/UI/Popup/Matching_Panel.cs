using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Matching_Panel : UIBase
{
    enum Buttons
    {
        Match_Button,
    }
    enum Texts
    {
        Player_Nick,
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

    public void OnClick_Button()
    {
        SendNickName();
    }

    //닉네임 서버에 보내기
    public void SendNickName()
    {
        if (Managers.Network.IsConnet)
        {
            C_StartMatch nick = new C_StartMatch();
            nick.nickname = Managers.Game.PlayerNick;
            Managers.Network.Send(nick.Write());
            Debug.Log("SendNickName");
        }
    }



    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));

        BindEvent(GetButton((int)Buttons.Match_Button).gameObject, (PointerEventData data) => OnClick_Button());
    }

    public void SetPlayerNick(string nickName)
    {
        GetText((int)Texts.Player_Nick).text = nickName;
    }
}
