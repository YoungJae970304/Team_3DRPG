using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // ���ϼ� ����
    // ������ �Ŵ����� ������ // ������Ƽ // �б� ����
    static Managers Instance { get { Init(); return s_instance; } }
    #region Contents
    GameManager _game = new GameManager();

    public static GameManager Game { get { return Instance._game; } }
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
    void Start()
    {
        Init();
    }

    void Update()
    {
        // input���� �� �� ������ �̰����� _input���� ���� �����߱� ����
        _input.OnUpdate();  // ��ǲ �Ŵ����� OnUpdate() ����, OnUpdate()���� Invoke�� �׼� ����
    }

    static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if(go == null)  // go�� ������
            {
                go = new GameObject { name = "@Managers" }; // �ڵ������ ������Ʈ�� �������
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._sound.Init();
            s_instance._pool.Init();
            s_instance._data.Init();
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

