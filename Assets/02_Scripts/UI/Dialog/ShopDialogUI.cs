using System.Collections;
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
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        //대화 시작 시 모든 유아이 닫아버리기
        Managers.UI.CloseAllOpenUI();
        StartCoroutine(ShopDialog());
    }
    IEnumerator ShopDialog()
    {
        Managers.Game._isActiveDialog = true;
        Managers.Game._player._isMoving = false;
        GetButton((int)Buttons.YesBtn).interactable = false;
        GetText((int)Texts.YesBtnTxt).text = "상점 이용";
        GetText((int)Texts.ExitBtnTxt).text = "아니?";
        yield return new WaitForSeconds(0.2f);
        GetButton((int)Buttons.YesBtn).interactable = true;
        yield return new WaitUntil(() => _dialogSystem[0].UpdateDialog());
        GetButton((int)Buttons.YesBtn).onClick.RemoveAllListeners();
        GetButton((int)Buttons.YesBtn).onClick.AddListener(() => { OpenShopUI(); });
        Managers.Game._isActiveDialog = false;
        Managers.Game._player._isMoving = true;
        Managers.UI.CloseUI(this);
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
            BaseUIData uiData = new BaseUIData();
            ShopUIData shopData = uiData as ShopUIData;
            Managers.UI.OpenUI<ShopUI>(shopData);
        }
    }
    #endregion
}
