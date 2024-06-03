using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    //메인판넬 초기화
    Main_Panel _main_Panel;

    protected override void Init()
    {
        base.Init();

        _sceneType = Define.Scenes.MainScene;


        //씬 오브젝트 이름 변경
        gameObject.name = System.Enum.GetName(typeof(Define.Scenes), _sceneType);

        _main_Panel = Managers.Game.Main_Panel;

        _main_Panel.Show();

        Time.timeScale = 1.0f;

    }

    public override void Clear()
    {
    }
}
