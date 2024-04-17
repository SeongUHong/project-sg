using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGame_NickName_Panel : UIBase
{
    Text _Lnick;
    Text _Rnick;

    private void Awake()
    {
        transform.gameObject.SetActive(false); // ������ ���۵Ǹ� �˾� â�� ������ �ʵ��� �Ѵ�.
    }

    public void Show()
    {
        new WaitForSeconds(Define.NEXT_DELAY_TIME);
        transform.gameObject.SetActive(true);
    }


    public override void Init()
    {
        
    }

    public void SetNickName()
    {
        _Lnick = Conf.Main._inGameNick.transform.Find("Panel").transform.Find("NickName_Left").transform.Find("Text").GetComponent<Text>();
        _Rnick = Conf.Main._inGameNick.transform.Find("Panel").transform.Find("NickName_Right").transform.Find("Text").GetComponent<Text>();
        if (Managers.Game.IsLeft)
        {
            _Lnick.text = Managers.Game.PlayerNick;
            _Rnick.text = Managers.Game.EnemyNick;

        }
        else
        {
            _Rnick.text = Managers.Game.PlayerNick;
            _Lnick.text = Managers.Game.EnemyNick;
        }
    }
}
