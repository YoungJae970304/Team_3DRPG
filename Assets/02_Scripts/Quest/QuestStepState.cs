using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestStepState
{
    public string _state;
    public string _status;

    public QuestStepState(string state, string status)
    {
        _state = state;
        _status = status;
    }

    public QuestStepState()
    {
        _state = "";
        _status = "";
    }
}
