using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemSlot : ItemSlot
{//상점 아이템 슬롯 클래스
    //이동이 되지 않으며 사고 파는기능만 있음
    public override void ItemInsert(ItemSlot moveSlot)
    {
        //판매 확인창이 뜸
        //판매 확인창에서 버튼을 누르면 아이템 가치에 맞는 돈을 획득
    }

    public override bool MoveItem(ItemSlot moveSlot)
    {
        //구현할 필요 없음
        return true;
    }

}
