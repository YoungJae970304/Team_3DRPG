using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Interect : UI_Base
{
    enum GameObjects
    {
        Image,
    }

    Stat _stat;
    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }

    // Update is called once per frame
    void Update()
    {
        Transform parent = transform.parent;
        //transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y);
        transform.rotation = Camera.main.transform.rotation;
    }
}
