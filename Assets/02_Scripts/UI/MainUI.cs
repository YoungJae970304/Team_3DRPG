using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : ItemUI
{
    Inventory _inventory;

    enum QuickItemSlots {
        ItemSlot_1,
        ItemSlot_2
    }

    protected override void Awake()
    {
        base.Awake();
        
    }
    private void Start()
    {
        _inventory = Managers.Game._player.gameObject.GetOrAddComponent<Inventory>();
        Bind<QuickItemSlot>(typeof(QuickItemSlots));

        foreach (QuickItemSlots quickItemSlot in Enum.GetValues(typeof(QuickItemSlots)))
        {
            Get<QuickItemSlot>((int)quickItemSlot).Inventory = _inventory;
        }
    }
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        Bind<QuickItemSlot>(typeof(QuickItemSlots));

        foreach (QuickItemSlots quickItemSlot in Enum.GetValues(typeof(QuickItemSlots))) {
            Get<QuickItemSlot>((int)quickItemSlot).Inventory = _inventory;
        }
    }
}
