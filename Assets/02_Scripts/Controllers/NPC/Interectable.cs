using UnityEngine;

public class Interectable : MonoBehaviour
{
    [SerializeField] public Canvas UI;
    [SerializeField] public Canvas DungeonDialogUI;
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
        var dialogDungeonUI = GameObject.Find("Dialog").GetComponent<DialogDungeonUI>();

        if (dialogDungeonUI != null)
        {
            dialogDungeonUI.StartDialog();
        }
    }
}
