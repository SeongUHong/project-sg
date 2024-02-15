using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIScene : UIBase
{
    //조이스틱 핸들러
    JoyStickHandler _joyStickHandler;

    public JoyStickHandler JoyStickHandler { get { return _joyStickHandler; } }

    //공격버튼 핸들러
    public Action OnAttackBtnDownHandler = null;

    AttackBtnHandler _attackBtnHandler;
    public AttackBtnHandler AttackBtnHandler { get { return _attackBtnHandler; } }
    

    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, false);
    }


    //조이스틱, 핸들을 인수로 받음
    //조이스틱에 핸들러 컴포넌트 적용
    public void BindJoyStickEvent(GameObject touchArea)
    {
        JoyStickHandler joyStickHandler = touchArea.AddComponent<JoyStickHandler>();
        _joyStickHandler = joyStickHandler;

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
