
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : BaseController
{
    //���� �̸�
    string SKILL_NAME = "fireballbluebig";

    //�÷��̾���Ʈ�ѷ� UI�� ����ִ� ������Ʈ
    UIScene _uiScene;

    //발사 이펙트
    GameObject flame;

    //스텟
    Stat _stat;

    //
    Animator animator;


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

        flame = Managers.Game.Enemy.transform.GetChild(0).gameObject;

        animator = GetComponent<Animator>();
        animator.SetBool("expl", false);

        //AddAction();

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
        if (!(_stat.AttackGague < 20))
        {
            flame.SetActive(true);
            StartCoroutine(WaitForIt());
            _stat.AttackGagueDown();
            Managers.Skill.SpawnSkill(SKILL_NAME, Managers.Game.Enemy.transform.Find("ship2-flame_enemy").position,
                Managers.Game.Enemy.transform.Find("ship2-flame_enemy").transform.up, _stat.AttackDistance, _stat.ProjectileSpeed, _stat.Offence, Define.Skill.Launch, Managers.Game.Enemy.transform);

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("On Trigger");
        if (collision.GetComponent<Collider2D>().gameObject.layer == 9)
        {

            _stat.OnAttacked(100);
            if (_stat.Hp <= 0)
            {
                Conf.Main.ENEMY_DEAD_FLAG = true;
            }
            if (Conf.Main.ENEMY_DEAD_FLAG && Managers.Game.Enemy!=null)
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
