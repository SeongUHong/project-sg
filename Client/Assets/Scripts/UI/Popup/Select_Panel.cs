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
        transform.gameObject.SetActive(false); // 게임이 시작되면 Select_Panel 팝업 창을 보이지 않도록 한다.
    }

    public void Show()
    {
        new WaitForSeconds(Define.RETRY_DELAY_TIME);
        transform.gameObject.SetActive(true);
    }

    public void SelectCharacter1() // '재도전' 버튼을 클릭하며 호출 되어질 함수
    {
        //플레이어 생성
        Conf.Main.ChosenShip = "Player1";
        
        Awake();

        SceneManagerEx scene = Managers.Scene;
        scene.LoadScene(Define.Scenes.GameScene);

    }
    public void SelectCharacter2()
    {
        //플레이어 생성
        Conf.Main.ChosenShip = "Player2";
        
        Awake();

        SceneManagerEx scene = Managers.Scene;
        scene.LoadScene(Define.Scenes.GameScene);
    }
}
