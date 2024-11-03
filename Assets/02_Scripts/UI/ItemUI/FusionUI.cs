using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FusionUI : ItemDragUI
{//아이템 합성 UI
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
        //슬롯의 아이템이 변경될 때 합성 조건 채크
        Get<ItemSlot>((int)ItemSlots.ItemSlot_1).itemChangedAction -= CheckFusion;
        Get<ItemSlot>((int)ItemSlots.ItemSlot_2).itemChangedAction -= CheckFusion;
        Get<ItemSlot>((int)ItemSlots.ItemSlot_1).itemChangedAction += CheckFusion;
        Get<ItemSlot>((int)ItemSlots.ItemSlot_2).itemChangedAction += CheckFusion;

        (Get<ItemSlot>((int)ItemSlots.ItemSlot_1) as FusionSlot)._inventory = _inventory;
        (Get<ItemSlot>((int)ItemSlots.ItemSlot_2) as FusionSlot)._inventory = _inventory;
        //버튼 바인드
        Get<Button>((int)Buttons.Cancel).onClick.RemoveAllListeners();
        Get<Button>((int)Buttons.Cancel).onClick.AddListener(OnCancelBtn);
        Get<Button>((int)Buttons.Confirm).onClick.RemoveAllListeners();
        Get<Button>((int)Buttons.Confirm).onClick.AddListener(OnConfirmBtn);
        Get<Button>((int)Buttons.Confirm).interactable = false;
    }
    //합성 조건 확인 함수
    void CheckFusion()
    {//빈칸 존재시 리턴
        if (Get<ItemSlot>((int)ItemSlots.ItemSlot_1).Item == null ||
            Get<ItemSlot>((int)ItemSlots.ItemSlot_2).Item == null) { ResetData(); ; return; }
        Item item1 = Get<ItemSlot>((int)ItemSlots.ItemSlot_1).Item;
        Item item2 = Get<ItemSlot>((int)ItemSlots.ItemSlot_2).Item;
        FusionData fusionData;
        //ID가 낮은 아이템을 기준으로 data.FusionItemID1와 비교한다.
        if (item1.Data.ID > item2.Data.ID)
        {
            //item2 기준 탐색
            fusionData = Managers.DataTable._FusionData.Where((data) => data.FusionItemID1 == item2.Data.ID && data.FusionItemID2 == item1.Data.ID).FirstOrDefault();
        }
        else
        {
            //item1 기준 탐색
            fusionData = Managers.DataTable._FusionData.Where((data) => data.FusionItemID1 == item1.Data.ID && data.FusionItemID2 == item2.Data.ID).FirstOrDefault();
        }
        //합성 조건달성시
        if (fusionData != null)
        {
            //예상 결과 출력
            Get<ItemSlot>((int)ItemSlots.Result).Setitem(Item.ItemSpawn(fusionData.ResultItemID));
            if (item1.Data.ID > item2.Data.ID)
            {
                //item2 기준 요구 수량 확인
                Get<TextMeshProUGUI>((int)Texts.RequiredAmount1).text = fusionData.FusionItemAmount2.ToString();
                Get<TextMeshProUGUI>((int)Texts.RequiredAmount1).color =
                    !(item1 is CountableItem)  || (item1 as CountableItem).GetCurrentAmount() >= fusionData.FusionItemAmount2 ? Color.black : Color.red; 
                Get<TextMeshProUGUI>((int)Texts.RequiredAmount2).text = fusionData.FusionItemAmount1.ToString();
                Get<TextMeshProUGUI>((int)Texts.RequiredAmount2).color =
                    !(item2 is CountableItem) || (item2 as CountableItem).GetCurrentAmount() >= fusionData.FusionItemAmount1 ? Color.black : Color.red;
            }
            else
            {
                //item1 기준  요구 수량 확인
                Get<TextMeshProUGUI>((int)Texts.RequiredAmount1).text = fusionData.FusionItemAmount1.ToString();
                Get<TextMeshProUGUI>((int)Texts.RequiredAmount1).color =
                    !(item1 is CountableItem) || (item1 as CountableItem).GetCurrentAmount() >= fusionData.FusionItemAmount1 ? Color.black : Color.red;
                Get<TextMeshProUGUI>((int)Texts.RequiredAmount2).text = fusionData.FusionItemAmount2.ToString();
                Get<TextMeshProUGUI>((int)Texts.RequiredAmount2).color =
                    !(item2 is CountableItem) || (item2 as CountableItem).GetCurrentAmount() >= fusionData.FusionItemAmount2 ? Color.black : Color.red;
            }
            Get<Button>((int)Buttons.Confirm).interactable = true;
        }
        else
        {
            ResetData();
            Get<Button>((int)Buttons.Confirm).interactable = false;
        }
        
    }

    private void ResetData()
    {
        Get<ItemSlot>((int)ItemSlots.Result).Setitem(null);
        Get<TextMeshProUGUI>((int)Texts.RequiredAmount1).text = "";
        Get<TextMeshProUGUI>((int)Texts.RequiredAmount2).text = "";
    }

    bool CheckFusionable() {
        if(Get<ItemSlot>((int)ItemSlots.Result).Item == null){ return false; }
        if (Get<TextMeshProUGUI>((int)Texts.RequiredAmount2).color !=
            Get<TextMeshProUGUI>((int)Texts.RequiredAmount1).color) { return false; }
        if(Get<TextMeshProUGUI>((int)Texts.RequiredAmount2).color == Color.red ){ return false; }
        return true;
    }
    void OnConfirmBtn() {
        if (!CheckFusionable()) { return; }
        _inventory.InsertItem(Get<ItemSlot>((int)ItemSlots.Result).Item);
        ResetData();
        Get<ItemSlot>((int)ItemSlots.ItemSlot_1).Setitem(null);
        Get<ItemSlot>((int)ItemSlots.ItemSlot_2).Setitem(null);
        Managers.Sound.Play("ETC/ui_equip_upgrade_success");
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
        Get<ItemSlot>((int)ItemSlots.ItemSlot_1).Setitem(null);
        Get<ItemSlot>((int)ItemSlots.ItemSlot_2).Setitem(null);
    }
    public override void CloseUI(bool isCloseAll = false)
    {
        Cancel();
        base.CloseUI(isCloseAll);
    }
}
