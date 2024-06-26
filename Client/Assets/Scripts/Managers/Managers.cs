using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    #region Contents
    GameManagerEx _game = new GameManagerEx();
    SkillManager _skill = new SkillManager();

    public static GameManagerEx Game { get { return Instance._game; } }
    public static SkillManager Skill { get { return Instance._skill; } }
    #endregion

    #region Core
    DataManager _data = new DataManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    UIManager _ui = new UIManager();
    NetworkManager _network = new NetworkManager();
    ClientPacketManager _packet = new ClientPacketManager();

    public static DataManager Data { get { return Instance._data; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static UIManager UI { get { return Instance._ui; } }
    public static NetworkManager Network { get { return Instance._network; } }
    public static ClientPacketManager Packet { get { return Instance._packet; } }
    #endregion

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (_network.IsConnet)
            _network.HandlePackets();
    }

    static void Init()
    {
        //매니저 초기화 & 없을 경우 생성
        if (s_instance != null)
            return;

        GameObject go = GameObject.Find("@Managers");
        if (go == null)
        {
            go = new GameObject("@Managers");
            go.AddComponent<Managers>();
        }

        DontDestroyOnLoad(go);
        s_instance = go.GetComponent<Managers>();

        //풀 매니저 실행
        s_instance._pool.Init();

        //게임 매니저 실행
        s_instance._game.Init();

        // 패킷 매니저 초기화
        s_instance._packet.Init();
    }

     public static void Clear()
    {
        Pool.Clear();
        Scene.Clear();
        UI.Clear();

        s_instance._game = new GameManagerEx();
        s_instance._skill = new SkillManager();
    }
}
