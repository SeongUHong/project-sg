using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : BaseScene
{

    protected override void Init()
    {
        base.Init();
        Managers.Game.Init();

        _sceneType = Define.Scenes.GameScene;

        Managers.UI.ShowSceneUI<UISceneGame>();

        //씬 오브젝트 이름 변경
        gameObject.name = System.Enum.GetName(typeof(Define.Scenes), _sceneType);


        GameObject player = Managers.Game.InstantiatePlayer();
        GameObject enemy = Managers.Game.InstantiateEnemy();

        Conf.Main._result = Managers.Game.Result_Panel;

    }




    public override void Clear()
    {
    }

    
}
