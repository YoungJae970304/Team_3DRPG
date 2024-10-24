using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour
{
    public Image Icon;
    public static GraphicRaycaster Raycaster { get; set; }  //레이캐스트를 위한 레이캐스터
    PointerEventData _pointerEvent;                         //포인트 이벤트
    List<RaycastResult> _result = new List<RaycastResult>();//레이캐스트 결과물을 담을 리스트
    IItemDragAndDropAble _currnetSlot;                                  //클릭을 시작한 슬롯
    private Vector3 _beginDragIconPoint;                    // 드래그 시작 시 슬롯의 위치
    private Vector3 _beginDragCursorPoint;                  // 드래그 시작 시 커서의 위치
    public ToolTipUI toolTip;           //아이템 정보를 표시할 UI
    private ItemSlot _pointerOverSlot; // 현재 포인터가 위치한 곳의 슬롯
    float _lastClicktime = 0;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Raycaster = GetComponent<GraphicRaycaster>();
        _pointerEvent = new PointerEventData(EventSystem.current);
    }
    // Update is called once per frame
    void Update()
    {
        MouseInput();
    }

    #region Event
    //UI 레이캐스트
    T GetUIRayCast<T>() where T : IItemDragAndDropAble
    {
        _result.Clear();
        _pointerEvent.position = Input.mousePosition;

        Raycaster.Raycast(_pointerEvent, _result);

        if (_result.Count == 0) { return default(T); }
        return _result[0].gameObject.GetComponent<T>();
    }

    //마우스 입력처리
    public void MouseInput()
    {
        if (Raycaster == null) { return; }

        OnPointerEnterAndExit();

        OnPointDown();
        OnPointerDrag();
        OnPointerUp();
    }
    //마우스 좌클릭클릭시작시
    public void OnPointDown()
    {
        if (Input.GetMouseButtonDown(0))
        {

            _currnetSlot = GetUIRayCast<IItemDragAndDropAble>();

            // 슬롯이 검사되면
            if (_currnetSlot != null)
            {
                // 슬롯의 드래그 시작 처리
                //드래그 가능한 상태가 아니면 false 리턴
                if (!_currnetSlot.DragEnter(Icon)) {
                    _currnetSlot = null;
                }
            }
        }
    }
    //클릭하면서 움직이는도중
    public void OnPointerDrag()
    {
        if (_currnetSlot == null) { return; }
        if (Input.GetMouseButton(0))
        {//마우스 위치로 아이콘 위치변경
            Icon.transform.position = Input.mousePosition ;
        }
    }
    //드래그 종료시
    public void OnPointerUp()
    {
        if (_currnetSlot == null) { return; }

        if (Input.GetMouseButtonUp(0))
        {
            //Down에서 한 처리 다시 초기화
            _currnetSlot.DragExit(Icon);

            DragEnd();
            _pointerOverSlot = null;
            _currnetSlot = null;
        }
    }
    //드롭했을때 작업처리
    private void DragEnd()
    {
        //마우스 밑의 UI 판단
        IItemDragAndDropAble target = GetUIRayCast<IItemDragAndDropAble>();

        if (target != null)
        {
            if (target != _currnetSlot)
            {
                Logger.Log(target.GetType().ToString());
                target.ItemInsert(_currnetSlot);
            }
        }
        else {
            if (_result.Count <= 0) {
                _currnetSlot.NullTarget();
            }
        }



    }
    //툴팁과 아이템 사용을 위해 슬롯을 인식하는 함수
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
        //선택된 슬롯이 없으면 리턴
        if (currSlot == null)
            return;
        //더블 클릭 혹은 우클릭시
        if (Input.GetMouseButtonDown(1) || IsDoubleClick())
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
        //더블 클릭 채크
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
        //아이템 사용
        void ItemUse()
        {
            if (currSlot.Item == null)
                return;
            Logger.Log(currSlot.Item.Data.Name);
            if (currSlot.Item is IUsableItem)//사용가능한 아이템이면
            {
                (currSlot.Item as IUsableItem).Use();//사용
                currSlot.UpdateSlotInfo();//슬롯 갱신
                (Managers.UI.GetActiveUI<InventoryUI>() as InventoryUI)?.UpdateSlot();
                (Managers.UI.GetActiveUI<MainUI>() as MainUI)?.QuickslotUpdate();
            }
        }
    }

    #endregion




}
