using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBtnController : MonoBehaviour
{
    public void OnClickShowUI()
    {

    }

    public void OnClickSceneChangeBtn(string sceneName)
    {
		try
		{
            Define.Scene targetScene = (Define.Scene)Enum.Parse(typeof(Define.Scene), sceneName, true);
            Managers.Scene._targetScene = targetScene;
            Managers.Scene.LoadScene(Define.Scene.Loading);
        }
		catch (Exception e)
		{
            Logger.LogError(e + "씬 없음, 지정한 씬 이름을 다시 확인");
		}
    }
}
