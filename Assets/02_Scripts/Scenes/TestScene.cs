using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : BaseScene
{
    [SerializeField] ItemManager ItemManager;
    [SerializeField] Inventory inventory;
    [SerializeField] ItemData ItemData;
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
        inventory.UpdateSlot();
    }
    [ContextMenu("remove")]
    public void remove()
    {
        ItemManager.Remove(0, ItemData.ItemType.Potion);
        inventory.UpdateSlot();
    }
}
