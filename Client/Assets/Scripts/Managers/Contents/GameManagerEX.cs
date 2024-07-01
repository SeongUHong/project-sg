using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerEx : ManagerBase
{
    //GameManager이름을 사용할 수 없는 관계로 Ex를 붙임

    //플레이어
    GameObject _player;

    //스폰되어 있는 적
    GameObject _enemy;

    //스폰 되는 지점
    Vector3 _playerSpawnPos;
    Vector3 _enemySpawnPos;

    //스폰위치 랜덤 범위
    //float _positionVar = 3.0f;

    //플레이어 닉
    string _playerNick;
    //적플레이어 닉
    string _enemyNick;

    //플레이어가 왼쪽인지 오른쪽인지 
    bool _isLeft;

    //적플레이어 위치정보
    Vector3 _enemyPosition;
    Vector3 _enemyRotate;

    //탄환 발사 허가
    bool _canShoot;

    //플레이어 격침
    bool _playerDeadFlag;

    //적플레이어 격침
    bool _enemyDeadFlag;

    //게임 시작전 포즈
    bool _isPause;

    //게임 남은 시간
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

    //스폰 되는 지점
    public Vector2 PlayerSpawnPos { get { return _playerSpawnPos; } }
    public Vector2 EnemySpawnPos { get { return _enemySpawnPos; } }

    //스폰 이벤트
    public Action<int> AddSqawnAction;

    public override void Init()
    {
        //플레이어 스폰위치
        GameObject playerSpawnPos = GameObject.Find(Enum.GetName(typeof(Define.SceneLocateObject), Define.SceneLocateObject.PlayerSpawnSpot));      
        if (playerSpawnPos == null)
        {
            return;
        }
        _playerSpawnPos = playerSpawnPos.transform.position;


        //적플레이어 스폰위치
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

    //캐릭터가 생성되는 위치를 반환
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

        //랜덤 위치 생성
        Vector2 newPos = Vector2.zero;

        return newPos;
    }

    public GameObject Spawn(Define.Layer layer, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate($"Characters/{path}", parent);

        

        //위치 설정
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
