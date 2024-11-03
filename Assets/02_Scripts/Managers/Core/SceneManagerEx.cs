using System;
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

    public void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Managers.Game.AddMonsterOnNowScene();

        if (Managers.Game._player == null) return;
        Managers.Game._player._interectController.Init();
        Managers.Game._player._playerInput.OpenPlayerUI<LargeMapUI>();
        Managers.Game._player._playerInput.OpenPlayerUI<LargeMapUI>();
        Managers.Game._player.PlayerStatInit();
    }

    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public bool LoadingSceneCheck()
    {
        return SceneManager.GetActiveScene().buildIndex == (int)Define.Scene.Loading;
    }
    public bool DungeonSceneCheck()
    {
        return SceneManager.GetActiveScene().buildIndex == (int)Define.Scene.Dungeon;
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

    public void SceneChange(string sceneName)
    {
        try
        {
            Define.Scene targetScene = (Define.Scene)Enum.Parse(typeof(Define.Scene), sceneName, true);
            _targetScene = targetScene;
            LoadScene(Define.Scene.Loading);
        }
        catch (Exception e)
        {
            Logger.LogError(e + "씬 없음, 지정한 씬 이름을 다시 확인");
        }
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
