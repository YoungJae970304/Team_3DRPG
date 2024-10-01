using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Interect : BaseUI
{
    enum GameObjects
    {
        Image,
    }

    Stat _stat;
    private void Awake()
    {
        Bind<GameObject>(typeof(GameObjects));
    }
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform parent = transform.parent;
        //transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y);
        transform.rotation = Camera.main.transform.rotation;
    }
}
