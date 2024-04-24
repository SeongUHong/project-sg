using System.Collections;
using System.Collections.Generic;
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
    Text _playerNick;

    private void Awake()
    {
        transform.gameObject.SetActive(false); // ������ ���۵Ǹ� �˾� â�� ������ �ʵ��� �Ѵ�.
    }

    public void Show()
    {
        new WaitForSeconds(Define.NEXT_DELAY_TIME);
        transform.gameObject.SetActive(true);
    }

    public void OnClick_Button()
    {
        Managers.Game.PlayerNick = Nickname.text;


        _playerNick = Conf.Main._maching.transform.Find("Player_Nick").GetComponent<Text>();
        _playerNick.text = Managers.Game.PlayerNick;

        Awake();
        Conf.Main._maching.Show();
    }
    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        //Bind<Text>(typeof(Text));
        //Awake();

        BindEvent(GetButton((int)Buttons.Nick_Button).gameObject, (PointerEventData data) => OnClick_Button());

    }

    public void SetText()
    {

        



    }
}
