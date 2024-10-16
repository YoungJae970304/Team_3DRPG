using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemGrab : MonoBehaviour
{
    public Image Icon;
    public static Action endGrapAction;
    public static GraphicRaycaster Raycaster { get; set; }  //레이캐스트를 위한 레이캐스터
    PointerEventData _pointerEvent;                         //포인트 이벤트
    List<RaycastResult> _result = new List<RaycastResult>();//레이캐스트 결과물을 담을 리스트
    ItemSlot _currnetSlot;                                  //클릭을 시작한 슬롯
    private Vector3 _beginDragIconPoint;                    // 드래그 시작 시 슬롯의 위치
    private Vector3 _beginDragCursorPoint;                  // 드래그 시작 시 커서의 위치
    public ToolTipUI toolTip;           //아이템 정보를 표시할 UI
    private ItemSlot _pointerOverSlot; // 현재 포인터가 위치한 곳의 슬롯
    float _lastClicktime = 0;
    private void Awake()
    {
        Raycaster = GetComponent<GraphicRaycaster>();
        _pointerEvent = new PointerEventData(EventSystem.current);
    }
    // Update is called once per frame
    void Update()
    {
        MouseInput();
    }

    #region Event

    T GetUIRayCast<T>() where T : IItemDropAble
    {
        _result.Clear();
        _pointerEvent.position = Input.mousePosition;

        Raycaster.Raycast(_pointerEvent, _result);
        
        if (_result.Count == 0) { return default(T); }
        return _result[0].gameObject.GetComponent<T>();
    }


    public void MouseInput()
    {
        if (Raycaster == null) { return; }

        OnPointerEnterAndExit();

        OnPointDown();
        OnPointerDrag();
        OnPointerUp();
    }

    public ItemSlot OnPointDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            _currnetSlot = GetUIRayCast<ItemSlot>();

            // 아이템을 갖고 있는 슬롯만 해당
            if (_currnetSlot != null && _currnetSlot.Item != null)
            {
                
                // 위치 기억, 참조 등록
                _beginDragCursorPoint = Input.mousePosition;//마우스 드래그 시작위치
                Icon.enabled = true;                        //마우스 따라다닐 이미지
                Icon.sprite = _currnetSlot._Image.sprite;   //이미지 변경
                _currnetSlot._Image.enabled = false;        //슬롯의 이미지 비활성화
                _beginDragIconPoint = _currnetSlot._Image.transform.position;
            }
            else
            {
                _currnetSlot = null;
            }
        }
        return null;
    }
    public void OnPointerDrag()
    {
        if (_currnetSlot == null) { return; }
        if (Input.GetMouseButton(0))
        {//변화량으로 위치변경
            Icon.transform.position = _beginDragIconPoint + (Input.mousePosition - _beginDragCursorPoint);
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
            _pointerOverSlot = null;
            endGrapAction?.Invoke();
            _currnetSlot = null;
        }
    }

    private void DragEnd()
    {
        IItemDropAble target = GetUIRayCast<IItemDropAble>();
        if (target != _currnetSlot)
        {
            if (target != null)
            {
                Logger.Log(target.GetType().ToString());
                target.ItemInsert(_currnetSlot);
            }
        }



    }
    private void OnPointerEnterAndExit()
    {
        // 이전 프레임의 슬롯
        var prevSlot = _pointerOverSlot;

        // 현재 프레임의 슬롯
        var currSlot = _pointerOverSlot = GetUIRayCast<ItemSlot>();

        if (prevSlot == null)
        {
            //슬롯위로 처음으로 올라갔을 때
            if (currSlot != null)
            {
                OnCurrentEnter();
            }
        }
        else
        {
            //슬롯 위에 있지 않으면
            if (currSlot == null)
            {
                OnPrevExit();
            }
            //새로운 슬롯 위로 올라가면
            else if (prevSlot != currSlot)
            {
                OnPrevExit();
                OnCurrentEnter();
            }
        }
        if (currSlot == null)
            return;
        if (Input.GetMouseButtonDown(1)|| IsDoubleClick())
        {
            ItemUse();
        }
        //슬롯 위에 올라가면
        void OnCurrentEnter()
        {
            //curSlot
            if (currSlot.Item != null && _currnetSlot == null)
            {
                toolTip.SetInfo(currSlot);
                toolTip.transform.position = Input.mousePosition;
                toolTip.gameObject.SetActive(true);

            }
        }
        //슬롯에서 나가면
        void OnPrevExit()
        {
            toolTip.gameObject.SetActive(false);
            //prevSlot
        }
        bool IsDoubleClick()
        {
            if (currSlot != null && Input.GetMouseButtonDown(0))
            {
                if (Time.time - _lastClicktime < 0.25f)
                {
                    return true;
                }
                _lastClicktime = Time.time;
            }
            return false;
        }
        void ItemUse() {
            if (currSlot.Item == null)
                return;
            Logger.Log(currSlot.Item.Data.Name);
            if (currSlot.Item is IUsableItem)
            {
                (currSlot.Item as IUsableItem).Use();
                currSlot.UpdateSlotInfo();
            }
        }
    }

    #endregion




}
