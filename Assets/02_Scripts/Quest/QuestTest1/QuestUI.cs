using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : BaseUI
{

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
    enum Buttons
    {
        AllowBtn,
        GiveupBtn,
        CompleteBtn,
        ExitBtn,

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
    public bool _activeObject = false;
    public Define.QuestInput _questInput;
    public List<GameObject> _questObject = new List<GameObject>();
    public List<GameObject> _uiButtons = new List<GameObject>();
    public List<GameObject> _questButtons = new List<GameObject>();
    public Dictionary<string, int> _buttonType = new Dictionary<string, int>();
    public Dictionary<int, int> _completeCheck = new Dictionary<int, int>();
    public Dictionary<int, bool> _questComplete = new Dictionary<int, bool>();
    public GameObject _questView;
    GameObject _simpleQuestUI;
    GameObject _simpleText;
    public int _test;
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        _questInput = Managers.QuestManager._questInput;
        _dataTableManager = Managers.DataTable;
        _activeObject = false;
        AddList();
        ButtonSet();

        MakeButton();

        GetButton((int)Buttons.ExitBtn).onClick.AddListener(() => CloseUI());
        GetButton((int)Buttons.AllowBtn).onClick.AddListener(() => AllowQuest());
        GetButton((int)Buttons.GiveupBtn).onClick.AddListener(() => GiveUpQuest());

        //여기에 리스트같은데에서 퀘스트 받아와서 버튼 생성되도록 //완
    }
    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);
        Bind<TextMeshProUGUI>(typeof(NowQuestText));
        Bind<Image>(typeof(RewardImage));
        Bind<Button>(typeof(Buttons));

        //이미지 바꿀 함수 //완
    }
    private void OnDisable()
    {
        StartCoroutine(ClearList());
    }
    public IEnumerator ClearList()
    {
        for (int i = 0; i < _questButtons.Count; i++)
        {
            Managers.Resource.Destroy(_questButtons[i]);
        }
        yield return new WaitForSeconds(0.1f);
        _questButtons.Clear();
    }
    public void ButtonSet()
    {
        _uiButtons.Add(GetButton((int)Buttons.AllowBtn).gameObject);
        _uiButtons.Add(GetButton((int)Buttons.GiveupBtn).gameObject);
        _uiButtons.Add(GetButton((int)Buttons.CompleteBtn).gameObject);
        for (int i = 0; i < _uiButtons.Count - 1; i++)
        {
            _uiButtons[i].SetActive(false);
        }
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
                StartCoroutine(OpenProgressQuest());

                break;
            case Define.QuestInput.Dialog:
                StartCoroutine(OpenPossibleQuest());
                break;
        }
    }
    public IEnumerator OpenPossibleQuest()
    {
        GameObject possibleQuest;
        for (int i = 0; i < Managers.QuestManager._activeQuest.Count; i++)
        {
            possibleQuest = Managers.Resource.Instantiate("UI/QuestListBtn", _questView.transform);
            //버튼을 미리 생성시킨걸 로드하는 방식이 날듯?
            possibleQuest.GetOrAddComponent<QuestButton>();
            possibleQuest.GetOrAddComponent<Poolable>();
            possibleQuest.name = Managers.QuestManager._activeQuest[i].ToString();
            _buttonType.Add(possibleQuest.name, Managers.QuestManager._activeQuest[i]);
            _questButtons.Add(possibleQuest);
        }
        yield return new WaitForSeconds(0.5f);
        OpenPossibleQuestBtnBind();
    }
    public void OpenPossibleQuestBtnBind()
    {
        for (int i = 0; i < Managers.QuestManager._activeQuest.Count; i++)
        {
            GetButton(Managers.QuestManager._activeQuest[i]).onClick.AddListener(() => QuestUITest(_buttonType[ButtonName()]));
        }
    }
    public void AllowQuest()
    {
        //퀘스트 수락을 어떻게 처리할지
        //일단 예상가는건 수락 시 리스트에서 불러와서 그 리스트에서 수락한 퀘스트 삭제//완
        //그 후 진행중인 퀘스트 리스트에 수락한 퀘스트 아이디 추가//완
        GameObject test = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        QuestButton testID = test.GetComponent<QuestButton>();
        _test = testID._questID;
        Managers.QuestManager._progressQuest.Add(_test);
        //이거 생각해보니 눌리는 버튼은 퀘스트 수락 거절임. 값 받아오게해야함 //완
        Managers.QuestManager._activeQuest.Remove(_test);
        //수락한 버튼 삭제 // 완
        Managers.Resource.Destroy(GetButton(_test).gameObject);
        PubAndSub.Subscrib<int>($"{_test.ToString()}", CheckTest);
        //몬스터 죽는쪽에 몬스터아이디랑 타켓이름이랑 비교해서 같다면 실행되도록 하면될듯 // 완
        //중간 관리 액션으로 할지 아니면 추가 액션을 하나 더할지 고민은 해야할듯 // 완
        if(Managers.UI.GetActiveUI<SimpleQuestUI>() == null)
        {
           OpenQuestUI<SimpleQuestUI>();
            GameObject simpleUI = Managers.UI.GetActiveUI<SimpleQuestUI>().gameObject;
            GameObject content = Util.FindChild(simpleUI, "QuestInfo");
            if(content.transform.childCount < 3)
            {
                _simpleText = Managers.Resource.Instantiate("UI/SimpleQuestText", content.transform);
                _simpleText.GetComponent<SimpleQuestText>().TextChange(_test);
                //퀘스트 생성
                //이 조건은 완료문에도 해야함
            }
            //값 전달하는부분
        }
    }
    public void OpenQuestUI<T>() where T : BaseUI
    {
        T questUI = Managers.UI.GetActiveUI<T>() as T;
        if(questUI == null)
        {
            BaseUIData baseUIData = new BaseUIData();
            Managers.UI.OpenUI<T>(baseUIData);
        }
    }
    public void CheckTest(int test)
    {
        //위에 두줄 수정필요

        int currentint;
        if (Managers.QuestManager._countCheck.Count == 0)
        {
            currentint = 0;
        }
        else
        {
            currentint = Managers.QuestManager._countCheck[test];
        }
        if (currentint == 0)
        {
            currentint = 0;
            Managers.QuestManager._countCheck.Add(test, currentint);
        }


        if (currentint < _completeCheck[test])//컴플리트 체크 변수 수정 필요 // 완
        {
            currentint++;
            Managers.QuestManager._countCheck[test] = currentint;
            //UI에 표시될 변수는 어떻게할건가 //완
        }
        else
        {
            //컴플리트 버튼 활성화, 포기 버튼 비활성화 조건 만들기 // 완
            PubAndSub.UnSubscrib<int>($"{test}", CheckTest);
            _questComplete[test] = true;
            //조건 활성화 후 구독 해제 // 완
            //좌측에 생성되는 버튼에 불변수 만들어놓고 false로 해뒀다가 조건맞추면 true로 변경되게하면될듯? // 완
        }
    }
    public IEnumerator OpenProgressQuest()
    {
        //진행중인 퀘스트 리스트를 불러와서 생성한 버튼에 이름넣어주기 // 완
        GameObject progressQuest;
        for (int i = 0; i < Managers.QuestManager._progressQuest.Count; i++)
        {
            progressQuest = Managers.Resource.Instantiate("UI/QuestListBtn", _questView.transform);
            progressQuest.GetOrAddComponent<QuestButton>();
            progressQuest.GetOrAddComponent<Poolable>();
            progressQuest.name = Managers.QuestManager._progressQuest[i].ToString();
            _buttonType.Add(progressQuest.name, Managers.QuestManager._progressQuest[i]);
            _questComplete.Add(Managers.QuestManager._progressQuest[i], false);
            _questButtons.Add(progressQuest);
        }
        yield return new WaitForSeconds(0.5f);
        OpenProgressQuestBtnBind();
    }
    public void OpenProgressQuestBtnBind()
    {
        //위의 버튼 바인드
        for (int i = 0; i < Managers.QuestManager._progressQuest.Count; i++)
        {
            GetButton(Managers.QuestManager._progressQuest[i]).onClick.AddListener(() => QuestUITest(_buttonType[ButtonName()]));
        }
    }
    public void GiveUpQuest()
    {
        GameObject test = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        QuestButton testID = test.GetComponent<QuestButton>();
        _test = testID._questID;
        Managers.QuestManager._progressQuest.Remove(_test);
        Managers.QuestManager._activeQuest.Add(_test);
        Managers.Resource.Destroy(GetButton(_test).gameObject);
        PubAndSub.UnSubscrib<int>($"{_test.ToString()}", CheckTest);
        //포기한 버튼 삭제
        if(Managers.QuestManager._progressQuest == null)
        {
            _simpleQuestUI = Managers.UI.GetActiveUI<SimpleQuestUI>().gameObject;
            CloseUI(_simpleQuestUI);
            //simplequestui해당하는거 삭제
        }
    }
    public string ButtonName()
    {
        return UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
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
        if (_questInput == Define.QuestInput.Q)
        {
            if (_questComplete[ID])
            {
                _uiButtons[2].SetActive(true);
            }
            else
            {
                _uiButtons[1].SetActive(true);
            }
        }
        else
        {
            _uiButtons[0].SetActive(true);
        }
        for (int i = 0; i < _uiButtons.Count - 1; i++)
        {
            if (_uiButtons[i].activeSelf)
            {
                _uiButtons[i].GetComponent<QuestButton>()._questID = ID;
            }
        }



        foreach (var questdata in _dataTableManager._QuestData)
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
                _completeCheck.Add(_questID, _targetCount);
                Managers.QuestManager._targetCheck.Add(_questID, _targetID);
                break;
            }
        }

        Get<TextMeshProUGUI>((int)NowQuestText.QuestName).text = _questName;
        Get<TextMeshProUGUI>((int)NowQuestText.QuestInfo).text = _questInfo;
        Get<TextMeshProUGUI>((int)NowQuestText.TargetCount).text = _targetCount.ToString();
        GetImage((int)RewardImage.ItemReward).sprite = Managers.Resource.Load<Sprite>($"{_questRewardValue1}");//밑에 추후 경로 입력
        GetImage((int)RewardImage.ItemReward).sprite = Managers.Resource.Load<Sprite>($"{_questRewardValue2}");
        GetImage((int)RewardImage.ItemReward).sprite = Managers.Resource.Load<Sprite>($"{_questRewardValue3}");
        _activeObject = true;//위치 고민해봐야할듯
    }
}
