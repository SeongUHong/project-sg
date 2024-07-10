using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField]
    protected int _level;
    [SerializeField]
    protected int _hp;
    [SerializeField]
    protected int _maxHp;
    [SerializeField]
    protected int _offence;
    [SerializeField]
    protected int _defence;
    [SerializeField]
    protected float _moveSpeed;
    [SerializeField]
    protected float _attackDistance;
    [SerializeField]
    protected float _attackSpeed;
    [SerializeField]
    protected float _projectileSpeed;
    [SerializeField]
    protected float _attackGague;
    [SerializeField]
    protected float _maxAttackGague;

    //폭발 애니메이터
    Animator animator;

    public int Level { get { return _level; } set { _level = value; } }
    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int Offence { get { return _offence; } set { _offence = value; } }
    public int Defence { get { return _defence; } set { _defence = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public float AttackDistance { get { return _attackDistance; } set { _attackDistance = value; } }
    public float AttackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }
    public float ProjectileSpeed { get { return _projectileSpeed; } set { _projectileSpeed = value; } }
    public float AttackGague { get { return _attackGague; } set { _attackGague = value; } }
    public float MaxAttackGague { get { return _maxAttackGague; } set { _maxAttackGague = value; } }
    public void SetStat(data.Stat stat)
    {
        _level = stat.level;
        _hp = stat.hp;
        _maxHp = stat.hp;
        _offence = stat.offence;
        _defence = stat.defence;
        _moveSpeed = stat.move_speed;
        _attackSpeed = stat.attack_speed;
        _projectileSpeed = stat.projectile_speed;
        _attackGague = stat.attack_gauge;
        _maxAttackGague = stat.max_attack_gauge;
    }

    public void Update()
    {
        if (Hp <= 0 && this.gameObject.layer == 10)
            Managers.Game.PlayerDeadFlag = true;
        else if (Hp <= 0 && this.gameObject.layer == 9)
            Managers.Game.EnemyDeadFlag = true;
    }

    public virtual bool OnAttacked(int pureDamage)
    {
        int damage = Mathf.Max(0, pureDamage - Defence);
        if (Hp <= 0) return false;
        Hp -= damage;
        


        return false;
    }

    public virtual bool AttackGagueDown()
    {
        _attackGague = (float)(_attackGague - 20.0);

        return true;
    }

    public virtual bool AttackGagueUp()
    {
        if (_attackGague < _maxAttackGague)
        {
            _attackGague += Time.deltaTime * 10f;
        }
        return true;
    }

    public virtual bool OnAttacked_AttackGagueDown()
    {
        if(_attackGague >= 0)
        {
            _attackGague = (float)(_attackGague - 10.0);
        }
        return true;
    }


    protected virtual void OnDead()
    {
    }
}
