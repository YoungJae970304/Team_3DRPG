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
        // 인벤토리 여는 것 I? ( 풀링 )
        Managers.UI.OpenUI<TestUI>(new BaseUIData());
    }
    [ContextMenu("OpenNewTest")]
    public void OpenNewtest()
    {
        // 완전 새로운 오브젝트를 생성 후 여는 것 ( 풀링은 되는데 무조건 생성 )
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
        // 오브젝트 자체를 삭제 ( 1번만 쓰는 UI 같은거 )
        Managers.UI.CloseCurrFrontUI(true);
    }
    [ContextMenu("Close")]
    public void Close() {
        // 버튼 이벤트
        inventory.CloseUI();
    }
    [ContextMenu("CloseLast")]
    public void CloseLast()
    {
        //ESC
        Managers.UI.CloseCurrFrontUI();
    }
}
