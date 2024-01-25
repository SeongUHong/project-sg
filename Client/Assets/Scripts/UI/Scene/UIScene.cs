using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIScene : UIBase
{
    
    //공격버튼 핸들러
    public Action OnAttackBtnDownHandler = null;

    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, false);
    }



    //공격버튼
    protected void AttackEvent(PointerEventData data)
    {
        if (OnAttackBtnDownHandler != null)
        {
            OnAttackBtnDownHandler.Invoke();
        }
    }

}
