using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LoadingScene : BaseScene
{
    public Slider _loadingBar;

    public TextMeshProUGUI _loadingTxt;
    public TextMeshProUGUI _skipTxt;

    Animator _fadeAnim;
    AsyncOperation ao;

    private void Start()
    {
        _fadeAnim = GameObject.FindWithTag("SceneManager").GetComponent<Animator>();
        StartCoroutine(GoNextScene(Managers.Scene._targetScene));
        //Logger.LogError((Managers.Game._player != null).ToString());
        if (Managers.Game._player != null) return;
        if (!TitleCanvasUI._isNewGame) { ApplyPlayerData(); } else
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
        }
    }

    public void ChangeScene()
    {
        ao.allowSceneActivation = true;
    }

    // 비동기 신
    IEnumerator GoNextScene(Define.Scene sceneType)
    {
        yield return null;

        // 지정된 씬을 비동기 형식으로 로드한다
        ao = Managers.Scene.LoadSceneAsync(sceneType);

        // 준비가 완료되어도 다음 씬으로 넘어가지 않게
        // 단, 이걸 사용하면 progree는 0.9까지밖에 안됨 -> 유니티 내부 구조의 문제

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
                    //널래퍼런스 방지용 유아이 ESC키로 전부 삭제하고 씬 로드
                    Managers.UI.CloseAllOpenUI();
                    //ao.allowSceneActivation = true;
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
        skillTree.CloseUI();
        foreach (var skill in skillTree._skillTreeItems) {
            SkillTreeItemData skillTreeItemData = skillSaveData._skillTreeItemDatas.Where((item) => skill._skillId == item._id).FirstOrDefault();
            if (skillTreeItemData != null) {
                skill.SkillLevel = skillTreeItemData._curLevel;
                skill.Skill.PassiveEffect(Managers.Game._player._playerStatManager);
                if (skill.SkillLevel > 0) {
                    skill.Skill._prevLevel = skill.SkillLevel - 1;
                }
            }
        }
    }

    void ApplyPlayerData()
    {
        PlayerSaveData playerSaveData = Managers.Data.GetData<PlayerSaveData>();
        Managers.Game._playerType = playerSaveData._PlayerTypes;
        Managers.Game.PlayerCreate();
        var player = Managers.Game._player;
        var stats = Managers.Game._player._playerStatManager;
        
        stats.Level = playerSaveData._level;
        stats.EXP = playerSaveData._exp;
        stats.MaxEXP = playerSaveData._maxExp;
        stats.SP = playerSaveData._sp;
        stats.Gold = playerSaveData._gold;
        //player.transform.position = new Vector3(_x, _y, _z);
        
    }

    void ApplyLargeMapData()
    {
        LargeMapData largeMapData = Managers.Data.GetData<LargeMapData>();
        LargeMapUI largeMapUI = Managers.UI.OpenUI<LargeMapUI>(new BaseUIData());
        largeMapUI.CloseUI();
        foreach(var largeMap in largeMapData._largeMapItemData)
        {
            if (largeMapData != null)
            {
                Texture2D texture = new Texture2D(largeMapUI._textureSize, largeMapUI._textureSize);
                // byte 배열을 Texture2D로 변환하는 작업
                texture.LoadImage(largeMap.fogTextureData);
                largeMapUI._sceneFogTextures[largeMap.sceneName] = texture;
            }
        }
    }

    public override void Clear()
    {

    }
}
