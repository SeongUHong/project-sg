using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{

    //���� �̸�
    string SKILL_NAME = "fireballredbig";
    //string SKILL_NAME = "Missile";

    //�÷��̾���Ʈ�ѷ� UI�� ����ִ� ������Ʈ
    UIScene _uiScene;

    //발사 이펙트
    GameObject flame;

    //스텟
    Stat _stat;

    //애니메이터
    Animator animator;

    public override void Init()
    {   

        //���� �ʱ�ȭ
        _stat = gameObject.GetComponent<Stat>();
        if (_stat == null)
        {
            Debug.Log("Can't Load Stat Component");
        }
        _stat.SetStat(Managers.Data.GetStatByLevel("PlayerStat", 1));

        //HP�� �߰�
        if (gameObject.GetComponentInChildren<UIHpBar>() == null)
        {
            Managers.UI.MakeWorldUI<UIHpBar>(transform);
        }

        //��Ʈ�ѷ�UI �ʱ�ȭ
        _uiScene = Managers.UI.UIScene;

        /*if (_uiScene == null || _uiScene.JoyStickHandler == null)
        {
            Debug.Log("Not Exist Player Controller UI");
        }*/

        AddAction();

        if (Managers.Game.IsLeft)
        {
            flame = Managers.Game.Player.transform.GetChild(0).gameObject;
        }
        else
        {
            flame = Managers.Game.Player_Right.transform.GetChild(0).gameObject;
        }
        

        animator = GetComponent<Animator>();
        animator.SetBool("expl", false);

    }

    public void Update()
    {
        if (_stat.Hp<=0) { 
            //C_Dead 보내기
            
        }
    }

    //Invoke�� ����� �� �ְ� ���� ��ư�� �׼� �߰�
    void AddAction()
    {
        _uiScene.OnAttackBtnDownHandler -= OnAttackBtnDownEvent;
        _uiScene.OnAttackBtnDownHandler += OnAttackBtnDownEvent;

    }

    //���ݹ�ư Ŭ����
    void OnAttackBtnDownEvent()
    {
        if(_stat.AttackGague != 0)
        {
            OnAttack();
        }
        StartCoroutine(AttackCoolTime());
    }

    //���� ��Ÿ�� ���� ���� �÷��׸� false
    protected IEnumerator AttackCoolTime()
    {
        //yield return new WaitForSeconds(_stat.AttackSpeed);
        yield return new WaitForSeconds(5.0f);
        
    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(0.2f);
        flame.SetActive(false);
    }

    //어택시
    void OnAttack()
    {

        Debug.Log("OnAttack");

        if (Managers.Game.IsLeft)
        {
            if (!(_stat.AttackGague < 20))
            {
                /*if (_stat.AttackGague == 100)
                    SKILL_NAME = "fireballredtail";*/

                flame.SetActive(true);
                StartCoroutine(WaitForIt());
                _stat.AttackGagueDown();
                Managers.Skill.SpawnSkill(SKILL_NAME, Managers.Game.Player.transform.Find("ship2-flame").position,
                    Managers.Game.Player.transform.Find("ship2-flame").transform.up, _stat.AttackDistance, _stat.ProjectileSpeed, _stat.Offence, Define.Skill.Launch, Managers.Game.Player.transform);

                Debug.Log($"Left Attack");

                C_Shot shot = new C_Shot();
                shot.posX = Managers.Game.Player.transform.Find("ship2-flame").position.x;
                shot.posY = Managers.Game.Player.transform.Find("ship2-flame").position.y;
                shot.angle = Managers.Game.Player.transform.rotation.z;
                
                Managers.Network.Send(shot.Write());
            }
        }
        else
        {
            if (!(_stat.AttackGague < 20))
            {
                /*if (_stat.AttackGague == 100)
                    SKILL_NAME = "fireballredtail";*/

                flame.SetActive(true);
                StartCoroutine(WaitForIt());
                _stat.AttackGagueDown();
                Managers.Skill.SpawnSkill(SKILL_NAME, Managers.Game.Player_Right.transform.Find("ship2-flame").position,
                    Managers.Game.Player_Right.transform.Find("ship2-flame").transform.up, _stat.AttackDistance, _stat.ProjectileSpeed, _stat.Offence, Define.Skill.Launch, Managers.Game.Player_Right.transform);

                Debug.Log($"Right Attack");

                C_Shot shot = new C_Shot();
                shot.posX = Managers.Game.Player.transform.Find("ship2-flame").position.x;
                shot.posY = Managers.Game.Player.transform.Find("ship2-flame").position.y;
                shot.angle = Managers.Game.Player.transform.rotation.z;

                Managers.Network.Send(shot.Write());
            }
        }
        

    }

    //기체폭파 & 결과화면문구생성
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().gameObject.layer == 10)
        {

            /*if (_stat.Hp <= 0)
            {
                Managers.Game.PlayerDeadFlag = true;
            }
            if (Managers.Game.IsLeft)
            {
                if (Managers.Game.PlayerDeadFlag && Managers.Game.Player != null)
                {
                    animator.SetBool("expl", true);
                    Conf.Main._result.SetText();

                    Conf.Main._result.Show();
                }
            }
            else
            {
                if (Managers.Game.PlayerDeadFlag && Managers.Game.Player_Right != null)
                {
                    animator.SetBool("expl", true);
                    Conf.Main._result.SetText();

                    Conf.Main._result.Show();
                }
            }*/
            
        }

    }


    void OnAttacked()
    {


    }
}

