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

    public void Init()
    {
        LoadQuestData();
    }

    //퀘스트 데이터를 매니저에서 다시 불러와줌
    void LoadQuestData()
    {
        var questData = Managers.DataTable._QuestData;

        _AllQuestData.AddRange(questData);
        Logger.LogWarning($"리스트 확인{questData}");
    }

    //시작 메서드
    public void OnStartQuest(int id)
    {
        QuestData questData = _AllQuestData.Find(q => q.ID == id);
        //현재 수락 가능한 상태인지 체크
        if (questData != null)
        {
            // 퀘스트 시작 로직
            Logger.Log("퀘스트를 수락");
            //현재 시작가능한 퀘스트를 새로운 퀘스트로 시작
            _ActiveQuests.Add(new Quest(questData));
            
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

    ////시작 가능 체크
    //public bool CheckUnlockQuest()
    //{
    //    int playeLevel = Managers.Game._player._playerStatManager.Level;

    //    bool metRequireLevel = false;

    //    foreach (var quest in _AllQuestData)
    //    {
    //        //현재 플레이어 레벨이 데이터안에있는 시작 가능레벨로 설정
    //        _currPlayerLevel = quest.PlayerLevelRequirement;
    //        //실제 플레이어 레벨이 데이터안에있는 시작가능 레벨보다 크거나 작으면
    //        if (playeLevel >= _currPlayerLevel)
    //        {
    //            //퀘스트 시작 가능
    //            metRequireLevel = true;
    //        }
    //    }
    //    return metRequireLevel;
    //}

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

}