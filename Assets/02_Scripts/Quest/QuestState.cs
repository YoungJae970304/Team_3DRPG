using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestState
{
    public enum State
    {
        //퀘스트가 아닌상태
        RequirementNot,
        //시작 가능 상태
        CanStart,
        //진행 중인 상태
        InProgress,
        //완료 가능 상태
        CanFinish,
        //완료한 상태
        Finished,
    }
}
