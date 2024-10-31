using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class QuestManager
{
    //현재 플레이어 레벨 // 사용중
    public int _currPlayerLevel = 1;

    ///////////////////////// 여기 밑부터 새로 작성한 부분
    public Dictionary<int,int> _questList = new Dictionary<int,int>();
    public Dictionary<int,int> _questrequirements = new Dictionary<int, int>();
    public Dictionary<int, int> _targetCheck = new Dictionary<int, int>();
    public Dictionary<int, int> _countCheck = new Dictionary<int, int>();
    public Dictionary<int, string> _questName = new Dictionary<int, string>();
    public List<int> _questID = new List<int>();
    public List<int> _activeQuest = new List<int>();
    public List<int> _progressQuest = new List<int>();
    public List<int> _completeQuest = new List<int>();
    public Action _curLevelCountPlus;
    public Action _completeCheck;
    DataTableManager _dataTableManager;
    public Define.QuestInput _questInput;
    public int test123;
    public void Init()
    {
        _dataTableManager = Managers.DataTable;
        // LoadQuestData();
        QuestListInput();
        _curLevelCountPlus += LevelCountPlus;
        
    }

    /////////////////////////////////////
    ///여기 밑부터 내가 작성한 부분
    public void LevelCountPlus()
    {
        _currPlayerLevel++;
        for (int i = _questID[0]; i <= _questID[_questID.Count-1]; i++)
        {
            if (_questList[i] <= _currPlayerLevel)
            {
                _activeQuest.Add(i);
                //Managers.UI.Init();
   
            }
        }
    }
    
    void QuestListInput()
    {
        foreach(var questdata in _dataTableManager._QuestData)
        {
            _questList.Add(questdata.ID, questdata.PlayerLevelRequirement);
            _questName.Add(questdata.ID, questdata.Name);
            _questID.Add(questdata.ID);
            _questrequirements.Add(questdata.ID, questdata.TargetCount);
        }
    }
}