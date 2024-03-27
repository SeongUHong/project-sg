using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingScene : BaseScene
{
    public bool flag = Conf.Main.LOADING_FLAG;
    protected override void Init()
    {
        base.Init();

        _sceneType = Define.Scenes.MatchingScene;


        //씬 오브젝트 이름 변경
        gameObject.name = System.Enum.GetName(typeof(Define.Scenes), _sceneType);

        
        

    }
    public void Update()
    {
        //서버에서 준비완료가 되면 게임씬으로 이동
        //if(서버에서 받는조건)
        //    flag = true;
        SceneManagerEx scene = Managers.Scene;
        if (flag)
            scene.LoadScene(Define.Scenes.GameScene);
    }


    public override void Clear()
    {
    }
}
