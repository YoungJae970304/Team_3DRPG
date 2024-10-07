using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public Define.Scene _targetScene;

    public BaseScene CurrentScene
    {
        get { return GameObject.FindObjectOfType<BaseScene>(); }
    }

    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public void LoadScene(Define.Scene type)
    {
        Time.timeScale = 1f;
        //Managers.Clear();
        SceneManager.LoadScene((int)type);
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;
        Managers.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public AsyncOperation LoadSceneAsync(Define.Scene sceneType)
    {
        Time.timeScale = 1f;
        return SceneManager.LoadSceneAsync((int)sceneType);
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
