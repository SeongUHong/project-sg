using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : ManagerBase
{
    #region Pool
    class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }

        Stack<Poolable> _poolStack = new Stack<Poolable>();

        public void Init(GameObject original, int count = 5)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name}_Root";

            for (int i = 0; i < count; i++)
            {
                Push(Create());
            }
        }

        //������Ʈ�� Poolable �ٿ��� ����
        Poolable Create()
        {
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;
            return Util.GetOrAddComponent<Poolable>(go);
        }

        //���ÿ� ��Ȱ��ȭ �� ����
        public void Push(Poolable poolable)
        {
            if (poolable == null)
            {
                return;
            }

            poolable.transform.parent = Root;
            //��Ȱ��ȭ
            poolable.gameObject.SetActive(false);
            poolable.IsUsing = false;

            //���ÿ� ����
            _poolStack.Push(poolable);
        }

        //���ÿ��� ������
        public Poolable Pop(Transform parent)
        {
            Poolable poolable;

            if (_poolStack.Count > 0)
            {
                poolable = _poolStack.Pop();
            }
            else
            {
                poolable = Create();
            }

            poolable.gameObject.SetActive(true);

            //DontDestroyOnLoad ���� �뵵
            if (parent == null)
            {
                poolable.transform.parent = Managers.Scene.CurrentScene.transform;
            }

            poolable.transform.parent = parent;
            poolable.IsUsing = true;

            return poolable;
        }
    }
    #endregion

    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();
    Transform _root;

    public override void Init()
    {
        //Ǯ�� ������Ʈ�� ���ؼ� �������� �ʴ� �θ� ������Ʈ�� ����
        if (_root == null)
        {
            _root = new GameObject { name = "@PoolRoot" }.transform;
            Object.DontDestroyOnLoad(_root);
        }
    }

    //���ӿ�����Ʈ�� ��ųʸ��� ����
    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = _root;

        _pool.Add(original.name, pool);
    }

    //_pool��ųʸ����� �������� Ǯ�� ������Ʈ��� _poolStack�� ����
    //�������� �ƴϸ� �ı�
    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;
        if (_pool.ContainsKey(name) == false)
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }

        _pool[name].Push(poolable);
    }

    //_pool��ųʸ����� �������� Ǯ�� ������Ʈ��� _poolStack���� ����
    //�������� �ƴϸ� ���� ���� ������� ����
    public Poolable Pop(GameObject original, Transform parent = null)
    {
        if (_pool.ContainsKey(original.name) == false)
        {
            CreatePool(original);
        }

        return _pool[original.name].Pop(parent);
    }

    //_pool��ųʸ����� �������� ������Ʈ�� ������
    public GameObject GetOriginal(string name)
    {
        if (_pool.ContainsKey(name) == false) return null;
        return _pool[name].Original;

    }

    //�������� Ǯ�� ������Ʈ ���� ����
    public void Clear()
    {
        foreach (Transform child in _root)
        {
            GameObject.Destroy(child.gameObject);
        }

        _pool.Clear();
    }


}