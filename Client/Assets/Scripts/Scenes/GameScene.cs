using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : BaseScene
{

    public TextMeshProUGUI CountDown;
    CountDown_Panel countPanel;
    int count;

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
        Conf.Main._inGameNick = Managers.Game.InGame_NickName_Panel;
        Conf.Main._inGameNick.SetNickName();
        Conf.Main._inGameNick.Show();

        Managers.Game.IsPause = false;


        //카운트 판넬 초기화
        CountDown_Panel countPanel = Managers.UI.MakePopUp<CountDown_Panel>();
        countPanel.Show();
        AddUI(countPanel);

    }

    private void Update()
    {

        if (Managers.Game.IsPause)
        {  
            Time.timeScale = 1;
            return;
        }
        else
        {
            Time.timeScale = 0;
            return;
        }
    }


    public override void Clear()
    {
    }

    
}
