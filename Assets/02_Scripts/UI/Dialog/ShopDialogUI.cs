using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopDialogUI : BaseUI
{
    public DialogSystem[] _dialogSystem;

    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        //대화 시작 시 모든 유아이 닫아버리기
        Managers.UI.CloseAllOpenUI();
        StartCoroutine(ShopDialog());
    }

    public override void CloseUI(bool isCloseAll = false)
    {
        base.CloseUI(isCloseAll);
        CloseUI(this);
    }

    IEnumerator ShopDialog()
    {
        Managers.Game._isActiveDialog = true;
        Managers.Game._player._isMoving = false;
        yield return new WaitUntil(() => _dialogSystem[0].UpdateDialog());
        Managers.Game._isActiveDialog = false;
        Managers.Game._player._isMoving = true;
        yield return new WaitForSeconds(0.2f);
        Managers.UI.CloseUI(this);
    }

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
            Managers.UI.CloseUI(this);
        }
    }
}
