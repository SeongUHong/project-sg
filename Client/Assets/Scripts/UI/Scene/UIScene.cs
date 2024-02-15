using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIScene : UIBase
{
    //���̽�ƽ �ڵ鷯
    JoyStickHandler _joyStickHandler;

    public JoyStickHandler JoyStickHandler { get { return _joyStickHandler; } }

    //���ݹ�ư �ڵ鷯
    public Action OnAttackBtnDownHandler = null;

    AttackBtnHandler _attackBtnHandler;
    public AttackBtnHandler AttackBtnHandler { get { return _attackBtnHandler; } }
    

    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, false);
    }


    //���̽�ƽ, �ڵ��� �μ��� ����
    //���̽�ƽ�� �ڵ鷯 ������Ʈ ����
    public void BindJoyStickEvent(GameObject touchArea)
    {
        JoyStickHandler joyStickHandler = touchArea.AddComponent<JoyStickHandler>();
        _joyStickHandler = joyStickHandler;

    }
  

    //���ݹ�ư
    protected void AttackEvent(PointerEventData data)
    {
        if (OnAttackBtnDownHandler != null)
        {
            OnAttackBtnDownHandler.Invoke();

        }
    }

}
