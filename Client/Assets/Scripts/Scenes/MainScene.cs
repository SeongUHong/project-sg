using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        _sceneType = Define.Scenes.MainScene;

        //씬 오브젝트 이름 변경
        gameObject.name = System.Enum.GetName(typeof(Define.Scenes), _sceneType);

        //메인판넬 초기화
        Managers.UI.MakePopUp<Main_Panel>();

        Time.timeScale = 1.0f;

        Screen.SetResolution(1600, 900, true);

    }

    public override void Clear()
    {
    }
}
