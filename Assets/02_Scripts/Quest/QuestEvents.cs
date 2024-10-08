using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestEvents : MonoBehaviour
{
    //퀘스트 시작 액션
    public event Action<string> _onStartQuest;
    

    //퀘스트 시작 트리거 함수
    public void StartQuest(string id)
    {
         if(_onStartQuest != null)
        {
            //퀘스트 id로 퀘스트 시작 알림
            _onStartQuest(id);
        }
    }
}
