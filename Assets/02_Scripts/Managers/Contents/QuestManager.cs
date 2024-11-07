using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager
{
    //현재 플레이어 레벨 // 사용중
    public int _currPlayerLevel = 1;

    ///////////////////////// 여기 밑부터 새로 작성한 부분
    public Dictionary<int,int> _questList = new Dictionary<int,int>(); //퀘스트 진행을 위한 플레이어 레벨 제한 정보
    public Dictionary<int, int> _targetCheck = new Dictionary<int, int>(); // 퀘스트 목표
    public Dictionary<int, int> _countCheck = new Dictionary<int, int>(); // 현재 진행중인 퀘스트의 진행 값 // 필수 저장
    public Dictionary<int, string> _questName = new Dictionary<int, string>(); // 퀘스트 이름
    public Dictionary<int, string> _targetName = new Dictionary<int, string>(); // 타켓 이름
    public Dictionary<int, GameObject> _changeText = new Dictionary<int, GameObject>();
    public Dictionary<int, int> _completeChecks = new Dictionary<int, int>();
    public Dictionary<int, bool> _questComplete = new Dictionary<int, bool>();
    public Dictionary<int, int> _targetToQuestID = new Dictionary<int, int>();
    public List<int> _questID = new List<int>();//모든 퀘스트의 아이디 // 필수 저장
    public List<int> _activeQuest = new List<int>(); // 현재 받을수있는 퀘스트 목록 
    public List<int> _progressQuest = new List<int>(); // 현재 진행중인 퀘스트 목록 // 필수 저장
    public List<int> _completeQuest = new List<int>(); // 완료한 퀘스트 목록 // 필수 저장
    public Action _curLevelCountPlus;
    public Action _completeCheck;
    DataTableManager _dataTableManager;
    public Define.QuestInput _questInput; // 퀘스트창 오픈 시 진행중 or 진행가능한 창을 판단하기 위한 enum
    public int _questTextID; // 메인화면 작은 퀘스트창에 들어가는 퀘스트텍스트의 ID정보를 전달하기위한 변수
    public void Init()
    {
        _dataTableManager = Managers.DataTable;
        // LoadQuestData();
        QuestListInput();
        AddActiveQuest();
        _curLevelCountPlus += LevelCountPlus;
        
    }

    /////////////////////////////////////
    ///여기 밑부터 내가 작성한 부분
    public void LevelCountPlus()
    {
        _currPlayerLevel++;
        AddActiveQuest();
    }
    public void AddActiveQuest()
    {
        for (int i = _questID[0]; i <= _questID[_questID.Count - 1]; i++)
        {
            if (_questList[i] <= _currPlayerLevel)
            {
                if (!_progressQuest.Contains(i) && !_completeQuest.Contains(i) && !_activeQuest.Contains(i))
                {
                    _activeQuest.Add(i);
                }



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
            _targetToQuestID.Add(questdata.TargetID, questdata.ID);
            _targetCheck.Add(questdata.ID, questdata.TargetID);
            _completeChecks.Add(questdata.ID, questdata.TargetCount);
            foreach (var monsterdata in _dataTableManager._MonsterDropData)
            {
                if (questdata.TargetID == monsterdata.ID && !_targetName.ContainsKey(questdata.ID))
                {
                    _targetName.Add(questdata.ID, monsterdata.Name);
                }
            }
            foreach (var itemdata in _dataTableManager._GoodsItemData)
            {
                if (questdata.TargetID == itemdata.ID && !_targetName.ContainsKey(questdata.ID))
                {
                    _targetName.Add(questdata.ID, itemdata.Name);
                }
            }
        }
    }
}