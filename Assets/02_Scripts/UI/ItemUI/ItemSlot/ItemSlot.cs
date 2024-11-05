using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemSlot : MonoBehaviour, IItemDragAndDropAble
{
    protected Item _item;                                           //아이템
    public Action itemChangedAction;                                //아이템 변경시 실행될 액션
    public Item Item                                                //설정시 슬롯정보 갱신하는 프로퍼티
    {
        get => _item; protected set
        {
            _item = value;
            itemChangedAction?.Invoke();
            UpdateSlotInfo();
        }
    }
    public bool _isUsealbe = true;
    public bool _isLocked=false;                                     //슬롯잠금
    public Image _Image;                                            //아이콘표시할 이미지
    [SerializeField] protected TextMeshProUGUI _text;               //남은 개수 표시할 텍스트
    public ItemData.ItemType _slotType = ItemData.ItemType.Weapon;  //슬롯이 담을 아이템 타입

    //슬롯 정보 갱신
    public virtual void UpdateSlotInfo()
    {
        if (Item == null)//빈칸이면 표시X
        {
            _Image.enabled = false;
            if (_text != null)
                _text.text = "";
            return;
        }
        //아이콘 변경
        _Image.enabled = true;
        _Image.sprite = Item.LoadIcon();
        //숫자 표시
        if (_text == null) { return; }
        if (Item is CountableItem)
        {
            _text.text = (Item as CountableItem)._amount.ToString(); ;
        }
        else
        {
            _text.text = "";
        }
    }
    //아이템 슬롯에 넣기
    public virtual void ItemInsert(IItemDragAndDropAble moveSlot)
    {
        if (moveSlot.GetType() == GetType())//같은종류의 슬롯이면
        {
            EqualSlot(moveSlot as ItemSlot);
        }
        else if(moveSlot is ItemSlot) {
            ItemSlot moveitemSlot = moveSlot as ItemSlot;
            Item item = moveitemSlot.Item;
            if (moveitemSlot.MoveItem(this))//리턴한 값이 ture일때만
            {
                Setitem(item);
            }
        }
    }
    //동일한 슬롯이랑 아이템을 변경할 경우
    public virtual void EqualSlot(ItemSlot moveSlot) {
        Item item = moveSlot.Item;
        if (moveSlot.MoveItem(this))
        {
            Setitem(item);
        }
    }

    //슬롯에 있는 아이템을 옮겼을때 해주어야할 일들
    public virtual bool MoveItem(ItemSlot moveSlot)
    {
        Item = moveSlot.Item;
        return true;
    }

    public virtual void RemoveItem()
    {
        Item = null;
        UpdateSlotInfo();
    }
    //허공에 버렸을 때 
    public virtual void NullTarget()
    {
        //아이템 버리기 UI 출력
    }
    //아이템을 변경해줄 경우
    public virtual void Setitem(Item item) {
        if(item == null) { Item = null; return; }
        if (Item != null && Item.Data.ID == item.Data.ID)
        {
            if (Item is CountableItem)
            {
                int overAmount = ((CountableItem)Item).AddAmount(((CountableItem)item)._amount);
                ((CountableItem)item).SetAmount(overAmount);
                UpdateSlotInfo();
                return;
            }
        }
        Item = item;
    }

    public virtual bool DragEnter(Image icon)
    {
        if (Item == null || _isLocked) { return false; }
        icon.enabled = true;                        //마우스 따라다닐 이미지
        icon.sprite = _Image.sprite;   //이미지 변경
        _Image.enabled = false;        //슬롯의 이미지 비활성화
        return true;
    }

    public virtual void DragExit(Image icon)
    {
        icon.enabled = false;
        _Image.enabled = true;
    }

    public virtual void Use() {
        if (!_isUsealbe) return;
    
    }
}
