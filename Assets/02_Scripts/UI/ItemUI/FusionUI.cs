using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FusionUI : ItemDragUI
{
    Inventory _inventory;
    #region bind
    enum ItemSlots
    {
        ItemSlot_1,
        ItemSlot_2,
        Result
    }

    enum Texts {
        RequiredAmount1,
        RequiredAmount2,
    }
    enum Buttons {
        Confirm,
        Cancel


    }


    #endregion

    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        _inventory=Managers.Game._player.GetComponent<Inventory>();
        Bind<ItemSlot>(typeof(ItemSlots));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Get<TextMeshProUGUI>((int)Texts.RequiredAmount1).text = "";
        Get<TextMeshProUGUI>((int)Texts.RequiredAmount2).text = "";

        Get<ItemSlot>((int)ItemSlots.ItemSlot_1).itemChangedAction -= CheckFusion;
        Get<ItemSlot>((int)ItemSlots.ItemSlot_2).itemChangedAction -= CheckFusion;
        Get<ItemSlot>((int)ItemSlots.ItemSlot_1).itemChangedAction += CheckFusion;
        Get<ItemSlot>((int)ItemSlots.ItemSlot_2).itemChangedAction += CheckFusion;

        (Get<ItemSlot>((int)ItemSlots.ItemSlot_1) as FusionSlot)._inventory = _inventory;
        (Get<ItemSlot>((int)ItemSlots.ItemSlot_2) as FusionSlot)._inventory = _inventory;
        Get<Button>((int)Buttons.Cancel).onClick.RemoveAllListeners();
        Get<Button>((int)Buttons.Cancel).onClick.AddListener(OnCancelBtn);
        Get<Button>((int)Buttons.Confirm).onClick.RemoveAllListeners();
        Get<Button>((int)Buttons.Confirm).onClick.AddListener(OnConfirmBtn);
    }

    void CheckFusion()
    {
        if (Get<ItemSlot>((int)ItemSlots.ItemSlot_1).Item == null ||
            Get<ItemSlot>((int)ItemSlots.ItemSlot_2).Item == null) { ResetData(); ; return; }
        Item item1 = Get<ItemSlot>((int)ItemSlots.ItemSlot_1).Item;
        Item item2 = Get<ItemSlot>((int)ItemSlots.ItemSlot_2).Item;
        FusionData fusionData;
        if (item1.Data.ID > item2.Data.ID)
        {
            //id2 기준 탐색
            fusionData = Managers.DataTable._FusionData.Where((data) => data.FusionItemID1 == item2.Data.ID && data.FusionItemID2 == item1.Data.ID).FirstOrDefault();
        }
        else
        {
            //id1 기준 탐색
            fusionData = Managers.DataTable._FusionData.Where((data) => data.FusionItemID1 == item1.Data.ID && data.FusionItemID2 == item2.Data.ID).FirstOrDefault();
        }
        if (fusionData != null)
        {
            Get<ItemSlot>((int)ItemSlots.Result).Setitem(Item.ItemSpawn(fusionData.ResultItemID));
            if (item1.Data.ID > item2.Data.ID)
            {
                //id2 기준 탐색
                Get<TextMeshProUGUI>((int)Texts.RequiredAmount1).text = fusionData.FusionItemAmount2.ToString();
                Get<TextMeshProUGUI>((int)Texts.RequiredAmount1).color =
                    !(item1 is CountableItem)  || (item1 as CountableItem).GetCurrentAmount() >= fusionData.FusionItemAmount2 ? Color.black : Color.red; 
                Get<TextMeshProUGUI>((int)Texts.RequiredAmount2).text = fusionData.FusionItemAmount1.ToString();
                Get<TextMeshProUGUI>((int)Texts.RequiredAmount2).color =
                    !(item2 is CountableItem) || (item2 as CountableItem).GetCurrentAmount() >= fusionData.FusionItemAmount1 ? Color.black : Color.red;
            }
            else
            {
                //id1 기준 탐색
                Get<TextMeshProUGUI>((int)Texts.RequiredAmount1).text = fusionData.FusionItemAmount1.ToString();
                Get<TextMeshProUGUI>((int)Texts.RequiredAmount1).color =
                    !(item1 is CountableItem) || (item1 as CountableItem).GetCurrentAmount() >= fusionData.FusionItemAmount1 ? Color.black : Color.red;
                Get<TextMeshProUGUI>((int)Texts.RequiredAmount2).text = fusionData.FusionItemAmount2.ToString();
                Get<TextMeshProUGUI>((int)Texts.RequiredAmount2).color =
                    !(item2 is CountableItem) || (item2 as CountableItem).GetCurrentAmount() >= fusionData.FusionItemAmount2 ? Color.black : Color.red;
            }
            
        }
        else
        {
            ResetData();
        }
    }

    private void ResetData()
    {
        Get<ItemSlot>((int)ItemSlots.Result).Setitem(null);
        Get<TextMeshProUGUI>((int)Texts.RequiredAmount1).text = "";
        Get<TextMeshProUGUI>((int)Texts.RequiredAmount2).text = "";
    }

    void OnConfirmBtn() {
        _inventory.InsertItem(Get<ItemSlot>((int)ItemSlots.Result).Item);
        ResetData();
        Get<ItemSlot>((int)ItemSlots.ItemSlot_1).Setitem(null);
        Get<ItemSlot>((int)ItemSlots.ItemSlot_2).Setitem(null);
    }
    void OnCancelBtn() {
        CloseUI();
    }
   public void Cancel() {
        if (Get<ItemSlot>((int)ItemSlots.ItemSlot_1).Item != null) {
            _inventory.InsertItem(Get<ItemSlot>((int)ItemSlots.ItemSlot_1).Item);
        }
        if (Get<ItemSlot>((int)ItemSlots.ItemSlot_2).Item != null)
        {
            _inventory.InsertItem(Get<ItemSlot>((int)ItemSlots.ItemSlot_2).Item);
        }
    }
    public override void CloseUI(bool isCloseAll = false)
    {
        Cancel();
        base.CloseUI(isCloseAll);
    }
}
