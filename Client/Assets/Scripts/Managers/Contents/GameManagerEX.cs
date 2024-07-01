using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerEx : ManagerBase
{
    //GameManager�̸��� ����� �� ���� ����� Ex�� ����

    //�÷��̾�
    GameObject _player;

    //�����Ǿ� �ִ� ��
    GameObject _enemy;

    //���� �Ǵ� ����
    Vector3 _playerSpawnPos;
    Vector3 _enemySpawnPos;

    //������ġ ���� ����
    //float _positionVar = 3.0f;

    //�÷��̾� ��
    string _playerNick;
    //���÷��̾� ��
    string _enemyNick;

    //�÷��̾ �������� ���������� 
    bool _isLeft;

    //���÷��̾� ��ġ����
    Vector3 _enemyPosition;
    Vector3 _enemyRotate;

    //źȯ �߻� �㰡
    bool _canShoot;

    //�÷��̾� ��ħ
    bool _playerDeadFlag;

    //���÷��̾� ��ħ
    bool _enemyDeadFlag;

    //���� ������ ����
    bool _isPause;

    //���� ���� �ð�
    int _remainSec = 0;

    public GameObject Player { get { return _player; } }
    public GameObject Enemy { get { return _enemy; } }

    public GameObject Player_Right { get { return _player; } }
    public GameObject Enemy_Left { get { return _enemy; } }

    public Result_Panel Result_Panel { get { return Managers.UI.MakePopUp<Result_Panel>();  } }
    public InGame_NickName_Panel InGame_NickName_Panel { get { return Managers.UI.MakePopUp<InGame_NickName_Panel>(); } }

    public string PlayerNick { get { return _playerNick; } set { _playerNick = value; } }
    public string EnemyNick { get { return _enemyNick; } set { _enemyNick = value; } }
    public bool IsLeft { get { return _isLeft; } set { _isLeft = value; } }
    public Vector3 EnemyPosition { get { return _enemyPosition; } set { _enemyPosition = value; } }
    public Vector3 EnemyRotate { get { return _enemyRotate; } set { _enemyRotate = value; } }
    public bool CanShoot { get { return _canShoot; } set { _canShoot = value;  } }
    public bool PlayerDeadFlag { get { return _playerDeadFlag; } set { _playerDeadFlag = value; } }
    public bool EnemyDeadFlag { get { return _enemyDeadFlag; } set { _enemyDeadFlag = value; } }
    public bool IsPause { get { return _isPause; } set { _isPause = value; } }
    public int RemainSec { get { return _remainSec; } set { _remainSec = value; } }

    //���� �Ǵ� ����
    public Vector2 PlayerSpawnPos { get { return _playerSpawnPos; } }
    public Vector2 EnemySpawnPos { get { return _enemySpawnPos; } }

    //���� �̺�Ʈ
    public Action<int> AddSqawnAction;

    public override void Init()
    {
        //�÷��̾� ������ġ
        GameObject playerSpawnPos = GameObject.Find(Enum.GetName(typeof(Define.SceneLocateObject), Define.SceneLocateObject.PlayerSpawnSpot));      
        if (playerSpawnPos == null)
        {
            return;
        }
        _playerSpawnPos = playerSpawnPos.transform.position;


        //���÷��̾� ������ġ
        GameObject enemySpawnSpot = GameObject.Find(Enum.GetName(typeof(Define.SceneLocateObject), Define.SceneLocateObject.EnemySpawnSpot));
        if (enemySpawnSpot == null)
        {
            return;
        }
        _enemySpawnPos = enemySpawnSpot.transform.position;

        Result_Panel.Init();
    }


    public GameObject InstantiatePlayer()
    {
        GameObject player = null;

        if (Managers.Game.IsLeft)
        {
            player = Managers.Resource.Instantiate("Characters/Player");
            player.transform.position = PlayerSpawnPos;
        }
        else
        {
            player = Managers.Resource.Instantiate("Characters/Player_Right");
            player.transform.position = EnemySpawnPos;
        }

        _player = player;

        return player;
    }

    public GameObject InstantiateEnemy()
    {
        GameObject enemy = null;

        if (Managers.Game.IsLeft)
        {
            enemy = Managers.Resource.Instantiate("Characters/Enemy");
            enemy.transform.position = EnemySpawnPos;
        }
        else
        {
            enemy = Managers.Resource.Instantiate("Characters/Enemy_Left");
            enemy.transform.position = PlayerSpawnPos;

        }

        _enemy = enemy;
        return enemy;
    }

    //ĳ���Ͱ� �����Ǵ� ��ġ�� ��ȯ
    public Vector2 CreatePos(Define.Layer layer)
    {
        Vector2 basePos;

        float spawnRange = 0;

        switch (layer)
        {
            case Define.Layer.Player:
                basePos = PlayerSpawnPos;

                spawnRange = Conf.Main.UNIT_SPAWN_RANGE;
                break;
            case Define.Layer.Enemy:
                basePos = _enemySpawnPos;

                spawnRange = Conf.Main.MONSTER_SPAWN_RANGE;
                break;
            default:
                break;
                
        }

        //���� ��ġ ����
        Vector2 newPos = Vector2.zero;

        return newPos;
    }

    public GameObject Spawn(Define.Layer layer, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate($"Characters/{path}", parent);

        

        //��ġ ����
        go.transform.position = CreatePos(layer);
        return go;
    }

    public void Despawn(Define.Layer layer, GameObject go)
    {
        /*SpawningPool sp = go.GetComponent<SpawningPool>();

        switch (layer)
        {
            case Define.Layer.Unit:
                if (_units.Contains(go))
                {
                    _units.Remove(go);

                    sp.MinusUnitCount(1);
                    if (AddSqawnAction != null)
                        AddSqawnAction.Invoke(-1);
                }
                else
                {
                    Debug.Log($"Not Spawned Object ObjectName:{go.name}, Layer{Enum.GetName(typeof(Define.Layer), layer)}");
                    return;
                }
                break;
            case Define.Layer.Monster:
                if (_monsters.Contains(go))
                {
                    _monsters.Remove(go);

                    sp.MinusMonsterCount(1);
                    if (AddSqawnAction != null)
                        AddSqawnAction.Invoke(-1);
                }
                else
                {
                    Debug.Log($"Not Spawned Object ObjectName:{go.name}, Layer{Enum.GetName(typeof(Define.Layer), layer)}");
                    return;
                }
                break;
        }*/

        Managers.Resource.Destroy(go);
    }

    public void Clear()
    {

    }
}
