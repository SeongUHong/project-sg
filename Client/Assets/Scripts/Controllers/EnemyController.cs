
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
    private bool m_IsBreak; // 코루틴 실행여부
    private Coroutine runningCoroutine; // 부드러운 회전 코루틴
    public Vector2 rect;


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

        //AddAction();

    }

    void Update()
    {
        if (character != null)
            //character.GetComponent<Rigidbody2D>().velocity = character.transform.up * speed;
        // 캐릭터는 3의 속도로 계속 전진

        //무빙테스트
        //Invoke("Random",2.0f);
        //InvokeRepeating("TestRotate", 1.0f,50.0f);

        //적 기체 무브
        InvokeRepeating("EnemyMove", 0.25f,0.25f);


        //발사 테스트
        //Invoke("Fire",2.0f);
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

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(0.2f);
        flame.SetActive(false);
    }

    void OnAttack()
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

            _stat.OnAttacked(100);
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

        random = UnityEngine.Random.Range(-1, 1);

        joystickVector = new Vector2(random, random);

        return joystickVector;
    }

    public void MoveHandler()
    {
        Random();

    }

    IEnumerator TestRotate()
    {
        character.transform.Rotate(0, 0, joystickVector.x); 
        return new WaitForSecondsRealtime(3.0f);
        
    }

    //적 기체 위치 C_Move
    IEnumerator EnemyMove()
    {
        TurnAngle(Managers.Game.EnemyRocation);
        return new WaitForSecondsRealtime(0.25f);
    }

    private void TurnAngle(Vector3 currentJoystickVec)
    {
        
        Vector3 originJoystickVec = character.transform.up;
        // character가 바라보고 있는 벡터

        float angle = Vector3.Angle(currentJoystickVec, originJoystickVec);
        int sign = (Vector3.Cross(currentJoystickVec, originJoystickVec).z > 0) ? -1 : 1;
        // angle: 현재 바라보고 있는 벡터와, 조이스틱 방향 벡터 사이의 각도
        // sign: character가 바라보는 방향 기준으로, 왼쪽:+ 오른쪽:-

        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }
        runningCoroutine = StartCoroutine(RotateAngle(angle, sign));
        // 코루틴이 실행중이면 실행 중인 코루틴 중단 후 코루틴 실행 
        // 코루틴이 한 개만 존재하도록.
        // => 회전 중에 새로운 회전이 들어왔을 경우, 회전 중이던 것을 멈추고 새로운 회전을 함.
    }


    IEnumerator RotateAngle(float angle, int sign)
    {
        float mod = angle % rotateSpeed; // 남은 각도 계산
        for (float i = mod; i < angle; i += rotateSpeed)
        {
            character.transform.Rotate(0, 0, sign * rotateSpeed); // 캐릭터 rotateSpeed만큼 회전
            yield return new WaitForSeconds(0.01f); // 0.01초 대기
        }
        //character.transform.Rotate(0, 0, sign * mod); // 남은 각도 회전
    }
}
