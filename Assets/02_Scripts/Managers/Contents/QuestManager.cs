using System.Collections.Generic;
using Unity.VisualScripting;

public class QuestManager
{
    //현재 플레이어 레벨
    public int _currPlayerLevel;
    //현재 활성화된 퀘스트 리스트
    public List<Quest> _ActiveQuests = new List<Quest>();
    //전체 퀘스트를 일단 가지고있는 리스트
    public List<QuestData> _AllQuestData = new List<QuestData>();

    QuestState.State _state = QuestState.State.RequirementNot;

    public void Init()
    {
        LoadQuestData();
    }

    //상태 전환 메서드
    public void OnQuestStateChange(Quest quest)
    {
        switch (_state)
        {
            case QuestState.State.RequirementNot:
                break;
            case QuestState.State.CanStart:

                break;
            case QuestState.State.InProgress:

                break;
            case QuestState.State.CanFinish:

                break;
            case QuestState.State.Finished:

                break;
        }
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
            //현재 시작가능한 퀘스트를 새로운 퀘스트로 시작
            _ActiveQuests.Add(new Quest(questData));
            _state = QuestState.State.InProgress;
            
            //수락 받으면 그 받은 퀘스트를 디스플레이에 표시하기
            //여러개 받으면 예외처리 해야함
            QuestDisplay questHUDInfoUI = Managers.UI.GetActiveUI<QuestDisplay>() as QuestDisplay;

            if(questHUDInfoUI != null)
            {
                Managers.UI.CloseUI(questHUDInfoUI);
            }
            else
            {
                Managers.UI.OpenUI<QuestDisplay>(new BaseUIData());
            }
        }
    }

    //시작 가능 체크
    public bool CheckUnlockQuest()
    {
        int playeLevel = Managers.Game._player._playerStatManager.Level;

        bool metRequireLevel = false;

        foreach (var quest in _AllQuestData)
        {
            //상태가 이미 변경되어서 현재 수락 가능상태라면 더이상 체크하지않게
            if (_state != QuestState.State.RequirementNot) break;
            //현재 플레이어 레벨이 데이터안에있는 시작 가능레벨로 설정
            _currPlayerLevel = quest.PlayerLevelRequirement;
            //실제 플레이어 레벨이 데이터안에있는 시작가능 레벨보다 크거나 작으면
            if (playeLevel >= _currPlayerLevel)
            {
                //퀘스트 시작 가능
                metRequireLevel = true;
                _state = QuestState.State.CanStart;
            }
        }
        return metRequireLevel;
    }

    //진행 메서드
    public void OnAdvanceQuest(int id)
    {
        //현재 진행 상태라면 퀘스트 타입에 따라 퀘스트 조건을 충족 시켜주고 퀘스트를 완료 가능상태로 변경
    }

    //완료 메서드
    public void OnFinishQuest(int id)
    {
        //보상 처리
    }

}