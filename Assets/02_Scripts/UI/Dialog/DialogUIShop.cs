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

    protected override void OnDisable()
    {
        base.OnDisable();
        //_TEMP
        ShopUIData shopUIData = new ShopUIData();
        //_TEMP
        shopUIData._itemCode = new List<(int, int)>();
        shopUIData._itemCode.Clear();
    }

    protected override IEnumerator DialogStart()
    {
        ActiveBtns(Buttons.CheckBtn);
        ActiveBtns(Buttons.RefuseBtn);
        ActiveBtns(Buttons.SynthesisBtn);

        yield return new WaitUntil(() => _dialogSystem[0].UpdateDialog());
        _isOpenUI = false;
        yield return new WaitUntil(() => _isOpenUI);
        Managers.Data.SaveData<InventorySaveData>();
        Managers.UI.CloseUI(this);
    }

    protected override void OnClickedButton()
    {
        
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
            (11006, 1),
            (11005, 1),
            (41001, 95)
        }
            };
            Managers.UI.OpenUI<ShopUI>(shopUIData);
        }
    }
}
