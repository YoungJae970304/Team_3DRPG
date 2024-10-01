using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : BaseScene
{
    [SerializeField] Inventory ItemManager;
    [SerializeField] InventoryUI inventory;
    [SerializeField] ItemData ItemData,itemData2;
    protected override void Init()
    {
        base.Init();
        Managers.Game._player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Logger.Log(Managers.Game._player.name);

        Managers.UI.OpenUI<InventoryUI>(new BaseUIData());
         
    }
    public override void Clear()
    {
       
    }
    [ContextMenu("test")]
    public void test() {
        Item item2 = new EquipmentItem(itemData2);
        ItemManager.InsertItem(item2);
        Item item = new EquipmentItem(ItemData);
        ItemManager.InsertItem(item);
    }
    [ContextMenu("remove")]
    public void remove()
    {
        ItemManager.Remove(0, ItemData.ItemType.Potion);
        inventory.UpdateSlot();
    }
    [ContextMenu("close")]
    public void close() {
        Managers.UI.CloseUI(inventory);
    }
}
