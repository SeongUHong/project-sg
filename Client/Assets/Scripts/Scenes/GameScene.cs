using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : BaseScene
{

    public TextMeshProUGUI CountDown;
    Pause_Panel pausePanel;

    protected override void Init()
    {
        base.Init();
        Managers.Game.Init();

        _sceneType = Define.Scenes.GameScene;

        Managers.UI.ShowSceneUI<UISceneGame>();

        //씬 오브젝트 이름 변경
        gameObject.name = System.Enum.GetName(typeof(Define.Scenes), _sceneType);

        
        GameObject enemy = Managers.Game.InstantiateEnemy();
        GameObject player = Managers.Game.InstantiatePlayer();



        Conf.Main._inGameNick = Managers.Game.InGame_NickName_Panel;
        Conf.Main._inGameNick.SetNickName();
        Conf.Main._inGameNick.Show();

        Managers.Game.IsPause = false;


        //카운트 판넬 초기화
        CountDown_Panel countPanel = Managers.UI.MakePopUp<CountDown_Panel>();
        countPanel.Show();
        AddUI(countPanel);

        //포즈판넬 초기화
        pausePanel = Managers.Game.Pause_Panel;
        

    }

    private void Update()
    {

        if (Managers.Game.IsPause)
        {
            pausePanel.Awake();
            Time.timeScale = 1;
            return;
        }
        else
        {
            pausePanel.Show();
            Time.timeScale = 0;
            return;
        }
    }


    public override void Clear()
    {
    }

    
}
