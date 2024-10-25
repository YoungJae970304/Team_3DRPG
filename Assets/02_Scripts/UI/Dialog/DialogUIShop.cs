using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogUIShop : DialogUI
{
    protected override void OnDisable()
    {
        base.OnDisable();
        //_TEMP
        ShopUIData shopUIData = new ShopUIData();
        //_TEMP
        shopUIData._itemCode = new List<(int, int)>();
        shopUIData._itemCode.Remove((11001, 1));
        shopUIData._itemCode.Remove((11002, 1));
        shopUIData._itemCode.Remove((11003, 1));
    }

  protected override  IEnumerator DialogStart()
    {
        yield return new WaitUntil(() => _dialogSystem[0].UpdateDialog());
        _isOpenUI = false;
        yield return new WaitUntil(() => _isOpenUI);
        Managers.UI.CloseUI(this);
    }

    protected override void OnButton()
    {
        OpenShopUI();
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
            ShopUIData shopUIData = new ShopUIData();
            //_TEMP
            shopUIData._itemCode = new List<(int,int)>();
            shopUIData._itemCode.Add((11001, 1));
            shopUIData._itemCode.Add((11002, 1));
            shopUIData._itemCode.Add((11003, 1));
            Managers.UI.OpenUI<ShopUI>(shopUIData);
            _isOpenUI = true;
        }
    }
}
