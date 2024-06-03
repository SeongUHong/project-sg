using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public Define.Scenes _sceneType { get; protected set; } = Define.Scenes.Unknown;
    Dictionary<Type, UIBase> _uis = new Dictionary<Type, UIBase>();

    void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        UnityEngine.Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
        {
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
        }
    }

    public void AddUI(UIBase ui)
    {
        _uis.Add(ui.GetType(), ui);
    }

    public UIBase GetUI<T>() where T : UIBase
    {
        UIBase ui = null;
        if (!_uis.TryGetValue(typeof(T), out ui))
        {
            // 존재하지 않는 UI일 경우 에러
            Debug.LogError($"Failed to get UI : {typeof(T).Name}");
            throw new System.Exception();
        }

        return ui;
    }

    public abstract void Clear();
}
