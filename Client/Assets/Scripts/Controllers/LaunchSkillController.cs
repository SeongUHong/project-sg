using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchSkillController : MonoBehaviour
{
    private Rigidbody2D rd2d;

    //탄환 현재 위치
    Vector3 _locate;

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

        //서버 전송용 탄환위치
        _locate = rd2d.position;
        //서버 전송용 탄환ID
        //this.name;
    }

    void OnBecameInvisible() //화면밖으로 나가 보이지 않게 되면 호출이 된다.
    {
        if (this.gameObject.layer == 9)
        {

            Destroy(this.gameObject); //객체를 삭제한다
            //번호로 저장된 탄환이름을 리스트에서 삭제
            Conf.Main.PLAYER_ID_LIST.Remove(this.gameObject.name);
            Debug.Log($"Removed Player Bullet : {this.name}");

        }
        else if (this.gameObject.layer == 10)
        {
            Destroy(this.gameObject); //객체를 삭제한다
            //번호로 저장된 탄환이름을 리스트에서 삭제
            Conf.Main.ENEMY_ID_LIST.Remove(this.gameObject.name);
            Debug.Log($"Removed Enemy Bullet : {this.name}");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //플레이어에 부딪히면 데미지를 주고 탄환파괴
    {
        if (this.gameObject.layer == 9 && collision.GetComponent<Collider2D>().gameObject.layer == 8)
        {

            Destroy(this.gameObject); //객체를 삭제한다
            //번호로 저장된 탄환이름을 리스트에서 삭제
            Conf.Main.PLAYER_ID_LIST.Remove(this.gameObject.name);
            Debug.Log($"Removed Player Bullet : {this.name}");

        }
        else if (this.gameObject.layer == 10 && collision.GetComponent<Collider2D>().gameObject.layer == 7)
        {
            Destroy(this.gameObject); //객체를 삭제한다
            //번호로 저장된 탄환이름을 리스트에서 삭제
            Conf.Main.ENEMY_ID_LIST.Remove(this.gameObject.name);
            Debug.Log($"Removed Enemy Bullet : {this.name}");
        }

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
