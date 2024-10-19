using System;
using UnityEngine;

public class Interectable : MonoBehaviour
{
    [SerializeField] public Canvas UI;
    [SerializeField] public Canvas DialogCanvas;

    public virtual void Interection(GameObject gameObject)
    {
        Debug.Log(name);
    }

    public virtual void UIPopUp(bool active)
    {
        UI.enabled = active;
    }

    public virtual void Dialogues()
    {
        DialogUI dialogUI = Managers.UI.GetActiveUI<DialogUI>() as DialogUI;

        if(dialogUI == null)
        {
            Managers.UI.OpenUI<DialogUI>(new BaseUIData());
            Managers.Game._isActiveDialog = true;
            Managers.Game._player._isMoving = false;
            Logger.Log($"플레이어 무브{Managers.Game._player._isMoving}");
        }
        else
        {
            Managers.UI.CloseUI(dialogUI);
            Logger.LogWarning($"{dialogUI}닫힘");
            Managers.Game._isActiveDialog = false;
            Managers.Game._player._isMoving = true;
            Logger.Log($"플레이어 무브{Managers.Game._player._isMoving}");
        }
    }

}
