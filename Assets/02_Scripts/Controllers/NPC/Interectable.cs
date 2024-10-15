using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interectable : MonoBehaviour
{
    [SerializeField] public Canvas UI;
    [SerializeField] public Canvas DungeonDialog;
    public virtual void Interection(GameObject gameObject)
    {
        Debug.Log(name);
    }

    public virtual void UIPopUp(bool active)
    {
        UI.enabled=active;
    }

    public virtual void DungeonNpcDialog()
    {
        DungeonDialog.gameObject.SetActive(true); 
    }
}
