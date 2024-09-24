using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public static class Logger 
{
    [Conditional("UNITY_EDITOR")]//조건부 컴파일 심볼
    public static void Log(string msg) {
        UnityEngine.Debug.LogFormat("[{0}]",  msg);
    }

    [Conditional("UNITY_EDITOR")]
    public static void LogWarning(string msg)//워밍 로그함수
    {
        UnityEngine.Debug.LogWarningFormat("[{0}]", msg);
    }
    public static void LogError(string msg)//에러 로그함수
    {
        UnityEngine.Debug.LogErrorFormat("[{0}]", msg);
    }

}
