using UnityEngine;

public class GameScene : BaseScene
{
    //셀렉트판넬 초기화
    Select_Panel _selectPanel;
    protected override void Init()
    {
        base.Init();
        Managers.Game.Init();

        _sceneType = Define.Scenes.GameScene;

        Managers.UI.ShowSceneUI<UISceneGame>();

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
