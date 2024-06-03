using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        _sceneType = Define.Scenes.MatchingScene;

        //씬 오브젝트 이름 변경
        gameObject.name = System.Enum.GetName(typeof(Define.Scenes), _sceneType);

        //닉네임 판넬 초기화
        NickName_Panel nickNamePanel = Managers.UI.MakePopUp<NickName_Panel>();
        nickNamePanel.Show();
        AddUI(nickNamePanel);

        //매칭 판넬 초기화
        Matching_Panel matchingPanel = Managers.UI.MakePopUp<Matching_Panel>();
        matchingPanel.Hide();
        AddUI(matchingPanel);

        //로딩 판넬 초기화
        Loading_Panel loadingPanel = Managers.UI.MakePopUp<Loading_Panel>();
        loadingPanel.Hide();
        AddUI(loadingPanel);

        Debug.Log("MatchingScene");
    }

    public override void Clear()
    {
    }
}
