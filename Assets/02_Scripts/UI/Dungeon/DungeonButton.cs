using TMPro;
using UnityEngine;

public class DungeonButton : BaseUI
{
    [Header("Dungeon관련 변수")]
    public DataTableManager _dataTableManager;
    public DeongeonType _deongeonLevel;
    int _dungeonID;
    public GameObject _dungeonTypeview;
    private void Awake()
    {

    }
    public override void Init(Transform anchor)
    {
        base.Init(anchor);

    }


    private void OnEnable()
    {
        _dataTableManager = Managers.DataTable;
        MakeDungeonType();

    }
    public void MakeDungeonType()
    {
        foreach (var dungeon in _dataTableManager._DungeonData)
        {
            GameObject dungeonType = Managers.Resource.Instantiate("UI/DeongeonType", _dungeonTypeview.transform);
            dungeonType.name = $"Dungeon{dungeon.ID}";
            dungeonType.GetComponentInChildren<TextMeshProUGUI>().text = dungeon.DungeonName;
        }


    }
}
