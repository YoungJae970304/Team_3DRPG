using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        if (Managers.Game._player != null) return;
        Managers.Game.PlayerCreate();
        //이어하기 일경우를 판단
        if (!TitleCanvasUI._isNewGame)
        {
            ActiveToSaveUI();
            LoadAllData();
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
    //모든 데이터 로드
    void LoadAllData()
    {
        Managers.Data.LoadData<PlayerSaveData>();
        Managers.Data.LoadData<SkillSaveData>();
        Managers.Data.LoadData<InventorySaveData>();
        Managers.Data.LoadData<EquipmentSaveData>();
        Managers.Data.LoadData<QuestSaveData>();
    }
    //처음 로딩씬에서 UI를 오픈해서 로드할때 널래퍼런스 방지용 함수
    void ActiveToSaveUI()
    {
        Managers.UI.OpenUI<EquipMentUI>(new BaseUIData());
        Managers.UI.OpenUI<SkillTree>(new BaseUIData());
        Managers.UI.OpenUI<InventoryUI>(new BaseUIData());
        Managers.UI.OpenUI<QuestUI>(new BaseUIData());
        Managers.UI.OpenUI<SimpleQuestText>(new BaseUIData());
    }

    public override void Clear()
    {

    }
}
