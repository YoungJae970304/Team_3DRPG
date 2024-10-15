using TMPro;
using UnityEngine;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;
using UnityEngine.UI;


public class DungeonType : BaseUI
{
    [Header("Dungeon관련 변수")]
    public DataTableManager _dataTableManager;
    public DeongeonType _deongeonLevel;
    DungeonData _dungeonData;
    string _dungeonName;
    public int _monsterType1;
    public int _monsterType2;
    public int _monsterType3;
    public int _dungeonID;
    [Header("UI이미지 관련 변수")]
    public GameObject _mainMonster;
    public TextMeshProUGUI _dungeonText;
    public GameObject _monsterImageType;
    private void Awake()
    {
        _dataTableManager = Managers.DataTable;
        _deongeonLevel = DeongeonType.Easy;
    }
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        
        
    }
    private void OnEnable()
    {
        SwitchDungeonID(_deongeonLevel);
        
    }
    /*private void OnDisable()
    {
        _deongeonLevel = DeongeonType.Easy;
        SwitchDungeonID(_deongeonLevel);
        DungeonUITest();
    }*/
    public void SwitchDungeonID(DeongeonType curType)
    {
        Logger.LogError("실행확인");
        foreach (var dungeonType in _dataTableManager._DungeonData)
        {
           
            if(dungeonType == null)
            {
                Logger.LogError("말이되냐");
                return;
            }
            switch (curType)
            {
                case DeongeonType.Easy:
                    if(dungeonType.Index == (int)curType)
                    {
                        _dungeonID = dungeonType.ID;
                        Logger.LogError($"{_dungeonID}값은 들어감");
                    }
                    break;
                case DeongeonType.Normal:
                    if (dungeonType.Index == (int)curType)
                    {
                        _dungeonID = dungeonType.ID;
                        Logger.LogError($"{_dungeonID}얜 2번");
                    }
                    break;
                case DeongeonType.Hard:
                    if (dungeonType.Index == (int)curType)
                    {
                        _dungeonID = dungeonType.ID;
                        Logger.LogError($"{_dungeonID}얜 3번");
                    }
                    break;
                case DeongeonType.Boss:
                    if (dungeonType.Index == (int)curType)
                    {
                        _dungeonID = dungeonType.ID;
                        Logger.LogError($"{_dungeonID}얜 4번");
                    }
                    break;

            }
            
        }
        DungeonUITest(_dungeonID);
    }
    public void DungeonUITest(int ID)
    {
        //아이템 데이터 테이블에서 ID에 맞는 아이템 찾기
        foreach (var dungeonType in _dataTableManager._DungeonData)
        {
            if (dungeonType == null)
            {
                Logger.LogError("값안들어간다");
                return;
            }

            Logger.LogError($"{dungeonType.ID},{ID} 다른가?");
            if (dungeonType.ID == ID)
            {
                _dungeonName = dungeonType.DungeonName;
                _monsterType1 = dungeonType.MonsterType1;
                _monsterType2 = dungeonType.MonsterType2;
                _monsterType3 = dungeonType.MonsterType3;
                Logger.LogError($"{_dungeonName}");
                Logger.LogError($"{_monsterType1}");
                Logger.LogError($"{_monsterType2}");
                Logger.LogError($"{_monsterType3}");
                break;
            }
        }
        _dungeonText.text = _dungeonName;
        Image mainMonster = _mainMonster.GetComponent<Image>();
        mainMonster.sprite = Managers.Resource.Load<Sprite>($"Prefabs/Enemy/Patern/{_monsterType1}");
        //던전이름text = dungeonName;
        for (int i = _monsterType1; i <= _monsterType3; i++)
        {
            GameObject dungeonMonsterType = Managers.Resource.Instantiate("UI/MonsterImage", _monsterImageType.transform);
            Image dungeonMonsterImage = dungeonMonsterType.GetComponent<Image>();
            dungeonMonsterImage.sprite = Managers.Resource.Load<Sprite>($"Prefabs/Enemy/Patern/{i}"); //추후 경로 변경
            //dungeonMonsterImage.rectTransform.localPosition = Vector3.zero;
        }
    }

}
