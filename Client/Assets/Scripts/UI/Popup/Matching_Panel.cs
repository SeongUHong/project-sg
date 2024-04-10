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

    String NickName;

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
        //닉네임 서버에 보내기
        IEnumerator SendNickName()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.25f);
                C_StartMatch nick = new C_StartMatch();
                nick.nickname = Conf.Main.PLAYER_NICK;
                Managers.Network.Send(nick.Write());
            }
        }

        //로딩씬전환
        SceneLoader.Instance.LoadScene("GameScene");
    
    }
    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        

        BindEvent(GetButton((int)Buttons.Match_Button).gameObject, (PointerEventData data) => OnClick_Button());

    }

    internal object Find(string v)
    {
        throw new NotImplementedException();
    }

    public void SetText()
    {





    }
}
