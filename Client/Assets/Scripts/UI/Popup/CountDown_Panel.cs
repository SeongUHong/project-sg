using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountDown_Panel : UIBase
{
    public Text CountDown;
    int remainSec;
    private void Awake()
    {
        transform.gameObject.SetActive(false); 
    }

    public void Show()
    {
        transform.gameObject.SetActive(true);
    }

    public void Update()
    {
        remainSec = Managers.Game.RemainSec;
        CountDown.text = remainSec.ToString();
    }

    public override void Init()
    {
        remainSec = Managers.Game.RemainSec;
        CountDown.text = remainSec.ToString();

    }
}
