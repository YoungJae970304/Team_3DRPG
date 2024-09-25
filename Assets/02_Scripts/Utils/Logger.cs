using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public static class Logger 
{
    [Conditional("UNITY_EDITOR")]//���Ǻ� ������ �ɺ�
    public static void Log(string msg) {
        UnityEngine.Debug.LogFormat("[{0}]",  msg);
    }
    [Conditional("UNITY_EDITOR")]//���Ǻ� ������ �ɺ�
    public static void Log(int msg)
    {
        UnityEngine.Debug.LogFormat("[{0}]", msg);
    }
    [Conditional("UNITY_EDITOR")]//���Ǻ� ������ �ɺ�
    public static void Log(float msg)
    {
        UnityEngine.Debug.LogFormat("[{0}]", msg);
    }
    public static void Log(GameObject msg)
    {
        UnityEngine.Debug.LogFormat("[{0}]", msg);
    }
    [Conditional("UNITY_EDITOR")]
    public static void LogWarning(string msg)//���� �α��Լ�
    {
        UnityEngine.Debug.LogWarningFormat("[{0}]", msg);
    }
    public static void LogError(string msg)//���� �α��Լ�
    {
        UnityEngine.Debug.LogErrorFormat("[{0}]", msg);
    }

}
