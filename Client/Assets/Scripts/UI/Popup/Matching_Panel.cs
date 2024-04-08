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
        //매치

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
