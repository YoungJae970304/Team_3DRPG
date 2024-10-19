using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonUI : BaseUI
{
    enum DungeonUIButton
    {
        EntryBtn,
    }
    enum DungeonUIText
    {
        SelectDungeonName,
    }
    enum SelectDungeonType
    {
        Dungeon70001,
        Dungeon70002,
        Dungeon70003,
        Dungeon70004,
    }
    enum DungeonUIImage
    {
        SelectDungeonMainMonster,
        Monster1,
        Monster2,
        Monster3,
        Monster4,
    }


    [Header("버튼 관련 변수")]
    SceneBtnController _sceneBtnController;
    Dictionary<string, int> _buttonType = new Dictionary<string, int>();
    [Header("던전 내 몬스터 이미지 관련 변수")]
    List<Image> _indungeonMonsterImage = new List<Image>();
    [Header("Dungeon관련 변수")]
    public DataTableManager _dataTableManager;
    public DeongeonType _deongeonLevel;
    int _dungeonID;
    public GameObject _dungeonTypeview;
    //DungeonData _dungeonData;
    string _dungeonName;
    public int _monsterType1;
    public int _monsterType2;
    public int _monsterType3;
    [Header("UI이미지 관련 변수")]
    public GameObject _monsterImageType;
    [Header("UI 관련 변수")]
    bool _Makecheck = false;
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        //Logger.LogError("123213");
        //만약 던전UI가(이게 붙어있는 게임오브젝트가)없다면
        //시작 작업을 실행(버튼생성, 이 오브젝트 파괴불가, 우측 UI level_easy로 초기설정)
        _dataTableManager = Managers.DataTable;
        _deongeonLevel = DeongeonType.Easy;
        _sceneBtnController = new SceneBtnController();
        //여기에 button, 이미지 생성 들어갈거임 //필요한거 List에 버튼하고 이미지 담아두기

        if (!_Makecheck)
        {
            MakeDungeUIElement();
        }



    }
    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);
        Logger.LogError("실행중");
        Bind<Button>(typeof(DungeonUIButton));
        Bind<TextMeshProUGUI>(typeof(DungeonUIText));
        Bind<Button>(typeof(SelectDungeonType));
        Bind<Image>(typeof(DungeonUIImage));
        //Bind<Image>(typeof(InDungeonMonster));
       
        
        GetButton((int)DungeonUIButton.EntryBtn).onClick.AddListener(() => _sceneBtnController.OnClickSceneChangeBtn("Dungeon"));//여기에 입장 관련 함수가 들어감
        DungeonUITest(SwitchDungeonID((int)_deongeonLevel));
        Logger.LogError("1");
        DungeonButtonBind();
        Logger.LogError("2");

    }

    public void DungeonButtonBind()
    {
        GetButton((int)SelectDungeonType.Dungeon70001).onClick.AddListener(() => DungeonUITest(SwitchDungeonID(_buttonType[this.name])));
        GetButton((int)SelectDungeonType.Dungeon70002).onClick.AddListener(() => DungeonUITest(SwitchDungeonID(_buttonType[this.name])));
        GetButton((int)SelectDungeonType.Dungeon70003).onClick.AddListener(() => DungeonUITest(SwitchDungeonID(_buttonType[this.name])));
        GetButton((int)SelectDungeonType.Dungeon70004).onClick.AddListener(() => DungeonUITest(SwitchDungeonID(_buttonType[this.name])));
    }
    public void MakeDungeUIElement()
    {
        foreach (var dungeon in _dataTableManager._DungeonData) //데이터 테이블 가져오기
        {
            GameObject dungeonType = Managers.Resource.Instantiate("UI/DeongeonType", _dungeonTypeview.transform);
            // resource에 있는 instantiate호출. inspector창에 넣어놓은 부모 하위로 생성
            dungeonType.name = $"Dungeon{dungeon.ID}";
            //던전 이름 바꾸기 (Datatable의 ID값
            dungeonType.GetComponentInChildren<TextMeshProUGUI>().text = dungeon.DungeonName;
            _buttonType.Add(dungeonType.name, dungeon.Index);//딕셔너리에 오브젝트이름으로 키값설정. value는 index값 가져오기


            GameObject monster = Managers.Resource.Instantiate("UI/MonsterImage", _monsterImageType.transform);
            monster.name = $"Monster{dungeon.Index}";
            //Logger.LogError(monster.name);
            Image monsterImage = monster.GetComponent<Image>();
            _indungeonMonsterImage.Add(monsterImage);
            // 몬스터 이미지 리스트에 추가 
            //monster.SetActive(false);


        }

        _Makecheck = true;

    }
    public int SwitchDungeonID(int index) //던전 ID받는 함수
    {
        Logger.LogError("실행확인");
        foreach (var dungeonType in _dataTableManager._DungeonData)
        {

            if (dungeonType == null)
            {
                Logger.LogError("말이되냐");

            }
            if (dungeonType.Index == index) //인덱스 값이 현재foreach로 돌아가고있는 index값과 같다면
            {
                _dungeonID = dungeonType.ID; //아이디는 그 당시에 들어갈 ID로 셋팅
                Logger.LogError($"{_dungeonID}값은 들어감");
            }
        }
        return _dungeonID;
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
            if (dungeonType.ID == ID) //던전아이디가 돌아가고있는 foreach문의 id와 같다면
            {
                _dungeonName = dungeonType.DungeonName; //세팅
                _monsterType1 = dungeonType.MonsterType1; //세팅
                _monsterType2 = dungeonType.MonsterType2; //세팅
                _monsterType3 = dungeonType.MonsterType3; //세팅
                break;
            }
        }

        GetText((int)DungeonUIText.SelectDungeonName).text = _dungeonName;
        
        GetImage((int)DungeonUIImage.SelectDungeonMainMonster).sprite = Managers.Resource.Load<Sprite>($"Prefabs/Enemy/Patern/{_dungeonID}");//대표이미지가 던전아이디랬던거같음
                                                                                                                                             //밑에 생성은 빠질거임 로드만 남을거임
        
        
        GetImage((int)DungeonUIImage.Monster1).sprite = Managers.Resource.Load<Sprite>($"Prefabs/Enemy/Patern/{_monsterType1}");
        Logger.LogError($"{GetImage((int)DungeonUIImage.Monster1).name}");
        Logger.LogError($"여기는 들어옴?{_monsterType1}");
        GetImage((int)DungeonUIImage.Monster2).sprite = Managers.Resource.Load<Sprite>($"Prefabs/Enemy/Patern/{_monsterType2}");
        Logger.LogError($"{GetImage((int)DungeonUIImage.Monster2).name}");
        Logger.LogError($"여기 들어옴?3{_monsterType2}");
        GetImage((int)DungeonUIImage.Monster3).sprite = Managers.Resource.Load<Sprite>($"Prefabs/Enemy/Patern/{_monsterType3}");
        Logger.LogError($"{GetImage((int)DungeonUIImage.Monster3).name}");
        Logger.LogError($"여기 들어옴?3{_monsterType3}");
        //GetImage((int)InDungeonMonster.Monster4).sprite = Managers.Resource.Load<Sprite>($"Prefabs/Enemy/Patern/{i}");


    }
}

