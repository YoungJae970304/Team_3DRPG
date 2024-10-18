using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonUI : BaseUI
{
    enum DungeonUIElement
    {
        CloseBtn,
        EntryBtn,
        SelectDungeonName,
        SelectDungeonMainMonster,
        SelectDungeonMonster,
        SelectDungeonType,
    }
    [Header("버튼 관련 변수")]
    SceneBtnController _sceneBtnController;
    [Header("Dungeon관련 변수")]
    public DataTableManager _dataTableManager;
    public DeongeonType _deongeonLevel;
    int _dungeonID;
    public GameObject _dungeonTypeview;
    DungeonData _dungeonData;
    string _dungeonName;
    public int _monsterType1;
    public int _monsterType2;
    public int _monsterType3;
    [Header("UI이미지 관련 변수")]
    public GameObject _mainMonster;
    public TextMeshProUGUI _dungeonText;
    public GameObject _monsterImageType;
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        //만약 던전UI가(이게 붙어있는 게임오브젝트가)없다면
        //시작 작업을 실행(버튼생성, 이 오브젝트 파괴불가, 우측 UI level_easy로 초기설정)
    }
    private void Awake()
    {
        _dataTableManager = Managers.DataTable;
        _deongeonLevel = DeongeonType.Easy;
        _sceneBtnController = new SceneBtnController();

        Bind<Button>(typeof(DungeonUIElement));
        Bind<Button>(typeof(DungeonUIElement));

        GetButton((int)DungeonUIElement.EntryBtn).onClick.AddListener(() =>_sceneBtnController.OnClickSceneChangeBtn("03_Dungeon"));//여기에 입장 관련 함수가 들어감
        GetButton((int)DungeonUIElement.CloseBtn).onClick.AddListener(() => CloseUI(this));//여기에 닫을때관련 함수가 들어가는듯
    }

    IEnumerator DungeonUIClose()
    {
        //이게 혼자인지 유효성검사 후 true가 되는 불변수를 false로 변경
        //이 스크립트가 붙은 gameobject setactive false //이게 managers.ui.closeUI(this)인듯
        //우측 UI전부다 easy난이도 일때로 변경
        return null; // 오류 방지용 추후 변경이나 삭제

    }
    public void MakeDungeonType()
    {
        foreach (var dungeon in _dataTableManager._DungeonData)
        {
            GameObject dungeonType;
            dungeonType = Managers.Resource.Instantiate("UI/DeongeonType", _dungeonTypeview.transform);
            dungeonType.GetComponent<TextMeshProUGUI>().text = dungeon.DungeonName;
            Bind<Button>(typeof(DungeonUIElement));

            //GetButton((int)DungeonUIElement.SelectDungeonType).onClick.AddListener(()=>);
        }


    }
    public void SwitchDungeonID(DeongeonType curType)
    {
        Logger.LogError("실행확인");
        foreach (var dungeonType in _dataTableManager._DungeonData)
        {

            if (dungeonType == null)
            {
                Logger.LogError("말이되냐");
                return;
            }
            switch (curType)
            {
                case DeongeonType.Easy:
                    if (dungeonType.Index == (int)curType)
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
            Bind<Image>(typeof(DungeonUIElement));
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
