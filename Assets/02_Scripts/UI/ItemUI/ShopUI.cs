using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShopUIData : BaseUIData {
    public List<(int,int)> _itemCode;
}

public class ShopUI : ItemDragUI
{
    public Transform _itemTrs;              //아이템 표시 위치
    [SerializeField] int _size;             //상점 최대 크기
    public List<ShopItemSlot> _shopItemSlot;//아이템 슬롯 관리용 리스트

    enum GameObjects
    {
        Slots,
        Window
    }
    //초기화
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        Bind<GameObject>(typeof(GameObjects));
        //아이템 슬롯이 아닌 UI에 아이템을 드롭했을 경우 작동하는 형태 정의
        GetGameObject((int)GameObjects.Window).GetOrAddComponent<ItemProxy>().SetProxy((moveSlot) => {
            _shopItemSlot[0].ItemInsert(moveSlot);
        }); ;
    }
    //데이터 초기화
    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);
        //비어있지 않다면 비움
        foreach (var slot in _shopItemSlot) { 
            Destroy(slot.gameObject);
        }
        ShopUIData shopData = uiData as ShopUIData;
        SlotSetting(shopData._itemCode.Count);
        //데이터로 아이템을 생성하고 슬롯에 할당
        for (int i = 0; i < shopData._itemCode.Count; i++) {
            _shopItemSlot[i].Setitem(Item.ItemSpawn(shopData._itemCode[i].Item1, shopData._itemCode[i].Item2)); 
        }
    }
    //표시할 아이템 슬롯을 원하는 개수만큼 생성
    void SlotSetting(int size)
    {
        _shopItemSlot = new List<ShopItemSlot>();
        for (int i = 0; i < size; i++)
        {
            ShopItemSlot slot = Managers.Resource.Instantiate("UI/ShopItemSlot",
                GetGameObject((int)GameObjects.Slots).transform).GetComponent<ShopItemSlot>();
            slot.Init();
            _shopItemSlot.Add(slot);
        }
    }
}
