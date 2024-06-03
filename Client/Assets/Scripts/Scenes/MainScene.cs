using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    //�����ǳ� �ʱ�ȭ
    Main_Panel _main_Panel;

    protected override void Init()
    {
        base.Init();

        _sceneType = Define.Scenes.MainScene;


        //�� ������Ʈ �̸� ����
        gameObject.name = System.Enum.GetName(typeof(Define.Scenes), _sceneType);

        _main_Panel = Managers.Game.Main_Panel;

        _main_Panel.Show();

        Time.timeScale = 1.0f;

    }

    public override void Clear()
    {
    }
}
