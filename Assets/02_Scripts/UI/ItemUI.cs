using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : BaseUI, IPointerEnterHandler,IPointerExitHandler
{
    protected GraphicRaycaster _raycaster;

    protected virtual void Awake()
    {
        _raycaster = GetComponent<GraphicRaycaster>();
    }
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        ItemGrab.Raycaster = _raycaster;
        Logger.Log(ItemGrab.Raycaster.gameObject);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        //ItemGrap.Raycaster = _raycaster;
    }
}
