using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingScene : BaseScene
{

    //�г��� �ǳ� �ʱ�ȭ
    NickName_Panel nickName_Panel;

    protected override void Init()
    {
        base.Init();

        _sceneType = Define.Scenes.MatchingScene;


        //�� ������Ʈ �̸� ����
        gameObject.name = System.Enum.GetName(typeof(Define.Scenes), _sceneType);

        nickName_Panel = Managers.Game.NickName_Panel;
        nickName_Panel.Show();

        Conf.Main._maching = Managers.Game.Matching_Panel;
        Conf.Main._loading = Managers.Game.Load_Panel;

    }

    private void Update()
    {
    }


    public override void Clear()
    {
    }
}
