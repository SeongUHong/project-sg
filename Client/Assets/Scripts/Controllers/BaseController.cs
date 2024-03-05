using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseController : MonoBehaviour
{

    //방향
    protected Vector2 _dir = Vector2.up;

    //리지드바디
    protected Rigidbody2D _rig;

    //스텟
    //protected Stat _stat;

    private void Start()
    {

        Init();
        
    }

    public abstract void Init();


    protected virtual void UpdateDie()
    {


    }


}
