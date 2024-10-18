using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemSlot : MonoBehaviour, IItemDropAble, IItemDragAble
{
    protected Item _item;
    public Action itemChangedAction;
    public Item Item
    {
        get => _item; protected set
        {
            _item = value;
            itemChangedAction?.Invoke();
            UpdateSlotInfo();
        }
    }
    public bool isLocked=false;
    public Image _Image;
    [SerializeField] protected TextMeshProUGUI _text;
    public ItemData.ItemType _slotType = ItemData.ItemType.Weapon;

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
    public virtual void ItemInsert(ItemSlot moveSlot)
    {
        if (moveSlot.GetType() == GetType())//같은종류의 슬롯이면
        {
            EqualSlot(moveSlot);
        }
        else {
            Item item = moveSlot.Item;
            if (moveSlot.MoveItem(this))//리턴한 값이 ture일때만
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
    public abstract bool MoveItem(ItemSlot moveSlot);

    public virtual void RemoveItem()
    {
        Item = null;
    }
    //허공에 버렸을 때 
    public virtual void NullTarget()
    {
        //아이템 버리기 UI 출력
    }
    //아이템을 변경해줄 경우
    public virtual void Setitem(Item item) {
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
}
