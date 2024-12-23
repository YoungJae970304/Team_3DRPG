using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScene : BaseScene
{
    // 로딩 진행도를 판단할 수 있는 슬라이더
    public Slider _loadingBar;

    public TextMeshProUGUI _loadingTxt;
    public TextMeshProUGUI _skipTxt;

    Animator _fadeAnim;
    AsyncOperation ao;

    float originEffectVolume;

    private void Start()
    {
        // 효과음 0으로 설정
        originEffectVolume = Managers.Sound.GetEffectVolume();
        Managers.Sound.SetEffectVolume(0);

        Managers.Data.DataInit();

        // 이전 씬에서 입력받은 _targetScene으로 전환
        _fadeAnim = GameObject.FindWithTag("SceneManager").GetComponent<Animator>();
        StartCoroutine(GoNextScene(Managers.Scene._targetScene));

        if (Managers.Game._player != null) return;
        if (!TitleCanvasUI._isNewGame) { ApplyPlayerData(); }
        else
        {
            Managers.Game.PlayerCreate();
        }

        //이어하기 일경우를 판단
        if (!TitleCanvasUI._isNewGame)
        {
            Managers.Data.LoadAllData();
            //데이터 적용 부분
            //장비
            ApplyEquipData();
            //인벤
            ApplyInvenData();
            //스킬
            ApplySkillData();
            //맵
            ApplyLargeMapData();
            //퀵슬롯
            ApplyQuickSlotData();
            //퀘스트
            ApplyQusetData();
        }
        Managers.QuestManager.QuestListInput();
        Managers.QuestManager.AddActiveQuest();
        Managers.QuestManager._curLevelCountPlus += Managers.QuestManager.LevelCountPlus;
    }

    // 애니메이션 이벤트로 사용될 메서드
    public void ChangeScene()
    {
        ao.allowSceneActivation = true;
    }

    // 비동기 씬
    IEnumerator GoNextScene(Define.Scene sceneType)
    {
        yield return null;

        // 지정된 씬을 비동기 형식으로 로드한다
        ao = Managers.Scene.LoadSceneAsync(sceneType);

        // 준비가 완료되어도 다음 씬으로 넘어가지 않게하기 위한 처리
        ao.allowSceneActivation = false;

        // 로딩이 완료될 때까지 반복해서 요소들을 로드하고 진행 과정을 하면에 표시한다
        while (!ao.isDone)
        {
            // 로딩 진행률을 슬라이더 바와 텍스트로 표시한다
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
            _loadingBar.value = progress;
            _loadingTxt.text = (progress * 100f).ToString("F0") + "%";

            // 만일 씬 로드 진행률이 90%를 넘어가면
            if (ao.progress >= 0.9f)
            {
                _skipTxt.enabled = true;
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    //null 방지용 UI ESC키로 전부 삭제하고 씬 로드
                    Managers.UI.CloseAllOpenUI();

                    // 원래 사운드로 복구
                    Managers.Sound.SetEffectVolume(originEffectVolume);

                    _fadeAnim.SetTrigger("doFade");
                }
            }

            yield return null;
        }
    }

    void ApplyEquipData()
    {
        EquipMentUI equipMentUI = Managers.UI.OpenUI<EquipMentUI>(new BaseUIData());
        equipMentUI.StatSum();
        equipMentUI.CloseUI();
    }

    void ApplyInvenData()
    {
        InventorySaveData inventorySaveData = Managers.Data.GetData<InventorySaveData>();
        Inventory inventory = Managers.Game._player.GetOrAddComponent<Inventory>();
        foreach (var itemData in inventorySaveData._InvenItemList)
        {
            //아이템 검증
            Item newSaveItem = Item.ItemSpawn(itemData._id, itemData._amount);
            if (newSaveItem == null)
            {
                Logger.LogWarning($"유효하지 않은 아이템 ID: {itemData._id}");

                continue;
            }

            // 빈 슬롯인지 확인 후 아이템 설정
            if (inventory.GetItem(itemData._index, newSaveItem.Data.Type) == null)
            {

                inventory.Setitem(itemData._index, newSaveItem);
            }
            else
            {
                Logger.LogWarning($"인벤토리 인덱스 {itemData._index}에 이미 아이템이 존재합니다.");
            }
        }
    }

    void ApplySkillData()
    {
        SkillSaveData skillSaveData = Managers.Data.GetData<SkillSaveData>();
        SkillTreeData skillTreeData = new SkillTreeData(Managers.Game._playerType);
        SkillTree skillTree = Managers.UI.OpenUI<SkillTree>(skillTreeData);
        foreach (var skill in skillTree._skillTreeItems)
        {
            SkillTreeItemData skillTreeItemData = skillSaveData._skillTreeItemDatas.Where((item) => skill._skillId == item._id).FirstOrDefault();
            if (skillTreeItemData != null)
            {
                skill.SkillLevel = skillTreeItemData._curLevel;
                skill.Skill.PassiveEffect(Managers.Game._player._playerStatManager);
                if (skill.SkillLevel > 0)
                {
                    skill.Skill._prevLevel = skill.SkillLevel - 1;
                }
            }
        }
        skillTree.CloseUI();
    }

    void ApplyPlayerData()
    {
        Managers.Data.LoadData<PlayerSaveData>();
        PlayerSaveData playerSaveData = Managers.Data.GetData<PlayerSaveData>();
        Managers.Game._playerType = playerSaveData._PlayerTypes;
        Managers.Game.PlayerCreate();
        var player = Managers.Game._player;
        var stats = Managers.Game._player._playerStatManager;

        stats.Level = playerSaveData._level;
        stats.SP = playerSaveData._sp;
        stats.MaxEXP = playerSaveData._maxExp;
        stats.EXP = playerSaveData._exp;
        stats.Gold = playerSaveData._gold;
        

        stats._originStat.MaxHP = stats.MaxHP > 0 ? stats.MaxHP : 150;
        stats._originStat.HP = stats.HP > 0 ? stats.HP : stats.MaxHP;
        stats._originStat.MaxMP = stats.MaxMP > 0 ? stats.MaxMP : 100;
        stats._originStat.MP = stats.MP > 0 ? stats.MP : stats.MaxMP;
        stats._originStat.MoveSpeed = stats.MoveSpeed > 0 ? stats.MoveSpeed : 5f;
        stats._originStat.DodgeSpeed = stats.DodgeSpeed > 0 ? stats.DodgeSpeed : 10f;
        stats._originStat.ATK = stats.ATK > 0 ? stats.ATK : 30;
        stats._originStat.DEF = stats.DEF > 0 ? stats.DEF : 10;
        stats._originStat.RecoveryHP = stats.RecoveryHP > 0 ? stats.RecoveryHP : 2;
        stats._originStat.RecoveryMP = stats.RecoveryMP > 0 ? stats.RecoveryMP : 2;
        Logger.Log($"스텟 적용후 스텟 확인 : {string.Join(", ", stats.SP)}");
    }

    void ApplyLargeMapData()
    {
        LargeMapData largeMapData = Managers.Data.GetData<LargeMapData>();
        LargeMapUI largeMapUI = Managers.UI.OpenUI<LargeMapUI>(new BaseUIData());
        foreach (var largeMap in largeMapData._largeMapItemData)
        {
            if (largeMapData != null)
            {
                Texture2D texture = new Texture2D(largeMapUI._textureSize, largeMapUI._textureSize);
                // byte 배열을 Texture2D로 변환하는 작업
                texture.LoadImage(largeMap.fogTextureData);
                largeMapUI._sceneFogTextures[largeMap.sceneName] = texture;
            }
        }
        largeMapUI.CloseUI();
    }

    void ApplyQuickSlotData()
    {
        QuickSlotSaveData quickSlotSaveData = Managers.Data.GetData<QuickSlotSaveData>();
        MainUI mainUI = Managers.UI.OpenUI<MainUI>(new BaseUIData());
        Inventory inventory = Managers.Game._player.GetOrAddComponent<Inventory>();
        if (mainUI != null)
        {
            QuickItemSlot[] quickItemSlots = mainUI.GetComponentsInChildren<QuickItemSlot>();
            foreach (var slotData in quickSlotSaveData._quickItemSlotData)
            {
                int slotIndex = slotData._slotIndex;
                if (slotIndex < quickItemSlots.Length)
                {
                    Item slotItem = inventory.GetItemToId(slotData._id);
                    if (slotItem != null)
                    {
                        quickItemSlots[slotIndex].Setitem(slotItem);
                    }
                }
            }
            SkillQuickSlot[] skillQuickSlots = mainUI.GetComponentsInChildren<SkillQuickSlot>();
            SkillTreeData skillTreeData = new SkillTreeData(Managers.Game._playerType);
            SkillTree skillTree = Managers.UI.OpenUI<SkillTree>(skillTreeData);
            foreach (var skillSlot in quickSlotSaveData._quickSkillSlotData)
            {
                // 저장된 Skill ID와 일치하는 SkillTreeItem을 찾아 설정
                var matchingSkill = skillTree._skillTreeItems.FirstOrDefault(skillItem => skillItem._skillId == skillSlot._id);

                // 매칭되는 스킬이 있을 경우에만 설정
                if (matchingSkill != null && skillSlot._slotIndex < skillQuickSlots.Length)
                {
                    skillQuickSlots[skillSlot._slotIndex].Skill = matchingSkill.Skill;  // SkillQuickSlot에 스킬을 설정
                    skillQuickSlots[skillSlot._slotIndex]._image.sprite = matchingSkill.Icon.sprite; // 아이콘 업데이트
                    skillQuickSlots[skillSlot._slotIndex]._image.enabled = true;  // 아이콘 활성화
                }
            }
            skillTree.CloseUI();
        }
        mainUI.QuickslotUpdate();
    }

    void ApplyQusetData()
    {
        QuestSaveData questSaveData = Managers.Data.GetData<QuestSaveData>();
        QuestManager questManager = Managers.QuestManager;
        QuestUI acceptedQuestUI = Managers.UI.OpenUI<QuestUI>(new BaseUIData());
        acceptedQuestUI._questInput = Define.QuestInput.Q;
        bool hasFinishedQuest = false;
        if (acceptedQuestUI)
        {
            foreach (var questItemData in questSaveData._questItemData)
            {
                // 진행 중인 퀘스트라면
                if (questItemData._isProgress == 1)
                {
                    // 퀘스트 ID에 해당하는 퀘스트가 _progressQuest에 없으면 추가
                    if (!questManager._progressQuest.Contains(questItemData._id))
                    {
                        questManager._progressQuest.Add(questItemData._id);
                    }

                    if (!questManager._countCheck.ContainsKey(questItemData._id))
                    {
                        questManager._countCheck.Add(questItemData._id, 0);
                    }

                    questManager._countCheck[questItemData._id] = questItemData._progressInfo;
                }
            }
            foreach (var completedQuest in questSaveData._complateQuest)
            {
                if (!questManager._completeQuest.Contains(completedQuest._id))
                {
                    questManager._completeQuest.Add(completedQuest._id);
                    hasFinishedQuest = true;
                    Logger.Log($"완료 한 퀘스트 {completedQuest._id}");
                }
            }
        }
        if (!hasFinishedQuest)
        {
            Logger.LogWarning("완료된 퀘스트가 없습니다.");
        }
        acceptedQuestUI.CloseUI();
        // 현재 진행 중인 퀘스트들을 문자열로 나열해주기
        Logger.Log($"현재 진행 중인 퀘스트: {string.Join(", ", questManager._progressQuest)}");
    }

    public override void Clear()
    {

    }
}
