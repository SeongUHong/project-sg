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

        flame = Managers.Game.Player.transform.GetChild(0).gameObject;

        animator = GetComponent<Animator>();
        animator.SetBool("expl", false);

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

    void OnAttack()
    {
        if(!(_stat.AttackGague < 20))
        {
            flame.SetActive(true);
            StartCoroutine(WaitForIt());
            _stat.AttackGagueDown();
            Managers.Skill.SpawnSkill(SKILL_NAME, Managers.Game.Player.transform.Find("ship2-flame").position,
                Managers.Game.Player.transform.Find("ship2-flame").transform.up, _stat.AttackDistance, _stat.ProjectileSpeed, _stat.Offence, Define.Skill.Launch, Managers.Game.Player.transform);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("On Trigger");
        if (collision.GetComponent<Collider2D>().gameObject.layer == 10)
        {

            _stat.OnAttacked(100);
            if (_stat.Hp <= 0)
            {
                Conf.Main.PLAYER_DEAD_FLAG = true;
            }
            if (Conf.Main.PLAYER_DEAD_FLAG && Managers.Game.Player != null)
            {
                animator.SetBool("expl", true);
                Conf.Main._result.SetText();
                
                Conf.Main._result.Show();
            }
        }

    }


    void OnAttacked()
    {


    }
}

