using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventoryUI : MonoBehaviour
{
    public ItemData.ItemType _currentType= ItemData.ItemType.Potion;

    public Transform _itemTrs;

    public Inventory _inventory;

    public Image Icon;
    
    public Inventory Inventory
    { 
        get {
            UpdateSlot();
            return _inventory; 
        } 
    }

    public List<InventorySlot> _inventorySlots;
    // Start is called before the first frame update
    void Start()
    {

        _raycaster = GetComponent<GraphicRaycaster>();
        _pointerEvent = new PointerEventData( EventSystem.current);
        _inventory.GetItemAction += UpdateSlot;
        SlotSetting(_currentType);
        UpdateSlot();
        //Managers.Input.UIMouseAction += MouseInput;
    }

    private void Update()
    {
        MouseInput();
    }

    void SlotSetting(ItemData.ItemType type) {
       int size= _inventory.GetGroupSize(type);
        _inventorySlots = new List<InventorySlot>();
        for (int i = 0; i < size; i++) {
            InventorySlot slot= Managers.Resource.Instantiate("UI/Slot", _itemTrs).GetComponent<InventorySlot>();
            slot.Init(_inventory,this);
            _inventorySlots.Add(slot);
        }
    }
    [ContextMenu("update")]
    public void UpdateSlot()
    {
        if (!gameObject.activeSelf) { return; }
        for (int i = 0; i < _inventorySlots.Count; i++)
        {
            _inventorySlots[i].UpdateInfo();
        }
    }

    #region Event
    GraphicRaycaster _raycaster;
    PointerEventData _pointerEvent;
    List<RaycastResult> _result=new List<RaycastResult>();
    InventorySlot _currnetSlot;
    private Vector3 _beginDragIconPoint;   // 드래그 시작 시 슬롯의 위치
    private Vector3 _beginDragCursorPoint; // 드래그 시작 시 커서의 위치
    T GetUIRayCast<T>() where T : Component
    {
        _result.Clear();
        _pointerEvent.position = Input.mousePosition;

        _raycaster.Raycast(_pointerEvent, _result);

        if (_result.Count == 0) { return null; }
        Logger.Log(_result[0].gameObject.name);
        return _result[0].gameObject.GetComponent<T>();
    }


    public void MouseInput() {
        OnPointDown();
        OnPointerDrag();
        OnPointerUp();
    }

    public InventorySlot OnPointDown() {
        if (Input.GetMouseButtonDown(0)) {
            _currnetSlot = GetUIRayCast<InventorySlot>();

            // 아이템을 갖고 있는 슬롯만 해당
            if (_currnetSlot != null && _currnetSlot.Item!=null)
            {
                // 위치 기억, 참조 등록
                _beginDragCursorPoint = Input.mousePosition;//마우스 드래그 시작위치
                Icon.enabled = true;                        //마우스 따라다닐 이미지
                Icon.sprite = _currnetSlot._Image.sprite;   //이미지 변경
                _currnetSlot._Image.enabled = false;        //슬롯의 이미지 비활성화
                _beginDragIconPoint = _currnetSlot._Image.transform.position;
                // 해당 슬롯의 하이라이트 이미지를 아이콘보다 뒤에 위치시키기
                //_currnetSlot.SetHighlightOnTop(false);
            }
            else
            {
                _currnetSlot = null;
            }
        }
        return null;
    }
    public void OnPointerDrag() {
        if (_currnetSlot == null) { return; }
        if (Input.GetMouseButton(0)) {//변화량으로 위치변경
            Icon.transform.position= _beginDragIconPoint + (Input.mousePosition - _beginDragCursorPoint);
        }
    }
    public void OnPointerUp()
    {
        if (_currnetSlot == null) { return; }
        if (Input.GetMouseButtonUp(0))
        {
            //Down에서 한 처리 다시 초기화
            Icon.enabled = false;
            _currnetSlot._Image.enabled = true;

            DragEnd();
            UpdateSlot();
            _currnetSlot = null;
        }
    }

    private void DragEnd()
    {
        InventorySlot target=  GetUIRayCast<InventorySlot>();
        if (target == _currnetSlot) { return; }
        if (target != null)
        {
            if (target.Item != null && target.Item.Data.ID == _currnetSlot.Item.Data.ID)
            {
                if (target.Item is CountableItem)
                {
                    int overamount = ((CountableItem)target.Item).AddAmount(((CountableItem)_currnetSlot.Item)._amount);
                    ((CountableItem)_currnetSlot.Item).SetAmount(overamount);
                    return;
                }
            }
            Inventory.SwitchItem(target._index, _currnetSlot._index, _currnetSlot.Item.Data.Type);
        }
        


    }
    #endregion
    
    

    public void ChageGroup(int type) {

        _currentType = (ItemData.ItemType)type;
        if (Enum.IsDefined(typeof(ItemData.ItemType), _currentType)) {
            UpdateSlot();
        }
        
    }
}
