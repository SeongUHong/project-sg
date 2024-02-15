using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchSkillController : MonoBehaviour
{
    private Rigidbody2D rd2d;

    //발사한 위치
    Vector2 _startPos;

    //방향
    Vector2 _dir;

    //사거리
    float _distance;

    //투사체 속도
    float _speed;

    //피해량
    int _damage;

    //대상 레이어
    int[] _layers;


    public void SetSkillStatus(Vector2 startPos, Vector2 dir, float distance, float speed, int damage)
    {
        _startPos = startPos;
        _dir = dir;
        _distance = distance;
        _speed = speed;
        _damage = damage;
    }

    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        rd2d.velocity = rd2d.transform.up * _speed * Time.fixedDeltaTime;

        rd2d.AddForce(rd2d.transform.up * _speed * Time.fixedDeltaTime);

        rd2d.MovePosition(rd2d.position + _dir * Time.fixedDeltaTime * 2);

    }

    void OnBecameInvisible() //화면밖으로 나가 보이지 않게 되면 호출이 된다.
    {
        Destroy(this.gameObject); //객체를 삭제한다.
    }

    void OnTriggerEnter(Collider other)
    {
        //대상 레이어가 아니면 리턴
        int targetLayer = other.gameObject.layer;
        bool canAttack = false;
        foreach (int layer in _layers)
        {
            if (targetLayer == layer)
            {
                canAttack = true;
                break;
            }
        }
        if (canAttack == false) return;

        if (other.gameObject.GetComponent<Stat>().OnAttacked(_damage))
        {
            Cleer();

            Managers.Resource.Destroy(gameObject);
        }
    }

    void Cleer()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
