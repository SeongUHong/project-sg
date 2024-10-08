using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Main_Panel : UIBase
{

    enum Buttons
    {
        Start_Button,
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        BindEvent(GetButton((int)Buttons.Start_Button).gameObject, (PointerEventData data) => OnClick_Start());
    }

    public void Hide()
    {
        transform.gameObject.SetActive(false);
    }

    public void Show()
    {
        new WaitForSeconds(Define.RETRY_DELAY_TIME);
        transform.gameObject.SetActive(true);
    }

    public void OnClick_Start() // 'Start' 버튼을 클릭하며 호출 되어질 함수
    {

        Managers.Network.Init();

        SceneManagerEx scene = Managers.Scene;
        scene.LoadScene(Define.Scenes.MatchingScene); // SceneManager의 LoadScene 함수를 사용하여! 현재 신 'MatchingScene'을 다시 불러오도록 시킨다.
                                                      // 같은 신을 다시 불러오면 게임이 재시작 된다.




    }

   
}
