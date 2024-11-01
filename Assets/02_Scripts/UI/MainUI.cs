using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : ItemDragUI
{
    Inventory _inventory;
    [SerializeField] RectTransform _icontr;
    enum QuickItemSlots {
        ItemSlot_1,
        ItemSlot_2
    }

    enum SkillSlots {
        SkillSlot_E,
        SkillSlot_R,
    }

    enum Sliders {
        HpBar,
        MpBar,

    }

    public SkillQuickSlot SkillSlot_E { get { return Get<SkillQuickSlot>((int)SkillSlots.SkillSlot_E); } }
    public SkillQuickSlot SkillSlot_R { get { return Get<SkillQuickSlot>((int)SkillSlots.SkillSlot_R); } }
    public QuickItemSlot ItemSlot_1 { get { return Get<QuickItemSlot>((int)QuickItemSlots.ItemSlot_1); } }
    public QuickItemSlot ItemSlot_2 { get { return Get<QuickItemSlot>((int)QuickItemSlots.ItemSlot_2); } }

    private void Start()
    {
        
        
        PubAndSub.Subscrib<int>("HP", HpChanged);
        PubAndSub.Subscrib<int>("MP", MpChanged);


    }
    public override void Init(Transform anchor)
    {
        Bind<QuickItemSlot>(typeof(QuickItemSlots));
        Bind<SkillQuickSlot>(typeof(SkillSlots));
        Bind<Slider>(typeof(Sliders));
        base.Init(anchor);
        _inventory = Managers.Game._player.gameObject.GetOrAddComponent<Inventory>();
        Managers.Game._player.StatusEffect._iconTr = _icontr;
        HpChanged(Managers.Game._player._playerStatManager.HP);
        MpChanged(Managers.Game._player._playerStatManager.MP);
        foreach (QuickItemSlots quickItemSlot in Enum.GetValues(typeof(QuickItemSlots)))
        {
            Get<QuickItemSlot>((int)quickItemSlot)._inventory = _inventory;
            _inventory.GetItemAction -= Get<QuickItemSlot>((int)quickItemSlot).UpdateSlotInfo;
            _inventory.GetItemAction += Get<QuickItemSlot>((int)quickItemSlot).UpdateSlotInfo;
        }
    }

    public void QuickslotUpdate() {
        foreach (QuickItemSlots quickItemSlot in Enum.GetValues(typeof(QuickItemSlots)))
        {
            Get<QuickItemSlot>((int)quickItemSlot).UpdateSlotInfo();
        }
    }

    private void HpChanged(int value) {
        Get<Slider>((int)Sliders.HpBar).value = (float)value / (float)Managers.Game._player._playerStatManager.MaxHP;
    }
    private void MpChanged(int value)
    {
        Get<Slider>((int)Sliders.MpBar).value = (float)value / (float)Managers.Game._player._playerStatManager.MaxMP;
    }
}
