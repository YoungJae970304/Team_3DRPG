using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : ItemDragUI
{
    Inventory _inventory;
    [SerializeField] RectTransform _icontr;
    GameObject _simpleQuestUI;
    enum QuickItemSlots
    {
        ItemSlot_1,
        ItemSlot_2
    }

    enum SkillSlots
    {
        SkillSlot_E,
        SkillSlot_R,
    }

    enum Sliders
    {
        HpBar,
        MpBar,
    }

    enum Buttons
    {
        Quest,
        Inventory,
        Equiped,
        Skill,
        Option,
    }

    enum Texts
    {
        Level,
    }
    enum Images
    {
        fill
    }

    public SkillQuickSlot SkillSlot_E { get { return Get<SkillQuickSlot>((int)SkillSlots.SkillSlot_E); } }
    public SkillQuickSlot SkillSlot_R { get { return Get<SkillQuickSlot>((int)SkillSlots.SkillSlot_R); } }
    public QuickItemSlot ItemSlot_1 { get { return Get<QuickItemSlot>((int)QuickItemSlots.ItemSlot_1); } }
    public QuickItemSlot ItemSlot_2 { get { return Get<QuickItemSlot>((int)QuickItemSlots.ItemSlot_2); } }

    private void Start()
    {

        PubAndSub.Subscrib<int>("HP", HpChanged);
        PubAndSub.Subscrib<int>("MP", MpChanged);
        PubAndSub.Subscrib<int>("Level", UpdateLevel);
        PubAndSub.Subscrib<int>("EXP", UpdateExp);
        GetButton((int)Buttons.Inventory).onClick.AddListener(() => OpenPlayerUI<InventoryUI>());
        GetButton((int)Buttons.Quest).onClick.AddListener(() => OpenPlayerUI<QuestUI>());
        GetButton((int)Buttons.Skill).onClick.AddListener(() => OpenPlayerUI<SkillTree>());
        GetButton((int)Buttons.Option).onClick.AddListener(() => OpenPlayerUI<OptionUI>());
        GetButton((int)Buttons.Equiped).onClick.AddListener(() => OpenPlayerUI<EquipMentUI>());
        
    }
    public override void Init(Transform anchor)
    {
        Bind<QuickItemSlot>(typeof(QuickItemSlots));
        Bind<SkillQuickSlot>(typeof(SkillSlots));
        Bind<Slider>(typeof(Sliders));
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        base.Init(anchor);
        _inventory = Managers.Game._player.gameObject.GetOrAddComponent<Inventory>();
        _simpleQuestUI = Util.FindChild(gameObject, "SimpleQuestUI");
        Managers.Game._player.StatusEffect._iconTr = _icontr;
        HpChanged(Managers.Game._player._playerStatManager.HP);
        MpChanged(Managers.Game._player._playerStatManager.MP);
        UpdateLevel(Managers.Game._player._playerStatManager.Level);
        UpdateExp(Managers.Game._player._playerStatManager.EXP);
        foreach (QuickItemSlots quickItemSlot in Enum.GetValues(typeof(QuickItemSlots)))
        {
            Get<QuickItemSlot>((int)quickItemSlot)._inventory = _inventory;
            _inventory.GetItemAction -= Get<QuickItemSlot>((int)quickItemSlot).UpdateSlotInfo;
            _inventory.GetItemAction += Get<QuickItemSlot>((int)quickItemSlot).UpdateSlotInfo;
        }
        StartSimpleQuestOpenCheck();
    }
    public void StartSimpleQuestOpenCheck()
    {
        if (Managers.QuestManager._progressQuest.Count > 0)
        {
            if (!_simpleQuestUI.activeSelf)
            {
                _simpleQuestUI.SetActive(true);
            }
            for(int i = 0; i < Managers.QuestManager._progressQuest.Count; i++)
            {
                int id = Managers.QuestManager._progressQuest[i];
                if (Managers.QuestManager._countCheck[id] >= Managers.QuestManager._completeChecks[id])
                {
                    Logger.LogError($"{Managers.QuestManager._countCheck[id]},{i}번째 진행중인 수");
                    Logger.LogError($"{Managers.QuestManager._completeChecks[id]},{i}번째 완료 수");
                   
                    Managers.QuestManager._questComplete[id] = true;
                    Logger.LogError($"{Managers.QuestManager._questComplete[id]},{i}번째 true, false확인");
                }
            }
            int simpleQuestCount;
            if(Managers.QuestManager._progressQuest.Count > 3)
            {
                simpleQuestCount = 3;
            }
            else
            {
                simpleQuestCount = Managers.QuestManager._progressQuest.Count;
            }
            GameObject content = Util.FindChild(_simpleQuestUI, "QuestInfo");
           
            for (int i = 0; i < simpleQuestCount; i++)
            {
                int id = Managers.QuestManager._progressQuest[i];
                Managers.QuestManager._questTextID = Managers.QuestManager._progressQuest[i];
                PubAndSub.Subscrib<int>($"{id}", Managers.QuestManager.CheckProgress);
                
                if (content.transform.childCount < 3)
                {
                    GameObject _simpleText = null;
                    _simpleText = Managers.Resource.Instantiate("UI/SimpleQuestText", content.transform);
                    Managers.QuestManager._changeText.Add(id, _simpleText);
                    Managers.QuestManager._changeID.Add(_simpleText, id);
                    var text = _simpleText.GetComponent<SimpleQuestText>();
                    if (Managers.QuestManager._targetCheck[id] / 10000 != 9)
                    {
                        int goodsID = id;
                        _inventory.GetItemAction += (() => { ValueCheck(goodsID); });
                        PubAndSub.Subscrib<int>($"{goodsID}", ValueCheck);
                        Managers.QuestManager._countCheck[goodsID] = _inventory.GetItemAmount(Managers.QuestManager._targetCheck[goodsID]);
                        
                    
                    }
                    
                    Managers.QuestManager._changeText[id].GetComponent<SimpleQuestText>().Init(content.transform);
                }
            }
            for(int i = simpleQuestCount; i < Managers.QuestManager._progressQuest.Count; i++)
            {
                int id = Managers.QuestManager._progressQuest[i];
                Managers.QuestManager._questTextID = Managers.QuestManager._progressQuest[i];
                PubAndSub.Subscrib<int>($"{id}", Managers.QuestManager.CheckProgress);
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
        if (!Managers.QuestManager._changeText.ContainsKey(id))
        {
            return;
        }
        else
        {
            Managers.QuestManager._changeText[id].GetComponent<SimpleQuestText>().Init(Util.FindChild(_simpleQuestUI, "QuestInfo").transform);
        }
        if (_inventory.GetItemAmount(Managers.QuestManager._targetCheck[id]) >= Managers.QuestManager._completeChecks[id])
        {
            Managers.QuestManager._questComplete[id] = true;
        }
        else
        {
            Managers.QuestManager._questComplete[id] = false;
        }
    }
    public void QuickslotUpdate()
    {
        foreach (QuickItemSlots quickItemSlot in Enum.GetValues(typeof(QuickItemSlots)))
        {
            Get<QuickItemSlot>((int)quickItemSlot).UpdateSlotInfo();
        }
    }

    private void HpChanged(int value)
    {
        Get<Slider>((int)Sliders.HpBar).value = (float)value / (float)Managers.Game._player._playerStatManager.MaxHP;
    }
    private void MpChanged(int value)
    {
        Get<Slider>((int)Sliders.MpBar).value = (float)value / (float)Managers.Game._player._playerStatManager.MaxMP;
    }

    public void OpenPlayerUI<T>() where T : BaseUI
    {
        T playerUI = Managers.UI.GetActiveUI<T>() as T;
        if (playerUI != null)
        {
            Managers.UI.CloseUI(playerUI);

            Managers.Sound.Play("ETC/ui_close");
        }
        else
        {
            if (Managers.Scene.LoadingSceneCheck()) return;

            Managers.UI.OpenUI<T>(new BaseUIData());
        }
    }

    public void UpdateLevel(int value)
    {
        GetText((int)Texts.Level).text = $"Lv. {value}";
        UpdateExp(Managers.Game._player._playerStatManager.EXP);
    }
    private void UpdateExp(int value)
    {
        GetImage((int)Images.fill).fillAmount = (float)value / (float)Managers.Game._player._playerStatManager.MaxEXP;
    }
}
