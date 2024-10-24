using UnityEngine;

public class CollectQuest : Quest
{
    DataTableManager _dataTable;
    QuestState.State _questState;

    public CollectQuest(QuestData questData) : base(questData)
    {
        SetSatus(_questState = QuestState.State.InProgress);
    }

    private void Start()
    {
        _dataTable = Managers.DataTable;
    }

    public void CollectedItem()
    {
        foreach (var data in _dataTable._QuestData)
        {
            if (_currentProgress < data.TargetCount)
            {
                GetSatus();
                //갱신 함수
                _currentProgress++;
            }
            if (_currentProgress >= data.TargetCount)
            {
                //최종 완료 함수 호출
                IsCompleted();
                GetSatus();
                if (data.Type == Define.QuestType.Sub)
                {
                    ResetSubQuest();
                    SetSatus(_questState = QuestState.State.Finished);
                }
            }
        }
    }
}
