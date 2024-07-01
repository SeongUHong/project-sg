using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : BaseScene
{
   
    protected override void Init()
    {
        base.Init();


        Screen.SetResolution(1920, 1080, true);
    }


    public override void Clear()
    {

    }

}
