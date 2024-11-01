using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogUIShop : DialogUI
{
    protected override void OnEnable()
    {
        base.OnEnable();
        GetButton((int)Buttons.CheckBtn).onClick.AddListener(() => {
            ShopOpenUI();
            UITypeOpen<InventoryUI>();
        });
        GetButton((int)Buttons.SynthesisBtn).onClick.AddListener(() => {
            UITypeOpen<FusionUI>();
            UITypeOpen<InventoryUI>();
        });
    }

    protected override IEnumerator DialogStart()
    {
        ActiveBtns(Buttons.CheckBtn);
        ActiveBtns(Buttons.RefuseBtn);
        ActiveBtns(Buttons.SynthesisBtn);

        yield return new WaitUntil(() => _dialogSystem[0].UpdateDialog());
        _isOpenUI = false;
        yield return new WaitUntil(() => _isOpenUI);
        Managers.UI.CloseUI(this);
    }

    protected override void OnClickedButton()
    {
        
    }

    void ShopOpenUI()
    {
        if (!Managers.UI.IsActiveUI<ShopUI>())
        {
            //Managers.DataTable._ShopUIData()
            ShopUIData shopUIData = Managers.DataTable.GetShopData();
            Managers.UI.OpenUI<ShopUI>(shopUIData);
   
        }
    }
}
