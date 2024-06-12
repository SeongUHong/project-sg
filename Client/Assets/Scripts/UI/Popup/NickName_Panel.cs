using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NickName_Panel : UIBase
{
    enum Buttons
    {
        Nick_Button,
    }

    public InputField Nickname;
    public TextMeshProUGUI Information;

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        new WaitForSeconds(Define.NEXT_DELAY_TIME);
        gameObject.SetActive(true);
    }

    public void OnClick_Button()
    {
        Managers.Game.PlayerNick = Nickname.text;
        Matching_Panel matchingPanel = Managers.Scene.CurrentScene.GetUI<Matching_Panel>() as Matching_Panel;

        if (Managers.Game.PlayerNick == null || Managers.Game.PlayerNick == "")
        {
            Information.text = "Please Enter Nickname";
        }
        else
        {
            
            matchingPanel.SetPlayerNick(Nickname.text);

            Hide();
            matchingPanel.Show();
        }


        
    }
    public override void Init()
    {
        Bind<Button>(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.Nick_Button).gameObject, (PointerEventData data) => OnClick_Button());
    }

    public void SetText()
    {





    }
}
