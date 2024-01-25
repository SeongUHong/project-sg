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



    public GameObject Player { get { return _player; } }
    public GameObject Enemy { get { return _enemy; } }

    public Main_Panel Main_Panel { get { return Managers.UI.MainSceneUI<Main_Panel>(); } }
    public Select_Panel Select_Panel { get { return Managers.UI.MakePopUp<Select_Panel>(); } }


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
            Debug.Log("Failed Load PlayerSpawnPos");
            return;
        }
        _playerSpawnPos = playerSpawnPos.transform.position;

        //적플레이어 스폰위치
        GameObject enemySpawnSpot = GameObject.Find(Enum.GetName(typeof(Define.SceneLocateObject), Define.SceneLocateObject.EnemySpawnSpot));
        if (enemySpawnSpot == null)
        {
            Debug.Log("Failed Load EnemySpawnSpot");
            return;
        }
        _enemySpawnPos = enemySpawnSpot.transform.position;

    }


    public GameObject InstantiatePlayer()
    {
        GameObject player = null;

        if (Conf.Main.ChosenShip == "Player1")
        {
            player = Managers.Resource.Instantiate("Characters/Player1");
            if (player == null)
            {
                Debug.Log("Failed Load Player");
                //return null;
            }

        }
        else
        {
            player = Managers.Resource.Instantiate("Characters/Player2");
            if (player == null)
            {
                Debug.Log("Failed Load Player");
                //return null;
            }
        }

        
        _player = player;

        player.transform.position = PlayerSpawnPos;

        return player;
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
}
