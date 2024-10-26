using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNpcPoint : MonoBehaviour
{
    //시작포인트 완료포인트는 동일함 대신 나오는 아이콘이 다름
    [SerializeField] bool _startPoint = false;
    [SerializeField] bool _finishPoint = false;

    DataTableManager _dataTable;

    int _questId;

    QuestState.State _currState;

    QuestIcon _questIcon;


    void GetState(QuestState.State newState)
    {

    }
}
