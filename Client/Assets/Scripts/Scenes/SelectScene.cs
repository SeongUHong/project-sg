using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectScene : BaseScene
{
    //셀렉트판넬 초기화
    Select_Panel _selectPanel;
    protected override void Init()
    {
        base.Init();

        _sceneType = Define.Scenes.SelectScene;


        //씬 오브젝트 이름 변경
        gameObject.name = System.Enum.GetName(typeof(Define.Scenes), _sceneType);

        _selectPanel = Managers.Game.Select_Panel;

        if (Managers.Game.Player == null)
            _selectPanel.Show();

    }

    public override void Clear()
    {
    }
}
