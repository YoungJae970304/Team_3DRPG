using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class QuestManager
{
    //현재 플레이어 레벨 // 사용중
    public int _currPlayerLevel = 1;
    //현재 활성화된 퀘스트 리스트
    public List<Quest> _ActiveQuests = new List<Quest>();
    //전체 퀘스트를 일단 가지고있는 리스트
    public List<QuestData> _AllQuestData = new List<QuestData>();
    public bool _metRequireLevel = false;
    ///////////////////////// 여기 밑부터 새로 작성한 부분
    public Dictionary<int,int> _questList = new Dictionary<int,int>();
    public Dictionary<int,int> _questrequirements = new Dictionary<int, int>();
    public List<int> _questID = new List<int>();
    public List<int> _activeQuest = new List<int>();
    public Action _curLevelCountPlus;
    public Action _completeCheck;
    DataTableManager _dataTableManager;
    public Define.QuestInput _questInput;
    public void Init()
    {
        _dataTableManager = Managers.DataTable;
       // LoadQuestData();
        _curLevelCountPlus += LevelCountPlus;
        
    }
    
    //퀘스트 데이터를 매니저에서 다시 불러와줌
    void LoadQuestData()
    {
        var questData = Managers.DataTable._QuestData;

        _AllQuestData.AddRange(questData);
    }

    //시작 메서드
    public void OnStartQuest(int id)
    {
        QuestData questData = _AllQuestData.Find(q => q.ID == id);
        //현재 수락 가능한 상태인지 체크
        CheckUnlockQuest();
        if (questData != null)
        {
            // 퀘스트 시작 로직
            Logger.Log("퀘스트를 수락");
            
            //수락 받으면 그 받은 퀘스트를 디스플레이에 표시하기
            //여러개 받으면 예외처리 해야함
            QuestDisplay questHUDInfoUI = Managers.UI.GetActiveUI<QuestDisplay>() as QuestDisplay;

            if(questHUDInfoUI == null)
            {
                Managers.UI.OpenUI<QuestDisplay>(new BaseUIData());
            }
        }
    }

    //시작 가능 체크
    public bool CheckUnlockQuest()
    {
        int playeLevel = Managers.Game._player._playerStatManager.Level;

        foreach (var quest in _AllQuestData)
        {
            //현재 플레이어 레벨이 데이터안에있는 시작 가능레벨로 설정
            _currPlayerLevel = quest.PlayerLevelRequirement;
            //실제 플레이어 레벨이 데이터안에있는 시작가능 레벨보다 크거나 작으면
            if (playeLevel >= _currPlayerLevel)
            {
                //퀘스트 시작 가능
                _metRequireLevel = true;
            }
            else
            {
                _metRequireLevel = false;
            }
        }
        return _metRequireLevel;
    }

    //진행 메서드
    public void OnAdvanceQuest(int targetId, int amount)
    {
        foreach (var quest in _AllQuestData)
        {
            if (quest.PlayerLevelRequirement == targetId)
            {

            }
        }
    }

    //완료 메서드
    public void OnFinishQuest(int id)
    {
        //보상 처리

    }
    /////////////////////////////////////
    ///여기 밑부터 내가 작성한 부분
    public void LevelCountPlus()
    {
        _currPlayerLevel++;
        for (int i = _questID[0]; i < _questID.Count; i++)
        {
            if(_questList[i] <= _currPlayerLevel)
            {
                _activeQuest.Add(_questList[i]);
                Managers.UI.Init();
            }
        }
    }
    
    void QuestListInput()
    {
        foreach(var questdata in _dataTableManager._QuestData)
        {
            _questList.Add(questdata.ID, questdata.PlayerLevelRequirement);
            _questID.Add(questdata.ID);
            _questrequirements.Add(questdata.ID, questdata.TargetCount);
        }
    }
}