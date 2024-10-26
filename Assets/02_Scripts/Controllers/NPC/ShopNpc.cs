using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNpc : NpcController
{
    public override void Interection(GameObject gameObject)
    {
        base.Interection(gameObject);
        NpcDialog<DialogUIShop>();
    }
}
