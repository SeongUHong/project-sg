using System;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        Managers.Game.Init();

        _sceneType = Define.Scenes.GameScene;

        Managers.UI.ShowSceneUI<UISceneGame>();

        //�� ������Ʈ �̸� ����
        gameObject.name = System.Enum.GetName(typeof(Define.Scenes), _sceneType);


        GameObject player = Managers.Game.InstantiatePlayer();
        GameObject enemy = Managers.Game.InstantiateEnemy();

        
        

    }

    public override void Clear()
    {
    }
}
