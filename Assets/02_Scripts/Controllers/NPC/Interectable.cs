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
        DialogUI dialogDungeonUI = Managers.UI.GetActiveUI<DialogUI>() as DialogUI;

        if(dialogDungeonUI == null)
        {
            Managers.UI.OpenUI<DialogUI>(new BaseUIData());
            Managers.Game._isActiveDialog = true;
            Managers.Game._player._isMoving = false;
        }
        else
        {
            Managers.UI.CloseCurrFrontUI(dialogDungeonUI);
            Managers.Game._isActiveDialog = true;
        }
    }

}
