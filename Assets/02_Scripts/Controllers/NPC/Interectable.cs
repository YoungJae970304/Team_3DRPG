using UnityEngine;

public class Interectable : MonoBehaviour
{
    [SerializeField] public Canvas UI;
    [SerializeField] public Canvas DungeonDialogUI;
    [SerializeField] public DialogActive _dialogActive;
    public virtual void Interection(GameObject gameObject)
    {
        Debug.Log(name);
    }

    public virtual void UIPopUp(bool active)
    {
        UI.enabled = active;
    }

    public virtual void DungeonNpcDialog()
    {
        DialogDungeonUI dialogDungeonUI = Managers.UI.GetActiveUI<DialogDungeonUI>() as DialogDungeonUI;

        if (dialogDungeonUI != null)
        {
            Managers.UI.CloseUI(dialogDungeonUI);
        }
        else
        {
            Managers.UI.OpenUI<DialogDungeonUI>(new BaseUIData());
        }
    }
}
