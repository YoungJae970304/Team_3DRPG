using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : BaseUI
{
    // UI부분 ,퀘스트 정보 부분 분리
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
    public GameObject _questView;
    public GameObject _simpleQuestUI;
    public GameObject _simpleText;
    public GameObject _content;
    public int _questId;
    MainUI mainUI;
    Player _player;
    Inventory _inventory;
    public int test;
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        ReVoid();
        //여기에 리스트같은데에서 퀘스트 받아와서 버튼 생성되도록 //완
    }
    public void ReVoid()
    {
        mainUI = Managers.UI.GetActiveUI<MainUI>() as MainUI;
        _simpleQuestUI = Util.FindChild(mainUI.gameObject, "SimpleQuestUI");
        _content = Util.FindChild(_simpleQuestUI, "QuestInfo");
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
    }
    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);
        //이미지 바꿀 함수 //완
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
        for (int i = 0; i < Managers.QuestManager._activeQuest.Count; i++)
        {

            possibleQuest = Managers.Resource.Instantiate("UI/QuestListBtn", _questView.transform);
            possibleQuest.GetOrAddComponent<QuestButton>()._questID = Managers.QuestManager._activeQuest[i];
            possibleQuest.GetOrAddComponent<Poolable>();
            possibleQuest.name = Managers.QuestManager._activeQuest[i].ToString();
            possibleQuest.GetComponentInChildren<TextMeshProUGUI>().text = Managers.QuestManager._questName[Managers.QuestManager._activeQuest[i]];
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
        for (int i = 0; i < _questButtons.Count; i++)
        {
            if (_questButtons[i] != null)
            {
                _questButtons[i].GetComponent<Button>().onClick.AddListener(() => QuestUIChange(_buttonType[ButtonName()]));
            }

        }
    }
    public void ValueCheck(int id)
    {
        if (id / 10000 != 8)
        {
            id = Managers.QuestManager._targetToQuestID[id];
        }
        Managers.QuestManager._countCheck[id] = _inventory.GetItemAmount(Managers.QuestManager._targetCheck[id]);
        
        if (_inventory.GetItemAmount(Managers.QuestManager._targetCheck[id]) >= Managers.QuestManager._completeChecks[id])
        {
            Managers.QuestManager._questComplete[id] = true;
        }
        else
        {
            Managers.QuestManager._questComplete[id] = false;
        }
        if (!Managers.QuestManager._changeText.ContainsKey(id))
        {
            
            Managers.QuestManager._changeText.Add(id, _content.transform.GetChild(2).gameObject);
            if (!Managers.QuestManager._changeID.ContainsKey(Managers.QuestManager._changeText[id]))
            {
                Managers.QuestManager._changeID.Add(_content.transform.GetChild(2).gameObject, id);
            }
            
            int lastID = Managers.QuestManager._changeID[_content.transform.GetChild(2).gameObject];
            Managers.QuestManager._changeID.Remove(Managers.QuestManager._changeText[lastID]);
            Managers.QuestManager._changeText.Remove(lastID);
            Managers.QuestManager._changeText[id].GetComponent<SimpleQuestText>()._questTextID = id;
            Managers.QuestManager._changeText[id].GetComponent<SimpleQuestText>().transform.SetAsFirstSibling();
            Managers.QuestManager._changeText[id].GetComponent<SimpleQuestText>().Init(Util.FindChild(_simpleQuestUI, "QuestInfo").transform);
        }
        else
        {
            Managers.QuestManager._changeText[id].GetComponent<SimpleQuestText>().Init(Util.FindChild(_simpleQuestUI, "QuestInfo").transform);
        }
    }
    public void AllowQuest()
    {
        GameObject button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        QuestButton buttonID = button.GetComponent<QuestButton>();
        _questId = buttonID._questID;
        if (!Managers.QuestManager._progressQuest.Contains(_questId))
        {
            Managers.QuestManager._progressQuest.Add(_questId);
            Managers.QuestManager._progressQuest.Sort();
        }
        if (Managers.QuestManager._activeQuest.Contains(_questId))
        {
            Managers.QuestManager._activeQuest.Remove(_questId);
            Managers.QuestManager._activeQuest.Sort();
        }

        PubAndSub.Subscrib<int>($"{_questId}", Managers.QuestManager.CheckProgress);
        if (!_simpleQuestUI.activeSelf)
        {
            _simpleQuestUI.SetActive(true);

            if (_content.transform.childCount < 3)
            {
                Managers.QuestManager._questTextID = _questId;
                _simpleText = null;
                _simpleText = Managers.Resource.Instantiate("UI/SimpleQuestText", _content.transform);
                Managers.QuestManager._changeText.Add(_questId, _simpleText);
                Managers.QuestManager._changeID.Add(_simpleText,_questId);
                var text = _simpleText.GetOrAddComponent<SimpleQuestText>();
                if (!Managers.QuestManager._countCheck.ContainsKey(_questId))
                {
                    Managers.QuestManager._countCheck.Add(_questId, 0);
                }
                if (Managers.QuestManager._targetCheck[_questId] / 10000 != 9)
                {
                    int goodsID = _questId;
                    _inventory.GetItemAction += (() => { ValueCheck(goodsID); });
                    PubAndSub.Subscrib<int>("ItemSell", ((goodsID) => { ValueCheck(goodsID); }));
                    Managers.QuestManager._countCheck[goodsID] = _inventory.GetItemAmount(Managers.QuestManager._targetCheck[goodsID]);
                    if (_inventory.GetItemAmount(Managers.QuestManager._targetCheck[goodsID]) >= Managers.QuestManager._completeChecks[goodsID])
                    {
                        Managers.QuestManager._questComplete[goodsID] = true;
                    }
                }
                Managers.QuestManager._changeText[_questId].GetComponent<SimpleQuestText>().Init(_content.transform);
            }
        }
        else
        {
            _simpleQuestUI.SetActive(true);
            if (_content.transform.childCount < 3)
            {
                Managers.QuestManager._questTextID = _questId;
                _simpleText = null;
                _simpleText = Managers.Resource.Instantiate("UI/SimpleQuestText", _content.transform);
                Managers.QuestManager._changeText.Add(_questId, _simpleText);
                Managers.QuestManager._changeID.Add(_simpleText, _questId);
                var text = _simpleText.GetOrAddComponent<SimpleQuestText>();
                if (!Managers.QuestManager._countCheck.ContainsKey(_questId))
                {
                    Managers.QuestManager._countCheck.Add(_questId, 0);
                }
                if (Managers.QuestManager._targetCheck[_questId] / 10000 != 9)
                {
                    int goodsID = _questId;
                    _inventory.GetItemAction += (() => ValueCheck(goodsID));
                    PubAndSub.Subscrib<int>("ItemSell", ((goodsID) => { ValueCheck(goodsID); }));
                    Managers.QuestManager._countCheck[goodsID] = _inventory.GetItemAmount(Managers.QuestManager._targetCheck[goodsID]);
                    if (_inventory.GetItemAmount(Managers.QuestManager._targetCheck[goodsID]) >= Managers.QuestManager._completeChecks[goodsID])
                    {
                        Managers.QuestManager._questComplete[goodsID] = true;
                    }
                }
                Managers.QuestManager._changeText[_questId].GetComponent<SimpleQuestText>().Init(_content.transform);
            }

        }
        ReVoid();
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

    
    public IEnumerator OpenProgressQuest()
    {
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
            _questButtons[i].GetComponent<Button>().onClick.AddListener(() => QuestUIChange(_progressButtonType[ButtonName()]));
        }
    }
    public void Minusitem(Item item, int amount)
    {
        if (item == null) { return; }
        if (item is CountableItem countableItem)
        {
            int currentValue = countableItem.GetCurrentAmount();
            if (((CountableItem)item).AddAmount(-amount) < 0)
            {
                Minusitem(item, ((CountableItem)item).AddAmount(-amount) - countableItem.GetCurrentAmount());
            }
            if (countableItem.GetCurrentAmount() < amount)
            {
                if (_inventory.Remove(item))
                {
                    Minusitem(_inventory.GetItemToId(item.Data.ID), amount - currentValue);
                }
            }
            return;
        }
    }
    public void CompleteQuest()
    {
        GameObject button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        QuestButton buttonID = button.GetComponent<QuestButton>();
        _questId = buttonID._questID;
        if(Managers.QuestManager._targetCheck[_questId]/10000 != 9)
        {
            _inventory.GetItemAction -= (() => ValueCheck(_questId));
            PubAndSub.UnSubscrib<int>("ItemSell", ((goodsID) => { ValueCheck(goodsID); }));
        }
        Logger.LogError($"{_content.transform.childCount}카운트 몇임?");
        test = _content.transform.childCount;
        if (!Managers.QuestManager._completeQuest.Contains(_questId))
        {
            Managers.QuestManager._completeQuest.Add(_questId);
            Managers.QuestManager._completeQuest.Sort();
        }

        Managers.QuestManager._progressQuest.Remove(_questId);
        Managers.QuestManager._progressQuest.Sort();
        if (Managers.QuestManager._targetCheck[_questId] / 10000 != 9)
        {
            Logger.LogError($"{Managers.QuestManager._targetCheck[_questId]}고블린은들어가?");
            Minusitem(_inventory.GetItemToId(Managers.QuestManager._targetCheck[_questId]), Managers.QuestManager._completeChecks[_questId]);
            Logger.LogError($"{_inventory.GetItemToId(Managers.QuestManager._targetCheck[_questId])}{Managers.QuestManager._completeChecks[_questId]}고블린 템 가져와짐?");
        }

        _player.PlayerEXPGain(_questRewardValue2);//추후 지석님께 여쭤보고 변경
        _player.PlayerGOLDGain(_questRewardValue1);//추후 지석님께 여쭤보고 변경
        for (int i = 0; i < _questRewardValue3; i++)
        {
            Item questItem = Item.ItemSpawn((int)_questRewardType3);
            _inventory.InsertItem(questItem);
        }
        bool destroy = false;
        PubAndSub.UnSubscrib<int>($"{_questId}", Managers.QuestManager.CheckProgress);
        if (_simpleQuestUI.activeSelf && Managers.QuestManager._changeText.ContainsKey(_questId))
        {
            if (Managers.QuestManager._changeID.ContainsKey(Managers.QuestManager._changeText[_questId]))
            {
                Managers.QuestManager._changeID.Remove(Managers.QuestManager._changeText[_questId]);
            }
            
            Managers.Resource.Destroy(Managers.QuestManager._changeText[_questId]);
            Managers.QuestManager._changeText.Remove(_questId);
            if (_content.transform.childCount == 1)
            {
                _simpleQuestUI.SetActive(false);
            }
            destroy = true;
        }
        //ReVoid();
        test = _content.transform.childCount;
        Logger.LogError($"{_content.transform.childCount}카운트 몇임?");
        if (_content.transform.childCount <= 3 && destroy)
        {
            Logger.LogError("1");
            if (Managers.QuestManager._progressQuest.Count >= 3)
            {
                Logger.LogError("2");
                for (int i = 0; i < Managers.QuestManager._progressQuest.Count; i++)
                {
                    Logger.LogError("3");
                    if (!Managers.QuestManager._changeText.ContainsKey(Managers.QuestManager._progressQuest[i]))
                    {
                        Logger.LogError("4");
                        Managers.QuestManager._questTextID = Managers.QuestManager._progressQuest[i];
                        break;
                    }
                }
                Logger.LogError("5");
                _simpleText = null;
                _simpleText = Managers.Resource.Instantiate("UI/SimpleQuestText", _content.transform);
                Managers.QuestManager._changeText.Add(Managers.QuestManager._questTextID, _simpleText);
                Managers.QuestManager._changeID.Add(_simpleText, Managers.QuestManager._questTextID);
                var text = _simpleText.GetOrAddComponent<SimpleQuestText>();
                Logger.LogError("6");
                if (!Managers.QuestManager._countCheck.ContainsKey(Managers.QuestManager._questTextID))
                {
                    Managers.QuestManager._countCheck.Add(Managers.QuestManager._questTextID, 0);
                }
                if (Managers.QuestManager._targetCheck[Managers.QuestManager._questTextID] / 10000 != 9)
                {
                    int goodsID = Managers.QuestManager._questTextID;
                    _inventory.GetItemAction += (() => ValueCheck(goodsID));
                    PubAndSub.Subscrib<int>("ItemSell", ((goodsID) => { ValueCheck(goodsID); }));
                    Managers.QuestManager._countCheck[goodsID] = _inventory.GetItemAmount(Managers.QuestManager._targetCheck[goodsID]);
                    if (_inventory.GetItemAmount(Managers.QuestManager._targetCheck[goodsID]) >= Managers.QuestManager._completeChecks[goodsID])
                    {
                        Managers.QuestManager._questComplete[goodsID] = true;
                    }
                }
                Managers.QuestManager._changeText[Managers.QuestManager._questTextID].GetComponent<SimpleQuestText>().Init(_content.transform);
            }
        }
        ReVoid();

        Managers.Sound.Play("ETC/ui_quest_clear");
    }
    public void GiveUpQuest()
    {
        GameObject button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        QuestButton buttonID = button.GetComponent<QuestButton>();
        _questId = buttonID._questID;
        _inventory.GetItemAction -= (() => ValueCheck(_questId));
        PubAndSub.UnSubscrib<int>("ItemSell", ((goodsID) => { ValueCheck(goodsID); }));
        Managers.QuestManager._progressQuest.Remove(_questId);
        Managers.QuestManager._progressQuest.Sort();
        if (!Managers.QuestManager._activeQuest.Contains(_questId))
        {
            Managers.QuestManager._activeQuest.Add(_questId);
            Managers.QuestManager._activeQuest.Sort();
        }
        PubAndSub.UnSubscrib<int>($"{_questId}", Managers.QuestManager.CheckProgress);
        Managers.QuestManager._countCheck[_questId] = 0;
        if (_simpleQuestUI.activeSelf)
        {
            Managers.QuestManager._changeID.Remove(Managers.QuestManager._changeText[_questId]);
            Managers.Resource.Destroy(Managers.QuestManager._changeText[_questId]);
            Managers.QuestManager._changeText.Remove(_questId);
            if (_content.transform.childCount == 1)
            {
                _simpleQuestUI.SetActive(false);
            }
        }
        if (Managers.QuestManager._progressQuest.Count >= 3)
        {
            for(int i = 0; i < Managers.QuestManager._progressQuest.Count; i++)
            {
                if (!Managers.QuestManager._changeText.ContainsKey(Managers.QuestManager._progressQuest[i]))
                {
                    Managers.QuestManager._questTextID = Managers.QuestManager._progressQuest[i];
                    break;
                }
            }
            
            _simpleText = null;
            _simpleText = Managers.Resource.Instantiate("UI/SimpleQuestText", _content.transform);
            Managers.QuestManager._changeText.Add(Managers.QuestManager._questTextID, _simpleText);
            Managers.QuestManager._changeID.Add(_simpleText, Managers.QuestManager._questTextID);
            var text = _simpleText.GetOrAddComponent<SimpleQuestText>();
            if (!Managers.QuestManager._countCheck.ContainsKey(Managers.QuestManager._questTextID))
            {
                Managers.QuestManager._countCheck.Add(Managers.QuestManager._questTextID, 0);
            }
            if (Managers.QuestManager._targetCheck[Managers.QuestManager._questTextID] / 10000 != 9)
            {
                int goodsID = Managers.QuestManager._questTextID;
                _inventory.GetItemAction += (() => ValueCheck(goodsID));
                PubAndSub.Subscrib<int>("ItemSell", ((goodsID) => { ValueCheck(goodsID); }));
                Managers.QuestManager._countCheck[goodsID] = _inventory.GetItemAmount(Managers.QuestManager._targetCheck[goodsID]);
                if (_inventory.GetItemAmount(Managers.QuestManager._targetCheck[goodsID]) >= Managers.QuestManager._completeChecks[goodsID])
                {
                    Managers.QuestManager._questComplete[goodsID] = true;
                }
            }
            Managers.QuestManager._changeText[Managers.QuestManager._questTextID].GetComponent<SimpleQuestText>().Init(_content.transform);
        }
        ReVoid();
    }
    public string ButtonName()
    {
        return UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
    }
    public void QuestUIChange(int ID)
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
            if (!Managers.QuestManager._questComplete[ID])
            {
                _uiButtons[1].SetActive(true);
                _uiButtons[2].SetActive(false);
            }
            else
            {
                _uiButtons[1].SetActive(false);
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

        GetImage((int)RewardImage.GoldReward).sprite = Managers.Resource.Load<Sprite>($"ItemIcon/EtcIcon/gold");//밑에 추후 경로 입력
        GetImage((int)RewardImage.ExpReward).sprite = Managers.Resource.Load<Sprite>($"ItemIcon/EtcIcon/exp");
        if (_questRewardType3 == 0)
        {
            GetImage((int)RewardImage.ItemReward).enabled = false;
        }
        else
        {
            if (!GetImage((int)RewardImage.ItemReward).enabled)
            {
                GetImage((int)RewardImage.ItemReward).enabled = true;
            }
            GetImage((int)RewardImage.ItemReward).sprite = Managers.Resource.Load<Sprite>($"ItemIcon/EtcIcon/{_questRewardType3}");

        }

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
        // 정렬된 순서로 자식들을 다시 설정 (선택 사항)
        for (int i = 0; i < children.Count; i++)
        {
            children[i].SetSiblingIndex(i);
        }
    }

}
