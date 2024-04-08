using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingScene : BaseScene
{
    public bool flag = Conf.Main.LOADING_FLAG;

    //닉네임 판넬 초기화
    NickName_Panel nickName_Panel;


    protected override void Init()
    {
        base.Init();

        _sceneType = Define.Scenes.MatchingScene;


        //씬 오브젝트 이름 변경
        gameObject.name = System.Enum.GetName(typeof(Define.Scenes), _sceneType);

        nickName_Panel = Managers.Game.NickName_Panel;
        nickName_Panel.Show();

        Conf.Main._maching = Managers.Game.Matching_Panel;


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
