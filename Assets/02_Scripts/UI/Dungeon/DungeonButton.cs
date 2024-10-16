using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;

public class DungeonButton : BaseUI
{
    [Header("Dungeon관련 변수")]
    public DataTableManager _dataTableManager;
    public DeongeonType _deongeonLevel;
    int _dungeonID;
    public GameObject _dungeonTypeview;
    private void Awake()
    {
        _dataTableManager = Managers.DataTable;
    }
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
    }

    private void OnEnable()
    {
        MakeDungeonType();
    }
    public void MakeDungeonType()
    {
        foreach(var dungeon in _dataTableManager._DungeonData)
        {
            GameObject dungeonType;
            dungeonType = Managers.Resource.Instantiate("UI/DeongeonType", _dungeonTypeview.transform);
            dungeonType.GetComponent<TextMeshProUGUI>().text = dungeon.DungeonName;

        }
        
        
    }
}
