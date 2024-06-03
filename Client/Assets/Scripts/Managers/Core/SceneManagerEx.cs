using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx : ManagerBase
{
    //SceneManager��� API�� �̹� �����ϱ� ������
    //SceneManagerEx��� �̸����� ����


    public BaseScene CurrentScene
    {
        get
        {
            //������ Ÿ��(BaseScene)�� ����ִ� ������Ʈ�� ã����
            return GameObject.FindObjectOfType<BaseScene>();
            //BaseScene baseScene = null;
            //try
            //{
            //    baseScene = GameObject.FindObjectOfType<BaseScene>();
            //    if (baseScene == null)
            //    {
            //        Debug.LogError("BaseScene not found in the scene.");
            //    }
            //    else
            //    {
            //        Debug.Log("BaseScene found in the scene.");
            //    }

            //}
            //catch (System.Exception ex)
            //{
            //    Debug.LogError("An exception occurred: " + ex.Message);
            //}
            //return baseScene;
        }
    }

    public void LoadScene(Define.Scenes type)
    {
        Managers.Clear();
        SceneManager.LoadScene(GetSceneName(type));
    }

    string GetSceneName(Define.Scenes type)
    {
        string name = System.Enum.GetName(typeof(Define.Scenes), type);
        return name;
    }


    public override void Init()
    {
        throw new System.NotImplementedException();
    }
    public void Clear()
    {
        CurrentScene.Clear();
    }
}
