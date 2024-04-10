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



    public GameObject Player { get { return _player; } }
    public GameObject Enemy { get { return _enemy; } }

    public GameObject Player_Right { get { return _player; } }
    public GameObject Enemy_Left { get { return _enemy; } }

    public Main_Panel Main_Panel { get { return Managers.UI.MainSceneUI<Main_Panel>(); } }
    public Result_Panel Result_Panel { get { return Managers.UI.MakePopUp<Result_Panel>();  } }
    public NickName_Panel NickName_Panel { get { return Managers.UI.MakePopUp<NickName_Panel>(); } }
    public Matching_Panel Matching_Panel { get { return Managers.UI.MakePopUp<Matching_Panel>(); } }
    public InGame_NickName_Panel InGame_NickName_Panel { get { return Managers.UI.MakePopUp<InGame_NickName_Panel>(); } }

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

    }


    public GameObject InstantiatePlayer()
    {
        GameObject player = null;

        /*if (Conf.Main.ChosenShip == "Player1")
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
        }*/

        
        

        if (Conf.Main.IS_LEFT)
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


        

        

        if (Conf.Main.IS_LEFT)
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
}
