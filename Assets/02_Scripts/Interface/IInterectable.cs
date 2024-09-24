using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IInterectable : MonoBehaviour
{
    [SerializeField] GameObject UI;
    public virtual void Interection(GameObject gameObject)
    {
        Debug.Log(name);
    }

    public virtual void UIPopUp(bool active)
    {
        UI.SetActive(active);
    }
}
