using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectScene : BaseScene
{
    //����Ʈ�ǳ� �ʱ�ȭ
    Select_Panel _selectPanel;
    protected override void Init()
    {
        base.Init();

        _sceneType = Define.Scenes.SelectScene;


        //�� ������Ʈ �̸� ����
        gameObject.name = System.Enum.GetName(typeof(Define.Scenes), _sceneType);

        _selectPanel = Managers.Game.Select_Panel;

        if (Managers.Game.Player == null)
            _selectPanel.Show();

    }

    public override void Clear()
    {
    }
}
