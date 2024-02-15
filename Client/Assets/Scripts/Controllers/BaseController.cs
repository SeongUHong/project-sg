using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{

    //����
    protected Vector2 _dir = Vector2.up;

    //������ٵ�
    protected Rigidbody2D _rig;

    //����
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


    //����� �����ð� �� ��Ȱ��ȭ
    protected virtual IEnumerator Despwn()
    {
        yield return new WaitForSeconds(Define.DESPAWN_DELAY_TIME);

    }

    protected virtual void OnEnable()
    {

    }
}
