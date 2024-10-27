using UnityEngine;

public class NpcController : Interectable
{
    public override void Interection(GameObject gameObject)
    {
        base.Interection(gameObject);
    }

    public override void UIPopUp(bool active)
    {
        base.UIPopUp(active);
    }

    public virtual void NpcDialog<T>() where T : BaseUI
    {
        if (!Managers.UI.IsActiveUI<T>())
        {
            Managers.UI.OpenUI<T>(new BaseUIData());
            Managers.Game._cantInputKey = true;
        }

        //T dialogUI = Managers.UI.GetActiveUI<T>() as T;

        //if (dialogUI == null)
        //{
        //    Managers.UI.OpenUI<T>(new BaseUIData());
        //    Managers.Game._cantInputKey = true;
        //}
    }
}
