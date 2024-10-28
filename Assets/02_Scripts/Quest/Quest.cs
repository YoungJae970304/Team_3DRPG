//퀘스트 상태 클래스
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

//퀘스트 클래스
public class Quest
{
    //퀘스트 데이터의 퀘스트 정보
    public QuestData _QuestData { get; set; }

    //현재 퀘스트 진행 상황
    public int _currentProgress { get; set; }

    //현재 퀘스트의 상태 (기본적으로 불가능 상태로 설정)
    QuestState.State _currentState = QuestState.State.RequirementNot;

    

    //새로운 퀘스트를 초기화하는 생성자
    public Quest(QuestData questData)
    {
        _QuestData = questData;
        //초기 진행 상황
        _currentProgress = 0;
        //레벨이 충족 되었을 때 새로운 퀘스트로 생성시켜줄거니까 CanStart로 설정
        _currentState = QuestState.State.CanStart;
    }

    //현재 상태를 가져올 함수
    public QuestState.State GetSatus()
    {
        return _currentState;
    }

    //현재 상태를 설정하는 함수
    public void SetSatus(QuestState.State newState)
    {
        _currentState = newState;
    }

    //퀘스트 완료여부를 판단해줄 함수
    public bool IsCompleted()
    {
        return _currentProgress >= _QuestData.TargetCount;
    }

    //서브퀘스트 반복퀘스트되도록 리셋
    public void ResetSubQuest()
    {
        if (_QuestData.Type == Define.QuestType.Sub)
        {
            _currentProgress = 0;
            SetSatus(QuestState.State.CanStart);
        }
    }

    public static Quest CreateQuest(int id)
    {
        DataTableManager dataTableManager = Managers.DataTable;
        //퀘스트를 생성할 때 Data에 있는 ID로 생성하는데 타입이 Main또는 Sub체크후
        //처치 퀘스트는 일단 메인으로 설정
        //모으기 퀘스트는 서브 퀘스트
        QuestData questData = dataTableManager._QuestData.Find(q => q.ID == id);

        switch (questData.Type)
        {
            case Define.QuestType.Main:
                //메인 퀘스트는 일단 던전별로 몬스터 처치
                return new KillQuest(questData);
            case Define.QuestType.Sub:
                //퀘스트타입이 서브인데컬랙터 퀘스트인지 처치 퀘스트인지 구분지어서 퀘스트를 생성
                return new CollectQuest(questData);
        }

        if (questData == null)
        {
            Logger.LogError($"해당 {id}의 퀘스트를 찾을 수 없습니다");
            return null;
        }
        return new Quest(questData);
    }
}
