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
        ActiveBtns(Buttons.CheckBtn);
        ActiveBtns(Buttons.RefuseBtn);
        ActiveBtns(Buttons.SynthesisBtn);
        _isOpenUI = false;
        yield return new WaitUntil(() => _isOpenUI);
        Managers.UI.CloseUI(this);
    }

    protected override void OnClickedButton()
    {
        ShopOpenUI();
    }

    void ShopOpenUI()
    {
        if (!Managers.UI.IsActiveUI<ShopUI>())
        {
            ShopUIData shopUIData = new()
            {
                //_TEMP
                _itemCode = new List<(int, int)>
        {
            (11001, 1),
            (11002, 1),
            (11003, 1)
        }
            };
            Managers.UI.OpenUI<ShopUI>(shopUIData);
            _isOpenUI = true;
        }
    }
}
