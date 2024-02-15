using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{

    //���� �̸�
    string SKILL_NAME = "fireballredbig";

    //�÷��̾���Ʈ�ѷ� UI�� ����ִ� ������Ʈ
    UIScene _uiScene;

    //발사 이펙트
    GameObject flame;

    //스텟
    Stat _stat;

    public override void Init()
    {   

        //������ٵ�
        _rig = gameObject.GetComponent<Rigidbody2D>();
        if (_rig == null)
        {
            Debug.Log("Can't Load Rigidbody Component");
        }

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

        if (_uiScene == null || _uiScene.JoyStickHandler == null)
        {
            Debug.Log("Not Exist Player Controller UI");
        }

        AddAction();

        flame = Managers.Game.Player.transform.GetChild(0).gameObject;

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
        yield return new WaitForSeconds(_stat.AttackSpeed);
    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(0.1f);
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
}

