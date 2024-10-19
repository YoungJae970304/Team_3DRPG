using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : BaseUI
{
    #region BIND
    enum Buttons
    {
        YesBtn,
    }

    enum Texts
    {
        YesBtnTxt,
        ExitBtnTxt,
    }
    #endregion

    public DialogSystem[] _dialogSystem;

    NpcController _npcController;

    public override void Init(Transform anchor)
    {
        base.Init(anchor);

        if (!Managers.Game._isActiveDialog) // 대사가 진행 중이지 않을 때만 실행
        {
            Managers.UI.CloseAllOpenUI();
            GetButton((int)Buttons.YesBtn).onClick.RemoveAllListeners();
            Logger.LogWarning("모든 에드리스너 리무브되는거 확인");
            StartCoroutine(DialogStart());
        }
    }

    private void Awake()
    {
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        _npcController = GameObject.FindGameObjectWithTag("NPC").GetComponent<NpcController>();

        if (_npcController == null) { Logger.LogError("npc를 못찾겠음"); }
    }

    IEnumerator DialogStart()
    {
        if (_npcController == null) { yield break; }

        foreach (var dialog in _dialogSystem)
        {
            dialog.gameObject.SetActive(false);
        }

        int dialogIdx = (int)_npcController._npcType - 1; //type.None

        if (dialogIdx >= 0 && dialogIdx < _dialogSystem.Length)
        {
            Logger.Log("Npc 타입 :  " + _npcController._npcType);
            Logger.Log("다이얼로그 인덱스 : " + dialogIdx);
            _dialogSystem[dialogIdx].gameObject.SetActive(true);
            Managers.Game._isActiveDialog = true;
            //NPC 타입에 따라서 나오는 대사 다르게
            switch (_npcController._npcType)
            {
                case NpcController.NpcType.DungeonNpc:
                    Logger.Log("던전 다이얼로그 시작");
                    yield return StartCoroutine(DungeonNPC());
                    break;
                case NpcController.NpcType.QuestNpc:
                    Logger.Log("퀘스트 다이얼로그 시작");
                    yield return StartCoroutine(QuestNPC());
                    break;
                case NpcController.NpcType.ShopNpc:
                    Logger.Log("상점 다이얼로그 시작");
                    yield return StartCoroutine(ShopNpc());
                    break;
                default:
                    Logger.LogWarning("타입을 찾을 수 없습니다");
                    break;
            }
            _dialogSystem[dialogIdx].gameObject.SetActive(false);
            Managers.Game._isActiveDialog = false;
            Logger.Log($"{Managers.Game._isActiveDialog}확인");
        }
        else { yield break; }
    }

    //던전 UI 오픈 함수 버튼 클릭시 생성
    public void OpenDungeonUI()
    {
        DungeonUI dungeonUI = Managers.UI.GetActiveUI<DungeonUI>() as DungeonUI;

        if (dungeonUI != null)
        {
            Managers.UI.CloseUI(dungeonUI);
        }
        else
        {
            Managers.UI.OpenUI<DungeonUI>(new BaseUIData());
        }
    }

    public void OpenShopUI()
    {
        ShopUI shopUI = Managers.UI.GetActiveUI<ShopUI>() as ShopUI;

        if (shopUI != null)
        {
            Managers.UI.CloseUI(shopUI);
        }
        else
        {
            Managers.UI.OpenUI<ShopUI>(new BaseUIData());
        }
    }

    public void AcceptBtn()
    {
        //퀘스트 새롭게 생성해서 퀘스트 로그 유아이스크롤뷰에 퀘스트리스트Btn생성
    }

    IEnumerator DungeonNPC()
    {
        GetText((int)Texts.YesBtnTxt).text = "던전 선택";
        GetText((int)Texts.ExitBtnTxt).text = "아니?";
        yield return new WaitUntil(() => _dialogSystem[0].UpdateDialog());
        bool isOpen = false;
        GetButton((int)Buttons.YesBtn).onClick.AddListener(() =>
        {
            isOpen = true;
            OpenDungeonUI();
            Logger.Log("던전 에드 리스너 확인");
        });
        yield return new WaitUntil(() => isOpen);
        GetButton((int)Buttons.YesBtn).onClick.RemoveListener(() => OpenDungeonUI());
        Logger.Log("던전 에드 리스너 리무브 확인");
        yield return null;
    }

    IEnumerator QuestNPC()
    {
        GetText((int)Texts.YesBtnTxt).text = "수락";
        GetText((int)Texts.ExitBtnTxt).text = "거절";
        yield return new WaitUntil(() => _dialogSystem[1].UpdateDialog());
        bool isAccepted = false;
        GetButton((int)Buttons.YesBtn).onClick.AddListener(() =>
        {
            isAccepted = true;
            AcceptBtn();
            Logger.Log("퀘스트 에드 리스너 확인");
        });

        bool isRefuse = false;
        //버튼을 어떤걸 눌러서 트루가 되는지 기다렸다가 실행
        yield return new WaitUntil(() => isAccepted || isRefuse);

        //수락 버튼을 눌렀을 경우
        if (isAccepted)
        {
            yield return new WaitUntil(() => _dialogSystem[2].UpdateDialog());
            HideBtn();
        }
        //거절 버튼을 눌렀을 경우
        else if (isRefuse)
        {
            //거절 눌렀을 경우 나오게
            yield return new WaitUntil(() => _dialogSystem[3].UpdateDialog());
            HideBtn();
        }
        //대화 종료
        yield return new WaitForSeconds(0.2f);
        GetButton((int)Buttons.YesBtn).onClick.RemoveListener(() => AcceptBtn());
        Logger.Log("퀘스트 에드 리스너 리무브 확인");
        yield return null;
    }
    void HideBtn()
    {
        GetButton((int)Buttons.YesBtn).gameObject.SetActive(false);
        _exitBtn.gameObject.SetActive(false);
    }
    IEnumerator ShopNpc()
    {
        GetText((int)Texts.YesBtnTxt).text = "상점 이용";
        GetText((int)Texts.ExitBtnTxt).text = "아니?";
        yield return new WaitUntil(() => _dialogSystem[4].UpdateDialog());
        bool isOpen = false;

        GetButton((int)Buttons.YesBtn).onClick.AddListener(() =>
        {
            isOpen = true;
            OpenShopUI();
        });
        Logger.Log("샵 에드 리스너 확인");
        yield return new WaitUntil(() => isOpen);
        GetButton((int)Buttons.YesBtn).onClick.RemoveListener(() => OpenShopUI());
        Logger.Log("샵 에드 리스너 리므부 확인");
        yield return null;
    }

}
