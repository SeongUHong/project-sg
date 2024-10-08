using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : ManagerBase
{
    int _player_id = 0;
    int _enemy_id = 0;


    //오브젝트를 메모리에 로드
    public T Load<T>(string path) where T : Object
    {
        //풀링된 오브젝트가 있을 경우 가져옴
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if (index >= 0)
            {
                name = name.Substring(index + 1);
            }

            //풀링된 오브젝트 가져오기 처리
            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null)
            {
                return go as T;
            }

        }

        //풀링되어 있지 않으면 걍 로드
        return Resources.Load<T>(path);
    }

    //오브젝트 생성
    //풀링된 오브젝트가 있으면 생성하지 않고 사용
    public GameObject Instantiate(string path, Transform parent = null)
    {
        //메모리에 생성
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if (original == null)
        {
            if ($"{path}" == "UI/Scene/UISceneMain")
                return null;
            //Debug.Log($"Failed to load Prefab : {path}");
            return null;
        }

        //풀링된 오브젝트인지 확인
        if (original.GetComponent<Poolable>() != null)
        {
            return Managers.Pool.Pop(original, parent).gameObject;
        }

        //오브젝트 생성
        GameObject go = Object.Instantiate(original, parent);
        //go.name = original.name;

        //탄환이름을 번호로 리스트에 저장
        if (go.layer == 9)
        {
            /*_player_id++;
            go.name = _player_id.ToString();

            Conf.Main.PLAYER_ID_LIST.Add(go.name);*/
            //go.name = Managers.Skill.GetFireBallID().ToString();
            

        }
        else if (go.layer == 10)
        {
            /*_enemy_id++;
            go.name = _enemy_id.ToString();

            Conf.Main.ENEMY_ID_LIST.Add(go.name);*/

            //go.name = Managers.Skill.GetFireBallID().ToString();
        }


        return go;
    }

    //오브젝트 삭제
    public void Destroy(GameObject go)
    {

        if (go == null) return;

        //만약 풀링이 필요한 오브젝트라면 풀링 매니저한테 위탁
        Poolable poolable = go.GetComponent<Poolable>();
        if (poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }


        Object.Destroy(go);


    }

    public override void Init()
    {
        throw new System.NotImplementedException();
    }
}
