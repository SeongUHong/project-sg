using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISceneGame : UIScene
{
    enum Buttons
    {
        AttackBtn,
    }

    enum Images
    {
        JoyStick,
        JoyStickTouchArea,
        OuterPad,
        InnnerPad,
    }

   

    public override void Init()
    {

        //���̽�ƽ�� �ڵ鷯 �߰�
        Bind<Image>(typeof(Images));
        BindJoyStickEvent( GetImage((int)Images.JoyStickTouchArea).gameObject);

        //�⺻���� ��ư
        Bind<Button>(typeof(Buttons));
        BindEvent(GetButton((int)Buttons.AttackBtn).gameObject, (PointerEventData data) => AttackEvent(data));
    }

    
        
    
}
