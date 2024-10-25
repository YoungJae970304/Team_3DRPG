using UnityEngine;

public class Interectable : MonoBehaviour
{
    [SerializeField] public Canvas UI;

    public virtual void Interection(GameObject gameObject)
    {
        Debug.Log(name);
    }

    public virtual void UIPopUp(bool active)
    {
        UI.enabled = active;
    }
}
 