using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        _sceneType = Define.Scenes.MatchingScene;

        //�� ������Ʈ �̸� ����
        gameObject.name = System.Enum.GetName(typeof(Define.Scenes), _sceneType);

        //�г��� �ǳ� �ʱ�ȭ
        NickName_Panel nickNamePanel = Managers.UI.MakePopUp<NickName_Panel>();
        nickNamePanel.Show();
        AddUI(nickNamePanel);

        //��Ī �ǳ� �ʱ�ȭ
        Matching_Panel matchingPanel = Managers.UI.MakePopUp<Matching_Panel>();
        matchingPanel.Hide();
        AddUI(matchingPanel);

        //�ε� �ǳ� �ʱ�ȭ
        Loading_Panel loadingPanel = Managers.UI.MakePopUp<Loading_Panel>();
        loadingPanel.Hide();
        AddUI(loadingPanel);

        Debug.Log("MatchingScene");
    }

    public override void Clear()
    {
    }
}
