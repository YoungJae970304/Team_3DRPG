using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        RewardText,
    }
    enum RewardImage
    {
        ItemReward,
        GoldReward,
        ExpReward,
    }
    enum Buttons
    {
        AllowBtn,
        GiveupBtn,
        CompleteBtn,
        //ExitBtn,

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
    public int _questRewardType3;
    public int _questRewardValue3;
    #endregion
    DataTableManager _dataTableManager;
    public bool _activeObject = false;
    public Define.QuestInput _questInput;
    public List<GameObject> _questObject = new List<GameObject>();
    public List<GameObject> _uiButtons = new List<GameObject>();
    public List<GameObject> _questButtons = new List<GameObject>();
    public Dictionary<string, int> _buttonType = new Dictionary<string, int>();
    public Dictionary<string, int> _progressButtonType = new Dictionary<string, int>();
    public Dictionary<int, int> _completeCheck = new Dictionary<int, int>();
    public Dictionary<int, bool> _questComplete = new Dictionary<int, bool>();
    public Dictionary<int, GameObject> _changeText = new Dictionary<int, GameObject>();
    public GameObject _questView;
    public GameObject _simpleQuestUI;
    GameObject _simpleText;
    public int _test;
    MainUI mainUI;
    Player _player;
    Inventory _inventory;
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        mainUI = Managers.UI.GetActiveUI<MainUI>() as MainUI;
        _simpleQuestUI = Util.FindChild(mainUI.gameObject, "SimpleQuestUI");
        _player = Managers.Game._player;
        _inventory = _player.gameObject.GetOrAddComponent<Inventory>();
        ClearList();
        _questButtons.Clear();
        _questInput = Managers.QuestManager._questInput;
        _dataTableManager = Managers.DataTable;
        _activeObject = false;
        Bind<TextMeshProUGUI>(typeof(NowQuestText));
        Bind<Image>(typeof(RewardImage));
        Bind<Button>(typeof(Buttons));
        AddList();
        ButtonSet();
        MakeButton(_questInput);

        //GetButton((int)Buttons.ExitBtn).onClick.AddListener(() => CloseUI());
        GetButton((int)Buttons.AllowBtn).onClick.RemoveAllListeners();
        GetButton((int)Buttons.GiveupBtn).onClick.RemoveAllListeners();
        GetButton((int)Buttons.CompleteBtn).onClick.RemoveAllListeners();
        GetButton((int)Buttons.AllowBtn).onClick.AddListener(() => AllowQuest());
        GetButton((int)Buttons.GiveupBtn).onClick.AddListener(() => GiveUpQuest());
        GetButton((int)Buttons.CompleteBtn).onClick.AddListener(() => CompleteQuest());
        //여기에 리스트같은데에서 퀘스트 받아와서 버튼 생성되도록 //완
    }
    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);


        //이미지 바꿀 함수 //완
    }
    private void OnDisable()
    {
        //ClearList();
    }
    public void ClearList()
    {
        for (int i = 0; i < _questButtons.Count; i++)
        {
            Managers.Resource.Destroy(_questButtons[i]);
        }

    }
    public void ButtonSet()
    {
        _uiButtons.Add(GetButton((int)Buttons.AllowBtn).gameObject);
        _uiButtons.Add(GetButton((int)Buttons.GiveupBtn).gameObject);
        _uiButtons.Add(GetButton((int)Buttons.CompleteBtn).gameObject);
        for (int i = 0; i < _uiButtons.Count; i++)
        {
            _uiButtons[i].SetActive(false);
        }
    }
    public void AddList()
    {
        for (int i = 0; i <= (int)NowQuestText.RewardText; i++)
        {
            _questObject.Add(Get<TextMeshProUGUI>(i).gameObject);
        }
        for (int i = 0; i <= (int)RewardImage.ExpReward; i++)
        {
            _questObject.Add(Get<Image>(i).gameObject);
        }
        for (int i = 0; i < _questObject.Count; i++)
        {
            _questObject[i].SetActive(false);
        }
    }
    public void MakeButton(Define.QuestInput input)
    {
        input = _questInput;
        switch (input)
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
        //_questButtons.Clear();
        //Logger.LogError($"{Managers.QuestManager._activeQuest.Count}몇이니");
        for (int i = 0; i < Managers.QuestManager._activeQuest.Count; i++)
        {

            possibleQuest = Managers.Resource.Instantiate("UI/QuestListBtn", _questView.transform);
            possibleQuest.GetOrAddComponent<QuestButton>()._questID = Managers.QuestManager._activeQuest[i];
            possibleQuest.GetOrAddComponent<Poolable>();
            possibleQuest.name = Managers.QuestManager._activeQuest[i].ToString();
            possibleQuest.GetComponentInChildren<TextMeshProUGUI>().text = Managers.QuestManager._questName[Managers.QuestManager._activeQuest[i]];
            // possibleQuest.GetComponentInChildren<TextMeshProUGUI>().text = 
            _questButtons.Add(possibleQuest);
            if (_buttonType.ContainsKey(Managers.QuestManager._activeQuest[i].ToString()))
            {
                continue;
            }
            _buttonType.Add(possibleQuest.name, Managers.QuestManager._activeQuest[i]);

        }
        TransformSort(_questView);
        yield return new WaitForSeconds(0.5f);
        OpenPossibleQuestBtnListner();
        yield break;
    }
    public void OpenPossibleQuestBtnListner()
    {
        //Logger.LogError(_questButtons.Count.ToString() + "sdafa");
        for (int i = 0; i < _questButtons.Count; i++)
        {
            if (_questButtons[i] != null)
            {
                _questButtons[i].GetComponent<Button>().onClick.AddListener(() => QuestUITest(_buttonType[ButtonName()]));
            }

        }
    }
    public void AllowQuest()
    {
        //Logger.LogError("진입");

        //퀘스트 수락을 어떻게 처리할지
        //일단 예상가는건 수락 시 리스트에서 불러와서 그 리스트에서 수락한 퀘스트 삭제//완
        //그 후 진행중인 퀘스트 리스트에 수락한 퀘스트 아이디 추가//완
        GameObject test = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        QuestButton testID = test.GetComponent<QuestButton>();
        _test = testID._questID;
        if (!Managers.QuestManager._progressQuest.Contains(_test))
        {
            Managers.QuestManager._progressQuest.Add(_test);
            Managers.QuestManager._progressQuest.Sort();
        }
        if (Managers.QuestManager._activeQuest.Contains(_test))
        {
            Managers.QuestManager._activeQuest.Remove(_test);
            Managers.QuestManager._activeQuest.Sort();
        }

        if (!_questComplete.ContainsKey(_test))
        {
            _questComplete.Add(_test, false);
        }
        //_questComplete.Add(_test, false);
        //수락한 버튼 삭제 // 완
        //Logger.LogError($"{_test}이름 확인");
        PubAndSub.Subscrib<int>($"{_test}", CheckTest);
        
        //몬스터 죽는쪽에 몬스터아이디랑 타켓이름이랑 비교해서 같다면 실행되도록 하면될듯 // 완
        //중간 관리 액션으로 할지 아니면 추가 액션을 하나 더할지 고민은 해야할듯 // 완

        if (!_simpleQuestUI.activeSelf)
        {
            _simpleQuestUI.SetActive(true);

            GameObject content = Util.FindChild(_simpleQuestUI, "QuestInfo");
            if (content.transform.childCount < 3)
            {
                Managers.QuestManager.test123 = _test;
                _simpleText = Managers.Resource.Instantiate("UI/SimpleQuestText", content.transform);
                _changeText.Add(_test, _simpleText);
                var text = _simpleText.GetOrAddComponent<SimpleQuestText>();
                if (!Managers.QuestManager._countCheck.ContainsKey(_test))
                {
                    Managers.QuestManager._countCheck.Add(_test, 0);
                }
                text.Init(content.transform);
                //퀘스트 생성
                //이 조건은 완료문에도 해야함
            }
            
            //값 전달하는부분
        }
        else
        {
            _simpleQuestUI.SetActive(true);

            GameObject content = Util.FindChild(_simpleQuestUI, "QuestInfo");
            if (content.transform.childCount < 3)
            {
                Managers.QuestManager.test123 = _test;
                _simpleText = Managers.Resource.Instantiate("UI/SimpleQuestText", content.transform);
                //퀘스트 생성
                //이 조건은 완료문에도 해야함
                _changeText.Add(_test, _simpleText);
                var text = _simpleText.GetOrAddComponent<SimpleQuestText>();
                if (!Managers.QuestManager._countCheck.ContainsKey(_test))
                {
                    Managers.QuestManager._countCheck.Add(_test, 0);
                }

                text.Init(content.transform);
            }
           
        }
        Init(transform);
    }
    public void OpenQuestUI<T>() where T : BaseUI
    {
        T questUI = Managers.UI.GetActiveUI<T>() as T;
        if (questUI == null)
        {
            BaseUIData baseUIData = new BaseUIData();
            Managers.UI.OpenUI<T>(baseUIData);
        }
    }

    public void CheckTest(int test)
    {
        //위에 두줄 수정필요
        int currentint;
        /*if (Managers.QuestManager._countCheck.Count == 0)
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
        }*/
        // _countCheck에 test 인덱스가 존재하는지 확인
        if (Managers.QuestManager._countCheck.ContainsKey(test))
        {
            currentint = Managers.QuestManager._countCheck[test];
        }
        else
        {
            currentint = 0; // 초기화
            Managers.QuestManager._countCheck.Add(test, currentint);
            Logger.LogError(Managers.QuestManager._countCheck[test].ToString() + "제대로 들어가나 테스트");
        }

        if (currentint < _completeCheck[test])//컴플리트 체크 변수 수정 필요 // 완
        {
            currentint++;
            Managers.QuestManager._countCheck[test] = currentint;
            _changeText[test].GetComponent<SimpleQuestText>().Init(_changeText[test].transform);
            if (currentint == _completeCheck[test])
            {
                _changeText[test].GetComponent<SimpleQuestText>().Init(_changeText[test].transform);
                //컴플리트 버튼 활성화, 포기 버튼 비활성화 조건 만들기 // 완
                PubAndSub.UnSubscrib<int>($"{test}", CheckTest);
                _questComplete[test] = true;
                //조건 활성화 후 구독 해제 // 완
                //좌측에 생성되는 버튼에 불변수 만들어놓고 false로 해뒀다가 조건맞추면 true로 변경되게하면될듯? // 완
            }
            //UI에 표시될 변수는 어떻게할건가 //완
        }
        else
        {
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
            progressQuest.GetOrAddComponent<QuestButton>()._questID = Managers.QuestManager._progressQuest[i];
            progressQuest.GetOrAddComponent<Poolable>();
            progressQuest.name = Managers.QuestManager._progressQuest[i].ToString();
            progressQuest.GetComponentInChildren<TextMeshProUGUI>().text = Managers.QuestManager._questName[Managers.QuestManager._progressQuest[i]];
            _questButtons.Add(progressQuest);
            if (_progressButtonType.ContainsKey(Managers.QuestManager._progressQuest[i].ToString()))
            {
                continue;
            }
            _progressButtonType.Add(progressQuest.name, Managers.QuestManager._progressQuest[i]);

        }
        TransformSort(_questView);
        yield return new WaitForSeconds(0.5f);
        OpenProgressQuestBtnBind();
        yield break;
    }
    public void OpenProgressQuestBtnBind()
    {
        //위의 버튼 바인드
        for (int i = 0; i < Managers.QuestManager._progressQuest.Count; i++)
        {
            _questButtons[i].GetComponent<Button>().onClick.AddListener(() => QuestUITest(_progressButtonType[ButtonName()]));
        }
    }
    public void CompleteQuest()
    {
        Logger.LogError("몇번들어가는지");
        GameObject test = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        QuestButton testID = test.GetComponent<QuestButton>();
        _test = testID._questID;
        if (!Managers.QuestManager._completeQuest.Contains(_test))
        {
            Managers.QuestManager._completeQuest.Add(_test);
            Managers.QuestManager._completeQuest.Sort();
        }
        Managers.QuestManager._progressQuest.Remove(_test);
        Managers.QuestManager._progressQuest.Sort();
        if(Managers.QuestManager._targetCheck[_test]/10000 != 9)
        {
            for(int i = 0; i < Managers.QuestManager._countCheck[_test]; i++)
            {
                _inventory.Remove(_inventory.GetItemToId(Managers.QuestManager._targetCheck[_test]));
            }
            
        }
        
        _player.PlayerEXPGain(_questRewardValue2);//추후 지석님께 여쭤보고 변경
        _player.PlayerGOLDGain(_questRewardValue1);//추후 지석님께 여쭤보고 변경
        Logger.LogError($"{_questRewardValue3} 밸류갑 제대로 들어가?");
        for(int i = 0; i < _questRewardValue3; i++)
        {
            Logger.LogError(_questRewardType3.ToString() + "포션값");
            Item questItem = Item.ItemSpawn((int)_questRewardType3);
            _inventory.InsertItem(questItem);
            Logger.LogError($"포션생성{i}");
        }
       
        PubAndSub.UnSubscrib<int>($"{_test}", CheckTest);
        if (_simpleQuestUI.activeSelf)
        {
            Managers.Resource.Destroy(_changeText[_test]);
            _changeText.Remove(_test);
            GameObject content = Util.FindChild(_simpleQuestUI, "QuestInfo");
            if (content.transform.childCount == 1)
            {
                _simpleQuestUI.SetActive(false);
            }
            Init(transform);
        }

        Managers.Sound.Play("ETC/ui_quest_clear");
    }
    public void GiveUpQuest()
    {
        GameObject test = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        QuestButton testID = test.GetComponent<QuestButton>();
        _test = testID._questID;
        Managers.QuestManager._progressQuest.Remove(_test);
        Managers.QuestManager._progressQuest.Sort();
        if (!Managers.QuestManager._activeQuest.Contains(_test))
        {
            Managers.QuestManager._activeQuest.Add(_test);
            Managers.QuestManager._activeQuest.Sort();
        }
        //Logger.LogError($"{_getProgressButtons[_test].name}이름뭐냐");
        Logger.LogError($"{_test}이름 확인2");
        PubAndSub.UnSubscrib<int>($"{_test}", CheckTest);
        Managers.QuestManager._countCheck[_test] = 0;
        //포기한 버튼 삭제


        if (_simpleQuestUI.activeSelf)
        {

            Managers.Resource.Destroy(_changeText[_test]);
            _changeText.Remove(_test);
            GameObject content = Util.FindChild(_simpleQuestUI, "QuestInfo");
            if (content.transform.childCount == 1)
            {
                _simpleQuestUI.SetActive(false);
                //simplequestui해당하는거 삭제
            }
        }


        Init(transform);
    }
    public string ButtonName()
    {
        return UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
    }
    public void QuestUITest(int ID)
    {
        //Logger.LogError($"{ID}들어가는 아이디 확인");
        if (!_activeObject)
        {
            for (int i = 0; i < _questObject.Count; i++)
            {
                _questObject[i].SetActive(true);
            }
        }
        if (_questInput == Define.QuestInput.Q)
        {
            if (!_questComplete[ID])
            {
                _uiButtons[1].SetActive(true);

            }
            else
            {
                _uiButtons[2].SetActive(true);
            }
        }
        else
        {
            _uiButtons[0].SetActive(true);
        }
        for (int i = 0; i < _uiButtons.Count; i++)
        {
            if (_uiButtons[i].activeSelf)
            {
                _uiButtons[i].GetOrAddComponent<QuestButton>()._questID = ID;
            }
        }


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
        Get<TextMeshProUGUI>((int)NowQuestText.QuestName).text = _questName;
        Get<TextMeshProUGUI>((int)NowQuestText.QuestInfo).text = _questInfo;
        if (Managers.QuestManager._countCheck.ContainsKey(ID))
        {
            Get<TextMeshProUGUI>((int)NowQuestText.TargetCount).text = $"{Managers.QuestManager._countCheck[ID]} / {_targetCount}";
        }
        else
        {
            Get<TextMeshProUGUI>((int)NowQuestText.TargetCount).text = $"{0} / {_targetCount}";
        }
       
        GetImage((int)RewardImage.ItemReward).sprite = Managers.Resource.Load<Sprite>($"{_questRewardValue1}");//밑에 추후 경로 입력
        GetImage((int)RewardImage.ItemReward).sprite = Managers.Resource.Load<Sprite>($"{_questRewardValue2}");
        GetImage((int)RewardImage.ItemReward).sprite = Managers.Resource.Load<Sprite>($"{_questRewardValue3}");
        _activeObject = true;//위치 고민해봐야할듯
    }
    void TransformSort(GameObject go)
    {
        // 현재 오브젝트의 모든 자식 Transform을 가져옵니다.
        List<Transform> children = new List<Transform>();

        foreach (Transform child in go.transform)
        {
            children.Add(child);
        }

        // 자식 이름으로 정렬합니다.
        children = children.OrderBy(child => child.name).ToList();

        // 정렬된 자식들의 이름을 출력
        foreach (Transform child in children)
        {
            //Debug.Log(child.name);
        }

        // 정렬된 순서로 자식들을 다시 설정 (선택 사항)
        for (int i = 0; i < children.Count; i++)
        {
            children[i].SetSiblingIndex(i);
        }
    }
    public override void CloseUI(bool isCloseAll = false)
    {
        base.CloseUI(isCloseAll);
    }
}
