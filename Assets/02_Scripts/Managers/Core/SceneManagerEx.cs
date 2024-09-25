using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene
    {
        get { return GameObject.FindObjectOfType<BaseScene>(); }
    }

    public void LoadScene(Define.Scene type)
    {
        Logger.Log($"{type} scene Loading");

        Time.timeScale = 1f;
        //CurrentScene.Clear();
        Managers.Clear();
        SceneManager.LoadScene(GetSceneName(type));
    }

    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }
    public void ReloadScene()
    {
        Logger.Log($"{SceneManager.GetActiveScene().name} scene Loading");

        Time.timeScale = 1f;

        Managers.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public AsyncOperation LoadSceneAsync(Define.Scene sceneType)
    {
        Logger.Log($"{sceneType} scene Async( Loading");

        Time.timeScale = 1f;


        return SceneManager.LoadSceneAsync(sceneType.ToString());
    }
    public void Clear()
    {
        CurrentScene.Clear();
    }
}
