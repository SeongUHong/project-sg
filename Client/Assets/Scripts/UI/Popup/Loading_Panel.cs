using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Loading_Panel : UIBase
{
    enum Buttons
    {
        Load_Button,
    }

    Text _playerNick;
    Text _enemyNick;


    private void Awake()
    {
        transform.gameObject.SetActive(false); // ������ ���۵Ǹ� �˾� â�� ������ �ʵ��� �Ѵ�.
    }

    public void Show()
    {
        new WaitForSeconds(Define.NEXT_DELAY_TIME);
        transform.gameObject.SetActive(true);


        _playerNick = Conf.Main._loading.transform.Find("Player_Nick").GetComponent<Text>();
        _enemyNick = Conf.Main._loading.transform.Find("Enemy_Nick").GetComponent<Text>();
        _playerNick.text = Managers.Game.PlayerNick;
        _enemyNick.text = Managers.Game.EnemyNick;
    }

    public void OnClick_Button()
    {
        SceneLoader.Instance.LoadScene("GameScene");

    }


    public override void Init()
    {
        Bind<Button>(typeof(Buttons));


        BindEvent(GetButton((int)Buttons.Load_Button).gameObject, (PointerEventData data) => OnClick_Button());

    }

    internal object Find(string v)
    {
        throw new NotImplementedException();
    }

    public void SetText()
    {





    }
}
