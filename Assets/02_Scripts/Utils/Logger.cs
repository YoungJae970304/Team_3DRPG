using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public static class Logger 
{
    [Conditional("UNITY_EDITOR")]//���Ǻ� ������ �ɺ�
    public static void Log(string msg) {
        UnityEngine.Debug.LogFormat("[{0} {1}]", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), msg);
    }

    [Conditional("UNITY_EDITOR")]
    public static void LogWarning(string msg)//���� �α��Լ�
    {
        UnityEngine.Debug.LogWarningFormat("[{0} {1}]", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), msg);
    }
    public static void LogError(string msg)//���� �α��Լ�
    {
        UnityEngine.Debug.LogErrorFormat("[{0} {1}]", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), msg);
    }

}
