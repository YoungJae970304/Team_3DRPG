using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController :IInterectable
{
    
    public void Interection(GameObject gameObject)
    {
        Debug.Log(name);
    }

    public void UIPopUp(bool active)
    {
        UI.SetActive(active);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
