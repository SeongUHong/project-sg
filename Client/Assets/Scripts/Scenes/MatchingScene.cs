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


        //�� ������Ʈ �̸� ����
        gameObject.name = System.Enum.GetName(typeof(Define.Scenes), _sceneType);

        
        

    }
    public void Update()
    {
        //�������� �غ�Ϸᰡ �Ǹ� ���Ӿ����� �̵�
        //if(�������� �޴�����)
        //    flag = true;
        SceneManagerEx scene = Managers.Scene;
        if (flag)
            scene.LoadScene(Define.Scenes.GameScene);
    }


    public override void Clear()
    {
    }
}
