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
        ItemManager = Managers.Game._player.GetComponent<Inventory>();

        Opentest();
        Close();
    }
    public override void Clear()
    {
        

    }
    [ContextMenu("OpenTest")]
    public void Opentest() {
        inventory = Managers.UI.OpenUI<InventoryUI>(new BaseUIData());
    }
    [ContextMenu("OpenNewTest")]
    public void OpenNewtest()
    {
        inventory = Managers.UI.OpenUI<InventoryUI>(new BaseUIData(),true,true);
    }

    [ContextMenu("Inserttest")]
    public void Inserttest() {
        Item item2 = new EquipmentItem(itemData2);
        ItemManager.InsertItem(item2);
        Item item = new EquipmentItem(ItemData);
        ItemManager.InsertItem(item);
    }
    [ContextMenu("Removetest")]
    public void Remove()
    {
        Managers.UI.CloseCurrFrontUI(true);
    }
    [ContextMenu("Close")]
    public void Close() {
        inventory.CloseUI();
    }
    [ContextMenu("CloseLast")]
    public void CloseLast()
    {
        Managers.UI.CloseCurrFrontUI();
    }
}
