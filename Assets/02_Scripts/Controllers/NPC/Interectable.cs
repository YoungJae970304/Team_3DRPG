using UnityEngine;

public class Interectable : MonoBehaviour
{
    [SerializeField] public Canvas UI;
    [SerializeField] public Canvas DungeonDialogUI;
    [SerializeField] public DialogDungeonUI _dialogDungeonUI;
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
        if (_dialogDungeonUI != null)
        {
            _dialogDungeonUI.StartDialog();
        }
    }
}
