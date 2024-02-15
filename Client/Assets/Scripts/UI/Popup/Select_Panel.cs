using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Select_Panel : UIBase
{
    enum Buttons
    {
        Ship_1,
        Ship_2,

    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        BindEvent(GetButton((int)Buttons.Ship_1).gameObject, (PointerEventData data) => SelectCharacter1());
        BindEvent(GetButton((int)Buttons.Ship_2).gameObject, (PointerEventData data) => SelectCharacter2());
    }

    private void Awake()
    {
        transform.gameObject.SetActive(false); // ������ ���۵Ǹ� Select_Panel �˾� â�� ������ �ʵ��� �Ѵ�.
    }

    public void Show()
    {
        new WaitForSeconds(Define.RETRY_DELAY_TIME);
        transform.gameObject.SetActive(true);
    }

    public void SelectCharacter1() // '�絵��' ��ư�� Ŭ���ϸ� ȣ�� �Ǿ��� �Լ�
    {
        //�÷��̾� ����
        Conf.Main.ChosenShip = "Player1";
        
        Awake();

        SceneManagerEx scene = Managers.Scene;
        scene.LoadScene(Define.Scenes.GameScene);

    }
    public void SelectCharacter2()
    {
        //�÷��̾� ����
        Conf.Main.ChosenShip = "Player2";
        
        Awake();

        SceneManagerEx scene = Managers.Scene;
        scene.LoadScene(Define.Scenes.GameScene);
    }
}
