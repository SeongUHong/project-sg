using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchSkillController : MonoBehaviour
{
    private Rigidbody2D rd2d;

    //�߻��� ��ġ
    Vector2 _startPos;

    //����
    Vector2 _dir;

    //��Ÿ�
    float _distance;

    //����ü �ӵ�
    float _speed;

    //���ط�
    int _damage;

    //��� ���̾�
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

    void OnBecameInvisible() //ȭ������� ���� ������ �ʰ� �Ǹ� ȣ���� �ȴ�.
    {
        Destroy(this.gameObject); //��ü�� �����Ѵ�.
    }

    void OnTriggerEnter(Collider other)
    {
        //��� ���̾ �ƴϸ� ����
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
