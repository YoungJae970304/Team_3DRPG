using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : BaseUI
{
    
    enum QuestInfoButton
    {
        ExitBtn,
    }
    enum NowQuestText
    {
        QuestName,
        QuestInfo,
        TargetCount,
        Count,
    }
    enum RewardImage
    {
        ItemReward,
        GoldReward,
        ExpReward,
        Count,
    }
    #region 데이터받아올변수
    public int _questID;
    public Define.QuestType _questType;
    public string _questName;
    public string _questInfo;
    public int _requirement;
    public int _targetID;
    public int _targetCount;
    public QuestData.RewardType _questRewardType1;
    public int _questRewardValue1;
    public QuestData.RewardType _questRewardType2;
    public int _questRewardValue2;
    public QuestData.RewardType _questRewardType3;
    public int _questRewardValue3;
    #endregion
    DataTableManager _dataTableManager;
    public bool _makeCheck = false;
    public bool _activeObject = false;
    public Define.QuestInput _questInput;
    public List<GameObject> _questObject = new List<GameObject>();
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        _questInput = Managers.QuestManager._questInput;
        _dataTableManager = Managers.DataTable;
        _activeObject = false;
        AddList();
        if (!_makeCheck)
        {

        }
        else
        {

        }
        //여기에 리스트같은데에서 퀘스트 받아와서 버튼 생성되도록
    }
    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);
        Bind<TextMeshProUGUI>(typeof(NowQuestText));
        Bind<Image>(typeof(RewardImage));
        GetButton((int)QuestInfoButton.ExitBtn).onClick.AddListener(() => CloseUI());
        //이미지 바꿀 함수
    }
    public void AddList()
    {
        for (int i = 0; i < (int)NowQuestText.Count; i++)
        {
            _questObject.Add(Get<TextMeshProUGUI>(i).gameObject);
        }
        for (int i = 0; i < (int)RewardImage.Count; i++)
        {
            _questObject.Add(Get<Image>(i).gameObject);
        }
        for (int i = 0; i < _questObject.Count; i++)
        {
            _questObject[i].SetActive(false);
        }
    }
    public void MakeButton()
    {
        switch (_questInput)
        {
            case Define.QuestInput.Q:

                _makeCheck = true;
                break;
            case Define.QuestInput.Dialog:

                _makeCheck = true;
                break;
        }
    }
    public void OpenProgressQuest()
    {

    }
    public void PossibleQuest()
    {

    }
    public void QuestUITest(int ID)
    {
        if (!_activeObject)
        {
            for (int i = 0; i < _questObject.Count; i++)
            {
                _questObject[i].SetActive(true);
            }
        }
            
        
        
        //아이템 데이터 테이블에서 ID에 맞는 아이템 찾기
        foreach (var questdata in _dataTableManager._QuestData)
        {
            if (questdata == null)
            {
                Logger.LogError("퀘스트 값안들어간다");
                return;
            }

            //Logger.LogError($"{dungeonType.ID},{ID} 다른가?");
            if (questdata.ID == ID) //던전아이디가 돌아가고있는 foreach문의 id와 같다면
            {
                _questID = questdata.ID;
                _questType = questdata.Type;
                _questName = questdata.Name;
                _questInfo = questdata.Info;
                _requirement = questdata.PlayerLevelRequirement;
                _targetID = questdata.TargetID;
                _targetCount = questdata.TargetCount;
                _questRewardType1 = questdata.RewardType1;
                _questRewardValue1 = questdata.RewardValue1;
                _questRewardType2 = questdata.RewardType2;
                _questRewardValue2 = questdata.RewardValue2;
                _questRewardType3 = questdata.RewardType3;
                _questRewardValue3 = questdata.RewardValue3;
                break;
            }
        }
        
        Get<TextMeshProUGUI>((int)NowQuestText.QuestName).text = _questName;
        Get<TextMeshProUGUI>((int)NowQuestText.QuestInfo).text = _questInfo;
        Get<TextMeshProUGUI>((int)NowQuestText.TargetCount).text = _targetCount.ToString();
        GetImage((int)RewardImage.ItemReward).sprite = Managers.Resource.Load<Sprite>($"{_questRewardValue1}");//밑에 추후 경로 입력
        GetImage((int)RewardImage.ItemReward).sprite = Managers.Resource.Load<Sprite>($"{_questRewardValue2}");
        GetImage((int)RewardImage.ItemReward).sprite = Managers.Resource.Load<Sprite>($"{_questRewardValue3}");
        //지석님께 여쭤보고 비활성화 하는 방안도 생각해야할듯
        _activeObject = true;//위치 고민해봐야할듯
    }
}
