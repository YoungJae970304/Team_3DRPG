using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNpc : NpcController
{
    public override void Interection(GameObject gameObject)
    {
        base.Interection(gameObject);
        Managers.Sound.RandSoundsPlay("NPC/shop_talk_1", "NPC/shop_talk_2");
        NpcDialog<DialogUIShop>();
    }
}
