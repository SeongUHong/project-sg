using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingScene : BaseScene
{

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


    public override void Clear()
    {
    }
}
