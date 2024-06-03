
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnemyController : BaseController
{
    //���� �̸�
    string SKILL_NAME = "fireballbluebig";

    //�÷��̾���Ʈ�ѷ� UI�� ����ִ� ������Ʈ
    UIScene _uiScene;


    //적 오브젝트
    GameObject character;

    //발사 이펙트
    GameObject flame;

    //스텟
    Stat _stat;

    //에니메이터
    Animator animator;

    //위치정보에 필요한 정보
    private Vector2 joystickVector; // 조이스틱의 방향벡터이자 플레이어에게 넘길 방향정보.
    private float speed = 1f; // 캐릭터 스피드
    private float rotateSpeed = 3f; // 회전 속도
    private Vector3 position;
    private Vector3 rotate;


    //적 랜덤움직임
    float random;

    public override void Init()
    {


        //스텟 초기화
        _stat = gameObject.GetComponent<Stat>();
        if (_stat == null)
        {
            Debug.Log("Can't Load Stat Component");
        }
        _stat.SetStat(Managers.Data.GetStatByLevel("EnemyStat", 1));

        //HP바 추가
        if (gameObject.GetComponentInChildren<UIHpBar_Enemy>() == null)
        {
            Managers.UI.MakeWorldUI_Enemy<UIHpBar_Enemy>(transform);
        }
        //��Ʈ�ѷ�UI �ʱ�ȭ
        _uiScene = Managers.UI.UIScene;

        if (_uiScene == null || _uiScene.JoyStickHandler == null)
        {
            Debug.Log("Not Exist Player Controller UI");
        }

        if (Managers.Game.IsLeft) 
        {
            flame = Managers.Game.Enemy.transform.GetChild(0).gameObject;
            character = Managers.Game.Enemy;
        }
        else
        {
            flame = Managers.Game.Enemy_Left.transform.GetChild(0).gameObject;
            character = Managers.Game.Enemy_Left;
        }

        animator = GetComponent<Animator>();
        animator.SetBool("expl", false);


    }

    void Update()
    {
        if (character != null)
            character.GetComponent<Rigidbody2D>().velocity = character.transform.up * speed;
        // 캐릭터는 3의 속도로 계속 전진


        //발사 테스트
        //Invoke("Fire",2.0f);

        position = new Vector3(Managers.Game.EnemyPosition.x, Managers.Game.EnemyPosition.y, 0);

        transform.position = position;

        rotate = Managers.Game.EnemyRotate;

        //현재 보는 각도
        /*float rad = Mathf.Atan2(Managers.Game.EnemyPosition.y, Managers.Game.EnemyPosition.x);
        float bAngle = rad * Mathf.Rad2Deg;*/

        //서버에서 받아온 각도
        float aAngle = rotate.z * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, aAngle * rotateSpeed);
    }

    void AddAction()
    {
        _uiScene.OnAttackBtnDownHandler -= OnAttackBtnDownEvent;
        _uiScene.OnAttackBtnDownHandler += OnAttackBtnDownEvent;
    }

    //���ݹ�ư Ŭ����
    void OnAttackBtnDownEvent()
    {
        if (_stat.AttackGague != 0)
        {
            OnAttack();
        }
        StartCoroutine(AttackCoolTime());

    }

    //테스트용 발사기
    void Fire()
    {
        OnAttack();
    }

    //���� ��Ÿ�� ���� ���� �÷��׸� false
    protected IEnumerator AttackCoolTime()
    {
        yield return new WaitForSeconds(_stat.AttackSpeed);
    }

    public IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(0.2f);
        flame.SetActive(false);
    }

    public void OnAttack()
    {
        if (Managers.Game.IsLeft)
        {
            if (!(_stat.AttackGague < 20))
            {

                flame.SetActive(true);
                StartCoroutine(WaitForIt());
                _stat.AttackGagueDown();

                Managers.Skill.SpawnSkill(SKILL_NAME, Managers.Game.Enemy.transform.Find("ship2-flame_enemy").position,
                    Managers.Game.Enemy.transform.Find("ship2-flame_enemy").transform.up, _stat.AttackDistance, _stat.ProjectileSpeed, _stat.Offence, Define.Skill.Launch, Managers.Game.Enemy.transform);


            }
        }else
        {
            if (!(_stat.AttackGague < 20))
            {

                flame.SetActive(true);
                StartCoroutine(WaitForIt());
                _stat.AttackGagueDown();

                Managers.Skill.SpawnSkill(SKILL_NAME, Managers.Game.Enemy_Left.transform.Find("ship2-flame_enemy").position,
                    Managers.Game.Enemy_Left.transform.Find("ship2-flame_enemy").transform.up, _stat.AttackDistance, _stat.ProjectileSpeed, _stat.Offence, Define.Skill.Launch, Managers.Game.Enemy_Left.transform);

            }
        }
        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<Collider2D>().gameObject.layer == 9)
        {

            _stat.OnAttacked(5);
            if (_stat.Hp <= 0)
            {
                Managers.Game.EnemyDeadFlag = true;
            }
            if (Managers.Game.IsLeft)
            {
                if (Managers.Game.EnemyDeadFlag && Managers.Game.Enemy != null)
                {
                    animator.SetBool("expl", true);
                    Conf.Main._result.SetText();
                    Conf.Main._result.Show();
                }
            }
            else
            {
                if (Managers.Game.EnemyDeadFlag && Managers.Game.Enemy_Left != null)
                {
                    animator.SetBool("expl", true);
                    Conf.Main._result.SetText();
                    Conf.Main._result.Show();
                }
            }
            
            
        }
            
    }

    void OnAttacked()
    {
        
    }

    Vector2 Random()
    {

        random = UnityEngine.Random.Range(0,360);

        joystickVector = new Vector2(random, random);

        return joystickVector;
    }

    public void MoveHandler()
    {
        Random();

    }

}
