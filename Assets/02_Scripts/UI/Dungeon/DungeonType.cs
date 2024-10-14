using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;

public class DungeonType : BaseUI
{
    [Header("Dungeon관련 변수")]
    public DataTableManager _dataTableManager;
    public DeongeonType _deongeonLevel;
    public DungeonData _dungeonData;
    string _dungeonName;
    int _monsterType1;
    int _monsterType2;
    int _monsterType3;
    int _dungeonID;
    [Header("UI이미지 관련 변수")]
    public GameObject _mainMonster;
    public TextMeshProUGUI _dungeonText;
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        _deongeonLevel = DeongeonType.Easy;
    }
    private void OnEnable()
    {
        SwitchDungeonID(_deongeonLevel);
        DungeonUITest();
    }
    private void OnDisable()
    {
        _deongeonLevel = DeongeonType.Easy;
        SwitchDungeonID(_deongeonLevel);
        DungeonUITest();
    }
    public void SwitchDungeonID(DeongeonType curType)
    {
        foreach (var dungeonType in _dataTableManager._DungeonData)
        {
            switch (curType)
            {
                case DeongeonType.Easy:
                    if(dungeonType.Index == (int)curType)
                    {
                        _dungeonID = dungeonType.ID;
                    }
                    break;
                case DeongeonType.Normal:
                    if (dungeonType.Index == (int)curType)
                    {
                        _dungeonID = dungeonType.ID;
                    }
                    break;
                case DeongeonType.Hard:
                    if (dungeonType.Index == (int)curType)
                    {
                        _dungeonID = dungeonType.ID;
                    }
                    break;
                case DeongeonType.Boss:
                    if (dungeonType.Index == (int)curType)
                    {
                        _dungeonID = dungeonType.ID;
                    }
                    break;

            }
        }
            
    }
    public void DungeonUITest()
    {
        //아이템 데이터 테이블에서 ID에 맞는 아이템 찾기
        foreach (var dungeonType in _dataTableManager._DungeonData)
        {
            if (dungeonType == null)
                return;
            
            if (dungeonType.ID == _dungeonID)
            {
                _dungeonName = dungeonType.DungeonName;
                _monsterType1 = dungeonType.MonsterType1;
                _monsterType2 = dungeonType.MonsterType2;
                _monsterType3 = dungeonType.MonsterType3;
            }
        }
        //던전이름text = dungeonName;
        Managers.Resource.Load<Sprite>("");
        Managers.Resource.Instantiate("");
        Managers.Resource.Instantiate("");
        Managers.Resource.Instantiate("");
    }

}
