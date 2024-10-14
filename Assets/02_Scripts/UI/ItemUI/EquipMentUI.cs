using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquipMentUI : ItemUI
{
    Dictionary<string, EquipmentItemData> _equipMentsDick = new Dictionary<string, EquipmentItemData>();//장비 슬롯을 담고 관리할 딕셔너리
    [SerializeField] List<EquipmentSlot> _slots = new List<EquipmentSlot>();//미리 지정해둔 슬롯
    [SerializeField] TextMeshProUGUI Atk;
    private void Update()
    {
        Atk.text = Managers.Game._player._playerStatManager.ATK.ToString();
    }
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        foreach (EquipmentSlot slot in _slots)
        {//슬롯 정보 저장
            AddSlot(slot);
        }
        StatSum();
    }
    public void StatSum() {
        PlayerStat equipStat = new PlayerStat();
        
        
        foreach (var item in _equipMentsDick.Values)
        {
            
            if (item != null) {
                equipStat.RecoveryHP += item.HealthRegen;
                equipStat.MaxMP += item.Mana;
                equipStat.RecoveryMP += item.ManaRegen;
                equipStat.DEF += item.Defense;
                equipStat.MaxHP += item.Health;
                equipStat.ATK += item.AttackPower;
            }
        }
        Logger.Log(equipStat.ATK);
        Managers.Game._player._playerStatManager._equipStat = equipStat;
        Logger.Log(Managers.Game._player._playerStatManager._equipStat.ATK);
    }
    public void AddSlot(EquipmentSlot equipmentSlot) {
        _equipMentsDick.Add(equipmentSlot.name, new EquipmentItemData());
       equipmentSlot.itemChangedAction += ()=>ChangedItem(equipmentSlot);
        if (equipmentSlot.Item != null) {
            _equipMentsDick[equipmentSlot.name] = equipmentSlot.Item.Data as EquipmentItemData;
        }
    }
    public void ChangedItem(EquipmentSlot equipmentSlot) {
        
        if (equipmentSlot.Item != null)
        {
            
            _equipMentsDick[equipmentSlot.name] = equipmentSlot.Item.Data as EquipmentItemData;
        }
        else {
            _equipMentsDick[equipmentSlot.name] = null;
        }
        StatSum();
    }
}
