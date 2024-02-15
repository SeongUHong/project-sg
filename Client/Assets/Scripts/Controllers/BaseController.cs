using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{

    //방향
    protected Vector2 _dir = Vector2.up;

    //리지드바디
    protected Rigidbody2D _rig;

    //스텟
    protected Stat _stat;



    private void Start()
    {

        Init();
    }

    public abstract void Init();


    protected virtual void UpdateDie()
    {

        StartCoroutine(Despwn());

    }


    //사망시 일정시간 후 비활성화
    protected virtual IEnumerator Despwn()
    {
        yield return new WaitForSeconds(Define.DESPAWN_DELAY_TIME);

    }

    protected virtual void OnEnable()
    {

    }
}
