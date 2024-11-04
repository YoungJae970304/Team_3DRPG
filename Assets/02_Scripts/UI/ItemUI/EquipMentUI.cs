using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquipMentUI : ItemDragUI
{//장비 슬롯의 아이템 정보를 담고 관리할 딕셔너리
    Dictionary<string, EquipmentItemData> _equipMentsDick = new Dictionary<string, EquipmentItemData>();
    Dictionary<string, EquipmentItem> _playerEquipsDick;
    [SerializeField] List<EquipmentSlot> _slots = new List<EquipmentSlot>();//미리 지정해둔 슬롯
    [SerializeField] TextMeshProUGUI Atk;
    private void Update()
    {
        //Atk.text = Managers.Game._player._playerStatManager.ATK.ToString();
        Atk.text = _playerEquipsDick.Count.ToString();
    }
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        _playerEquipsDick = Managers.Game._player.GetComponent<Inventory>().EquipMents;
        foreach (EquipmentSlot slot in _slots)
        {//슬롯 정보 저장
            AddSlot(slot);
        }
        StatSum();
    }
    public void StatSum() {//장비의 스탯을 전부 합해서 장비스탯을 계산하는 메서드
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
        Managers.Game._player._playerStatManager._equipStat = equipStat;
    }
    public void AddSlot(EquipmentSlot equipmentSlot) {//장비 슬롯 추가
        if (_equipMentsDick.ContainsKey(equipmentSlot.name)) { return; }//중복 방지
        _equipMentsDick.Add(equipmentSlot.name, new EquipmentItemData());
       equipmentSlot.itemChangedAction += ()=>ChangedItem(equipmentSlot);
        if (equipmentSlot.Item != null) {
            _equipMentsDick[equipmentSlot.name] = equipmentSlot.Item.Data as EquipmentItemData;
        }
    }
    public void ChangedItem(EquipmentSlot equipmentSlot) {//슬롯의 아이템 변경시 아이템 정보 갱신
        
        if (equipmentSlot.Item != null)
        {
            _equipMentsDick[equipmentSlot.name] = equipmentSlot.Item.Data as EquipmentItemData;
            _playerEquipsDick[equipmentSlot.name] = equipmentSlot.Item as EquipmentItem;
        }
        else {
            _equipMentsDick.Remove(equipmentSlot.name);
            _playerEquipsDick.Remove(equipmentSlot.name);
        }
        StatSum();
        Managers.Sound.Play("ETC/ui_equipped_item");
    }
}
