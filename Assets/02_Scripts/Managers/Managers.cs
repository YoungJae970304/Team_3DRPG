using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // 유일성 보장
    // 유일한 매니저를 가져옴 // 프로퍼티 // 읽기 전용
    static Managers Instance { get { Init(); return s_instance; } }

    #region Contents
    GameManager _game = new GameManager();
    DataTableManager _dataTable = new DataTableManager();
    QuestManager _questManager = new QuestManager();
    
    public static GameManager Game { get { return Instance._game; } }
    public static DataTableManager DataTable { get { return Instance._dataTable; } }
    public static QuestManager QuestManager { get { return Instance._questManager; } }
    
    #endregion

    #region Core
    InputManager _input = new InputManager();
    ResourceManager _resourece = new ResourceManager();
    UIManager _ui = new UIManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
    PoolManager _pool = new PoolManager();
    DataManager _data = new DataManager();
    
    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resourece; } }
    public static UIManager UI { get { return Instance._ui; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static DataManager Data { get { return Instance._data; } }
    #endregion

    private void Awake()
    {
        if (s_instance != null)
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {
        
        
    }

    void Update()
    {
        // input으로 안 쓴 이유는 이곳에서 _input으로 직접 접근했기 때문
        _input.OnUpdate();  // 인풋 매니저의 OnUpdate() 실행, OnUpdate()에서 Invoke로 액션 실행
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if (go == null)  // go가 없으면
            {
                go = new GameObject { name = "@Managers" }; // 코드상으로 오브젝트를 만들어줌
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._sound.Init();
            s_instance._pool.Init();
            s_instance._data.Init();
            s_instance._ui.Init();
            
            s_instance._dataTable.Init();
            s_instance._questManager.Init();
            s_instance._scene.Init();
            
        }
    }

    public static void Clear()
    {
        Input.Clear();
        Sound.Clear();
        UI.Clear();
        Scene.Clear();

        Pool.Clear();
    }
}

