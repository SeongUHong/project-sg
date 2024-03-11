using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseController : MonoBehaviour
{

    //����
    protected Vector2 _dir = Vector2.up;

    //������ٵ�
    protected Rigidbody2D _rig;

    //����
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
