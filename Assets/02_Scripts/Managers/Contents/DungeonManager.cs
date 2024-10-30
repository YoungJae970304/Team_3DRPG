using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public int _monsterCount; //던전 클리어 조건을 위한 변수
    //시작할때 던전에서 몬스터가 생성될때 더해지고
    //죽을 때 감소하는 변수 (0이되면 클리어가됨) //
    //-- 바로클리어를 막기위해 bool변수 추가해주면좋을듯
    public bool _startCheck = false;
    Player _player;
    [SerializeField] DeongeonType _curLevel;
    public GameObject _bossSpawn;
    public GameObject _dungeonSpawn;
    public GameObject _bossDungeonWall;
    public GameObject _dungeonWall;
    [SerializeField] Transform _playerSpawnPos;
    [SerializeField] Transform _playerBossDungeonSpawnPos;
    GameManager _game;
    private void OnEnable()
    {
        _player = Managers.Game._player;
        _game = Managers.Game;
        _curLevel = _game._selecDungeonLevel;
        DungeonCheck();
        SpawnCheck();
        DungeonCheck();
    }
    private void OnDisable()
    {
        _monsterCount = 0;
        if (_bossSpawn != null || _dungeonSpawn != null)
        {
            if (_bossSpawn.activeSelf)
            {
                _bossSpawn.SetActive(false);
                _bossDungeonWall.SetActive(false);
            }
            else if (_dungeonSpawn.activeSelf)
            {
                _dungeonSpawn.SetActive(false);
            }
        }
    }
    private void Start()
    {
        _curLevel = Managers.Game._selecDungeonLevel;
        SpawnCheck();

    }
    private void Update()
    {
        ClearDungeon();
        // Logger.LogError($"{Managers.Game._monsters.Count}");
        FalseDungeon();
    }
    public void SpawnCheck()
    {
        if (_curLevel == DeongeonType.Boss)
        {
            Managers.Game.PlayerPosSet(_playerBossDungeonSpawnPos);
        }
        else
        {
            Managers.Game.PlayerPosSet(_playerSpawnPos);
        }
    }
    public void DungeonCheck()
    {
        if (_curLevel == DeongeonType.Boss)
        {
            _bossSpawn.SetActive(true);
            _bossDungeonWall.SetActive(true);
            Logger.LogError("벽체크");
        }
        else
        {
            _dungeonSpawn.SetActive(true);
        }
    }
    public void ClearDungeon()
    {
        if (_monsterCount <= 0 && _startCheck == true)
        {
            //던전 UI활성화
            InDungeonUI inDungeonUI = Managers.UI.GetActiveUI<InDungeonUI>() as InDungeonUI;
            if (inDungeonUI != null)
            {
                Managers.UI.CloseUI(inDungeonUI);
            }
            else
            {
                inDungeonUI = Managers.UI.OpenUI<InDungeonUI>(new BaseUIData());
                inDungeonUI._loadText.text = "Clear";
            }
            _startCheck = false;
        }
    }
    public void FalseDungeon()
    {
        if (_monsterCount > 0 && _player._playerStatManager._originStat.HP <= 0 && _startCheck == true)
        {
            //던전 UI활성화
            InDungeonUI inDungeonUI = Managers.UI.GetActiveUI<InDungeonUI>() as InDungeonUI;
            if (inDungeonUI != null)
            {
                Managers.UI.CloseUI(inDungeonUI);
            }
            else
            {
                inDungeonUI = Managers.UI.OpenUI<InDungeonUI>(new BaseUIData());
                inDungeonUI._loadText.text = "Lose";
            }
            _startCheck = false;
        }
    }
    public void CountPlus()
    {
        _monsterCount += 1;
        Logger.LogError($"{_monsterCount.ToString()}일단 더한숫자확인");
    }
    public void CountMinus()
    {
        _monsterCount -= 1;
        Logger.LogError($"{_monsterCount.ToString()}일단 뺀숫자확인");
    }
    public void DecideMonster(int ID)
    {

    }
}
