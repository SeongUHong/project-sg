using UnityEngine;

public class GameScene : BaseScene
{
    //����Ʈ�ǳ� �ʱ�ȭ
    Select_Panel _selectPanel;
    protected override void Init()
    {
        base.Init();
        Managers.Game.Init();

        _sceneType = Define.Scenes.GameScene;

        Managers.UI.ShowSceneUI<UISceneGame>();

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
