using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Result_Panel : UIBase
{

    enum Buttons
    {
        main_btn,
    }

    private Text result;
    public string wText = "You Win";
    public string lText = "You Lose";
    public string dText = "Draw";

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.main_btn).gameObject, (PointerEventData data) => OnClick_Main());
    }
    private void Awake()
    {
        transform.gameObject.SetActive(false); // ������ ���۵Ǹ� �˾� â�� ������ �ʵ��� �Ѵ�.
    }

    public void Show()
    {
        new WaitForSeconds(Define.NEXT_DELAY_TIME);
        transform.gameObject.SetActive(true);
    }

    public void OnClick_Main()
    {
        Awake();
        SceneManagerEx scene = Managers.Scene;
        scene.LoadScene(Define.Scenes.MainScene);

        Managers.Clear();

    }
    

    public void SetText(int status)
    {

        result = transform.Find("Panel").transform.Find("Result_Text").transform.Find("Text").GetComponent<Text>();
        if (status == (int)Conf.Main.GAMEOVER_STATUS.LOSE)
            result.text = lText;
        else if (status == (int)Conf.Main.GAMEOVER_STATUS.WIN)
            result.text = wText;
        else
            result.text = dText;

    }
}
