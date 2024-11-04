using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCompleteValue : MonoBehaviour
{
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
    public int _questRewardType3;
    public int _questRewardValue3;
    #endregion
    DataTableManager _dataTableManager;
    public Dictionary<int, int> _completeCheck = new Dictionary<int, int>();
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        _dataTableManager = Managers.DataTable;
        QuestUITest(int.Parse(gameObject.name));
    }
    public void QuestUITest(int ID)
    {
        foreach (var questdata in _dataTableManager._QuestData) //추후 버튼으로 뺄 파트
        {
            if (questdata == null)
            {
                Logger.LogError("퀘스트 값안들어간다");
                return;
            }
            if (questdata.ID == ID) //퀘스트아이디가 돌아가고있는 foreach문의 id와 같다면
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
                if (!_completeCheck.ContainsKey(_questID) && !Managers.QuestManager._targetCheck.ContainsKey(_questID))
                {
                    _completeCheck.Add(_questID, _targetCount);
                    Managers.QuestManager._targetCheck.Add(_questID, _targetID);
                }


                break;
            }
        }//여기까지 뺄 파트
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
