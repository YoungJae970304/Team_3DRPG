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
    public Dictionary<GameObject, int> _changeID = new Dictionary<GameObject, int>(); //딕셔너리 변경용 딕셔너리
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
    public GameObject _simpleQuestUI;
    public GameObject _content;
    MainUI mainUI;
    public Define.QuestInput _questInput; // 퀘스트창 오픈 시 진행중 or 진행가능한 창을 판단하기 위한 enum
    public int _questTextID; // 메인화면 작은 퀘스트창에 들어가는 퀘스트텍스트의 ID정보를 전달하기위한 변수
    public void Init()
    {
        _dataTableManager = Managers.DataTable;
    }

    /////////////////////////////////////
    ///여기 밑부터 내가 작성한 부분
    public void LevelCountPlus()
    {
        AddActiveQuest();
    }
    public void AddActiveQuest()
    {
        _currPlayerLevel = Managers.Game._player._playerStatManager.Level;
        for (int i = _questID[0]; i <= _questID[_questID.Count - 1]; i++)
        {
            if (_questList[i] <= _currPlayerLevel)
            {
                if (!_progressQuest.Contains(i) && !_completeQuest.Contains(i) && !_activeQuest.Contains(i))
                {
                    _activeQuest.Add(i);
                }
            }
        }
    }
    public void CheckProgress(int progressValue)
    {
        int currentint;
        mainUI = Managers.UI.GetActiveUI<MainUI>() as MainUI;
        _simpleQuestUI = Util.FindChild(mainUI.gameObject, "SimpleQuestUI");
        _content = Util.FindChild(_simpleQuestUI, "QuestInfo");
        if (Managers.QuestManager._countCheck.ContainsKey(progressValue))
        {
            currentint = Managers.QuestManager._countCheck[progressValue];
        }
        else
        {
            currentint = 0; // 초기화
            Managers.QuestManager._countCheck.Add(progressValue, currentint);
        }

        if (currentint < Managers.QuestManager._completeChecks[progressValue])//컴플리트 체크 변수 수정 필요 // 완
        {
            currentint++;
            Managers.QuestManager._countCheck[progressValue] = currentint;
            if (!Managers.QuestManager._changeText.ContainsKey(progressValue))
            {
                Managers.QuestManager._changeText.Add(progressValue, _content.transform.GetChild(2).gameObject);
                int lastID = Managers.QuestManager._changeID[_content.transform.GetChild(2).gameObject];
                Managers.QuestManager._changeID.Remove(Managers.QuestManager._changeText[lastID]);
                Managers.QuestManager._changeText.Remove(lastID);
                Managers.QuestManager._changeID.Add(_content.transform.GetChild(2).gameObject, progressValue);
                Managers.QuestManager._changeText[progressValue].GetComponent<SimpleQuestText>()._questTextID = progressValue;
                Managers.QuestManager._changeText[progressValue].transform.SetAsFirstSibling();
                Managers.QuestManager._changeText[progressValue].GetComponent<SimpleQuestText>().Init(Util.FindChild(_simpleQuestUI, "QuestInfo").transform);
            }
            else
            {
                Managers.QuestManager._changeText[progressValue].GetComponent<SimpleQuestText>().Init(Util.FindChild(_simpleQuestUI, "QuestInfo").transform);
            }
            if (currentint == Managers.QuestManager._completeChecks[progressValue])
            {
                Managers.QuestManager._changeText[progressValue].GetComponent<SimpleQuestText>().Init(Managers.QuestManager._changeText[progressValue].transform);
                PubAndSub.UnSubscrib<int>($"{progressValue}", CheckProgress);
                Managers.QuestManager._questComplete[progressValue] = true;
            }
        }
    }
    public void QuestListInput()
    {
        
        foreach (var questdata in _dataTableManager._QuestData)
        {
            _questList.Add(questdata.ID, questdata.PlayerLevelRequirement);
            _questName.Add(questdata.ID, questdata.Name);
            _questID.Add(questdata.ID);
            _targetToQuestID.Add(questdata.TargetID, questdata.ID);
            _targetCheck.Add(questdata.ID, questdata.TargetID);
            _completeChecks.Add(questdata.ID, questdata.TargetCount);
            _questComplete.Add(questdata.ID, false);
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