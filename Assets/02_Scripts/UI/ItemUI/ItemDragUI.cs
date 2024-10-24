using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragUI : BaseUI, IPointerEnterHandler,IPointerExitHandler
{
    protected GraphicRaycaster _raycaster;

    protected virtual void Awake()
    {
        _raycaster = GetComponent<GraphicRaycaster>();
    }
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        DragAndDrop.Raycaster = _raycaster;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        //ItemGrap.Raycaster = _raycaster;
    }
}
