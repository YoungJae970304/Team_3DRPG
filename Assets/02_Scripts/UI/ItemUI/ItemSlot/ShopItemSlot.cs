using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemSlot : ItemSlot
{//상점 아이템 슬롯 클래스
    //이동이 되지 않으며 사고 파는기능만 있음
    public override void ItemInsert(ItemSlot moveSlot)
    {
        if (moveSlot is InventorySlot) { SellConfirm(moveSlot as InventorySlot); }
        //판매 확인창이 뜸
        //판매 확인창에서 버튼을 누르면 아이템 가치에 맞는 돈을 획득
       
    }

    public void Init() { 
    
    }
    public override bool MoveItem(ItemSlot moveSlot)
    {
        //구현할 필요 없음
        return true;
    }

    public void BuyConfirm(InventorySlot inventorySlot)
    {
        ItemConfirmData itemConfirmData = new ItemConfirmData();
        itemConfirmData.isBuy = false;
        itemConfirmData.Item = Item;
        itemConfirmData.ShopSlot = this;
        itemConfirmData.InventorySlot = inventorySlot;
        Managers.UI.OpenUI<ItemConfirm>(itemConfirmData);
        //돈이 사려는 아이템보다 많으면
        //구매 확인 UI 출력
        //구매확인 UI 는 확인 버튼을 누를시 아이템을 insert하고 사라짐
    }
    public void SellConfirm(InventorySlot moveSlot)
    {
        ItemConfirmData itemConfirmData = new ItemConfirmData();
        itemConfirmData.isBuy = false;
        itemConfirmData.Item = moveSlot.Item;
        itemConfirmData.ShopSlot = this;
        itemConfirmData.InventorySlot = moveSlot;
        Managers.UI.OpenUI<ItemConfirm>(itemConfirmData);
        //개수 채크
        //여러개면 슬라이더 표시
        //확인 버튼 누르면 그 개수만큼 차감 0이면삭제 그만큼 돈 증가
    }
}
