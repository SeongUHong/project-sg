using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : BaseScene
{
    private int Timer = 0;

    public GameObject Num_A;   //1��
    public GameObject Num_B;   //2��
    public GameObject Num_C;   //3��
    public GameObject Num_GO;

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

        Conf.Main._result = Managers.Game.Result_Panel;

    }




    public override void Clear()
    {
    }

    
}
