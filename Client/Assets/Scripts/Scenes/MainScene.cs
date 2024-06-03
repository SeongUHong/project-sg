using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        _sceneType = Define.Scenes.MainScene;

        //�� ������Ʈ �̸� ����
        gameObject.name = System.Enum.GetName(typeof(Define.Scenes), _sceneType);

        //�����ǳ� �ʱ�ȭ
        Managers.UI.MakePopUp<Main_Panel>();

        Time.timeScale = 1.0f;

    }

    public override void Clear()
    {
    }
}
