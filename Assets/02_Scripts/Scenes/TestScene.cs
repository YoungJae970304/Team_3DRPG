using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestScene : BaseScene
{
    [SerializeField] Inventory ItemManager;
    [SerializeField] InventoryUI inventory;

    [SerializeField] Transform SpawnPos;

    protected override void Init()
    {
        base.Init();
        //Managers.Game.PlayerCreate();
        Managers.Game.PlayerPosSet(SpawnPos);
        Managers.Game._player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //Logger.Log(Managers.Game._player.name);
        ItemManager = Managers.Game._player.gameObject.GetOrAddComponent<Inventory>();
        Managers.UI.OpenUI<MainUI>(new BaseUIData(),false);
        //Opentest();
        //Close();

        ShopUIData shopUIData = new ShopUIData();
        shopUIData._itemCode = new List<(int, int)>();
        shopUIData._itemCode.Add((11001, 1));
        shopUIData._itemCode.Add((43001, 2));
        //Managers.UI.OpenUI<ShopUI>(shopUIData);
    }
    public override void Clear()
    {
        

    }

    [ContextMenu("OpenTest")]
    public void Opentest() {
        // 인벤토리 여는 것 I? ( 풀링 )
        Managers.UI.OpenUI<EquipMentUI>(new BaseUIData());
    }
    [ContextMenu("OpenNewTest")]
    public void OpenNewtest()
    {
        // 완전 새로운 오브젝트를 생성 후 여는 것 ( 풀링은 되는데 무조건 생성 )
        Managers.UI.OpenUI<EquipMentUI>(new BaseUIData());
    }

    [ContextMenu("Inserttest")]
    public void Inserttest() {
        ItemManager.InsertItem(Item.ItemSpawn(42001,15));
        ItemManager.InsertItem(Item.ItemSpawn(11010, 15));
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
    }
    [ContextMenu("CloseLast")]
    public void CloseLast()
    {
        //ESC
        Managers.UI.CloseCurrFrontUI();
    }
}
