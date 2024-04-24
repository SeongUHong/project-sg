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

    private void Awake()
    {
        transform.gameObject.SetActive(false); // ������ ���۵Ǹ� GameOver �˾� â�� ������ �ʵ��� �Ѵ�.
    }

    public void Show()
    {
        new WaitForSeconds(Define.RETRY_DELAY_TIME);
        transform.gameObject.SetActive(true);
    }

    public void OnClick_Start() // 'Start' ��ư�� Ŭ���ϸ� ȣ�� �Ǿ��� �Լ�
    {

        Managers.Network.Init();

        SceneManagerEx scene = Managers.Scene;
        scene.LoadScene(Define.Scenes.MatchingScene); // SceneManager�� LoadScene �Լ��� ����Ͽ�! ���� �� 'MatchingScene'�� �ٽ� �ҷ������� ��Ų��.
                                                      // ���� ���� �ٽ� �ҷ����� ������ ����� �ȴ�.




    }

   
}
