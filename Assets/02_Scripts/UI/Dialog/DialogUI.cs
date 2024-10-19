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

    public NpcController _npcController;

    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        if (!Managers.Game._isActiveDialog) // 대사가 진행 중이지 않을 때만 실행
        {
            Managers.UI.CloseAllOpenUI();
            _npcController._npcType = NpcController.NpcType.None;
            GetButton((int)Buttons.YesBtn).onClick.RemoveAllListeners();
            StartCoroutine(DialogStart());
        }
    }

    private void Awake()
    {
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        _npcController = GameObject.FindGameObjectWithTag("NPC").GetComponent<NpcController>();
    }

    IEnumerator DialogStart()
    {
        //NPC 타입에 따라서 나오는 대사 다르게
        switch (_npcController._npcType)
        {
            case NpcController.NpcType.DungeonNpc:
                StartCoroutine(DungeonNPC());
                yield break;
            case NpcController.NpcType.QuestNpc:
                StartCoroutine(QuestNPC());
                yield break;
            case NpcController.NpcType.ShopNpc:
                StartCoroutine(ShopNpc());
                yield break;
        }
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
        //대사 시작
        Managers.Game._isActiveDialog = true;
        GetText((int)Texts.YesBtnTxt).text = "던전 선택";
        GetText((int)Texts.ExitBtnTxt).text = "아니?";
        yield return new WaitUntil(() => _dialogSystem[0].UpdateDialog());
        bool isOpen = false;
        GetButton((int)Buttons.YesBtn).onClick.AddListener(() =>
        {
            isOpen = true;
            OpenDungeonUI();
        });
        yield return new WaitUntil(() => isOpen);
        GetButton((int)Buttons.YesBtn).onClick.RemoveListener(() => OpenDungeonUI());
        Managers.Game._isActiveDialog = false;
        Managers.UI.CloseUI(this);
    }

    IEnumerator QuestNPC()
    {
        Managers.Game._isActiveDialog = true;
        yield return new WaitUntil(() => _dialogSystem[1].UpdateDialog());
        GetText((int)Texts.YesBtnTxt).text = "수락";
        GetText((int)Texts.ExitBtnTxt).text = "거절";
        bool isAccepted = false;
        GetButton((int)Buttons.YesBtn).onClick.AddListener(() =>
        {
            isAccepted = true;
            AcceptBtn();
        });

        bool isRefuse = false;
        _exitBtn.onClick.AddListener(() => isRefuse = true);
        //버튼을 어떤걸 눌러서 트루가 되는지 기다렸다가 실행
        yield return new WaitUntil(() => isAccepted || isRefuse);

        //수락 버튼을 눌렀을 경우
        if (isAccepted)
        {
            yield return new WaitUntil(() => _dialogSystem[2].UpdateDialog());
            GetButton((int)Buttons.YesBtn).gameObject.SetActive(false);
            _exitBtn.gameObject.SetActive(false);
        }
        //거절 버튼을 눌렀을 경우
        else if (isRefuse)
        {
            //거절 눌렀을 경우 나오게
            yield return new WaitUntil(() => _dialogSystem[3].UpdateDialog());
            GetButton((int)Buttons.YesBtn).gameObject.SetActive(false);
            _exitBtn.gameObject.SetActive(false);
        }

        //대화 종료
        yield return new WaitForSeconds(0.2f);
        GetButton((int)Buttons.YesBtn).onClick.RemoveListener(() => AcceptBtn());
        Managers.Game._isActiveDialog = false;
        Managers.UI.CloseUI(this);
    }

    IEnumerator ShopNpc()
    {
        Managers.Game._isActiveDialog = true;
        yield return new WaitUntil(() => _dialogSystem[4].UpdateDialog());
        GetText((int)Texts.YesBtnTxt).text = "상점 이용";
        GetText((int)Texts.ExitBtnTxt).text = "아니?";
        bool isOpen = false;
        GetButton((int)Buttons.YesBtn).onClick.AddListener(() => {

            isOpen = true;
            OpenShopUI();
        });
        yield return new WaitUntil(() => isOpen);
        GetButton((int)Buttons.YesBtn).onClick.RemoveListener(() => OpenShopUI());
        Managers.Game._isActiveDialog = false;
        Managers.UI.CloseUI(this);
    }
}
