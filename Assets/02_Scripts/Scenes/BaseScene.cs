using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    //Define.Scene _sceneType = Define.Scene.Unknown;
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        // 타입으로 오브젝트를 찾고
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));

        // 없으면 생성
        if (obj == null)
        {
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
        }
    }

    // 여기서 정의하지 않을 것이라 abstract로 제작
    public abstract void Clear();

    public void OnSceneChange(string sceneName)
    {
        Managers.Scene.SceneChange(sceneName);

        Managers.UI.CloseAllOpenUI();
    }

    public void OnStartBGM(string path)
    {
        Managers.Sound.Play(path, Define.Sound.Bgm);
    }
}
