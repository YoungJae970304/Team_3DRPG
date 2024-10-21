using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopDialogUI : BaseUI
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

    public override void Init(Transform anchor)
    {
        base.Init(anchor);

        if (!Managers.Game._isActiveDialog) // 대사가 진행 중이지 않을 때만 실행
        {
            //대화 시작 시 모든 유아이 닫아버리기
            Managers.UI.CloseAllOpenUI();
            StartCoroutine(ShopNpc());
        }
    }

    private void Awake()
    {
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
    }

    IEnumerator ShopNpc()
    {
        Managers.Game._isActiveDialog = true;
        GetText((int)Texts.YesBtnTxt).text = "상점 이용";
        GetText((int)Texts.ExitBtnTxt).text = "아니?";
        yield return new WaitUntil(() => _dialogSystem[0].UpdateDialog());
        bool isOpen = false;
        GetButton((int)Buttons.YesBtn).onClick.AddListener(() =>
        {
            isOpen = true;
            OpenShopUI();
        });
        Managers.Game._isActiveDialog = false;
        Logger.Log("샵 에드 리스너 확인");
        yield return new WaitUntil(() => isOpen);
        ReomovedListeners();
        Logger.Log("샵 에드 리스너 리므부 확인");
        Managers.UI.CloseAllOpenUI();
    }

    #region 버튼 함수들
    //던전 UI 오픈 함수 버튼 클릭시 생성
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

    public void ReomovedListeners()
    {
        GetButton((int)Buttons.YesBtn).onClick.RemoveAllListeners();
    }
    #endregion
}
