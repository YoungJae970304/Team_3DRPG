using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class PubAndSub 
{
    static Hashtable Actions= new Hashtable();  //발행-구독관계를 저장할 해시테이블
    #region 발행
    //매개변수 없는 함수 발행
    public static void Publish(string name) {
        if (Actions.ContainsKey(name))
        {
            if (!(Actions[name] is Action)) { return; }
            (Actions[name] as Action)?.Invoke();
        }
    }
    //매개변수 있는 함수 발행
    public static void Publish<T>(string name,T parameter)
    {
        if (Actions.ContainsKey(name))
        {
            if (!(Actions[name] is Action<T>)) { return; }
            (Actions[name] as Action<T>)?.Invoke(parameter);
        }
    }
    #endregion
    #region 구독
    //매개변수 없는 함수 구독
    public static void Subscrib(string name, Action action)
    {
        if (Actions.ContainsKey(name))
        {
            if (!(Actions[name] is Action)) { return; }
            Action action1 = (Actions[name] as Action);
            action1 += action;
            Actions[name] = action1;
        
        }
        else {
            Actions.Add(name, action);
        }
    }
    //매개변수 있는 함수 구독
    public static void Subscrib<T>(string name, Action<T> action)
    {
        if (Actions.ContainsKey(name))
        {
            if (!(Actions[name] is Action<T>)) { return; }
            Action<T> action1 = (Actions[name] as Action<T>);
            action1 -= action;//중복 방지
            action1 += action;
            Actions[name] = action1;
            //Logger.LogError("구독됨");


        }
        else
        {
            Actions.Add(name, action);
            //Logger.LogError("구독됨");
        }
    }
    #endregion
    #region 구독해제
    //매개변수 없는 함수 구독해제
    public static void UnSubscrib(string name, Action action)
    {
        if (Actions.ContainsKey(name))
        {
            if (!(Actions[name] is Action)) { return; }
            Action action1 = (Actions[name] as Action);
            action1 -= action;
            Actions[name] = action1;
            if (Actions[name] == null) { Actions.Remove(name); };
        }
    }
    //매개변수 있는 함수 구독해제
    public static void UnSubscrib<T>(string name, Action<T> action)
    {
        if (Actions.ContainsKey(name))
        {
            if (!(Actions[name] is Action<T>)) { return; }
            Action<T> action1 = (Actions[name] as Action<T>);
            action1 -= action;
            Actions[name] = action1;
            if (Actions[name] == null) { Actions.Remove(name); };
        }
    }
    #endregion
}
