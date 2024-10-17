using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemConfirmData : BaseUIData {
    public bool isBuy;
    public Item Item;
    public ItemSlot ShopSlot;
    public InventorySlot InventorySlot;
}


public class ItemConfirm : ItemUI
{
    #region bind
    enum Buttons {
        ConfirmButton,
        CancelButton
    }
    enum Texts {
        ItmeName,
        ItemAmountTxt,
        ConfirmButtonTxt,
        CancelButtonTxt,
        MoneyAmountTxt
    }

    enum Sliders {
        ItemAmount,
    }
    enum itemSlots {
        ItemSlot,
    }
    #endregion
    public ItemSlot _shopSlot;
    public InventorySlot _inventorySlot;
    bool isBuy;
    public override void Init(Transform anchor)
    { 
        base.Init(anchor);
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Slider>(typeof(Sliders));
        Bind<ShowOnlySlot>(typeof(itemSlots));
    }

    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);
        ItemConfirmData data = uiData as ItemConfirmData;
        isBuy = data.isBuy;
        _shopSlot = data.ShopSlot;
        _inventorySlot = data.InventorySlot;
        Get<ShowOnlySlot>((int)itemSlots.ItemSlot).Setitem(data.Item);
        Get<TextMeshProUGUI>((int)Texts.ItmeName).text = data.Item.Data.Name;
        Get<TextMeshProUGUI>((int)Texts.ConfirmButtonTxt).text = isBuy ? "구매" : "판매";
        
        GetButton((int)Buttons.ConfirmButton).onClick.AddListener(OnConfirmBtn);
        GetButton((int)Buttons.CancelButton).onClick.AddListener(()=>CloseUI());
        if (data.Item is CountableItem)
        {
            Get<Slider>((int)Sliders.ItemAmount).gameObject.SetActive(true);
        }
        else {
            Get<Slider>((int)Sliders.ItemAmount).gameObject.SetActive(false);
        }
    }

    public void OnConfirmBtn() {
        if (isBuy)//살때
        {
            //if() 구매조건
            Item item = Item.ItemSpawn(_shopSlot.Item.Data.ID);
            if (item is CountableItem)
            {
                (item as CountableItem).SetAmount(Mathf.Max(1,(int)Get<Slider>((int)Sliders.ItemAmount).value));
                _inventorySlot.GetInventory().InsertItem(item);
            }
            else {
                _inventorySlot.GetInventory().InsertItem(item);
            }
        }
        else { //팔때
        
        
        
        }
        CloseUI();


    }

    public override void CloseUI(bool isCloseAll = false)
    {
        base.CloseUI(isCloseAll);
        GetButton((int)Buttons.ConfirmButton).onClick.RemoveAllListeners();
    }
}
