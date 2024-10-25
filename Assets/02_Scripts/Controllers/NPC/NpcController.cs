using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : Interectable
{
    public override void Interection(GameObject gameObject)
    {
        base.Interection(gameObject);
        NpcDialog<DialogUI>();
    }

    public override void UIPopUp(bool active)
    {
        base.UIPopUp(active);
    }

    public virtual void NpcDialog<T> () where T : BaseUI
    {
        if (!Managers.UI.IsActiveUI<T>())
        {
            Managers.UI.OpenUI<T>(new BaseUIData());
            Managers.Game._cantInputKey = true;
        }
    }
}
