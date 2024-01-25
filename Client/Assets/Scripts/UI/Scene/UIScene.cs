using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIScene : UIBase
{
    
    //���ݹ�ư �ڵ鷯
    public Action OnAttackBtnDownHandler = null;

    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, false);
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
