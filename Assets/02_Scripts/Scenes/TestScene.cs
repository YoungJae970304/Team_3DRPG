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

        
    }
    public override void Clear()
    {
       
    }
    [ContextMenu("test")]
    public void test() {
        Item item = new CountableItem(ItemData,60);
        ItemManager.InsertItem(item);
        Item item2 = new CountableItem(itemData2, 60);
        ItemManager.InsertItem(item2);
        inventory.UpdateSlot();
    }
    [ContextMenu("remove")]
    public void remove()
    {
        ItemManager.Remove(0, ItemData.ItemType.Potion);
        inventory.UpdateSlot();
    }
}
