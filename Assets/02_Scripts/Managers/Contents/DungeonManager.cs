using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public int _monsterCount; //던전 클리어 조건을 위한 변수
    //시작할때 던전에서 몬스터가 생성될때 더해지고
    //죽을 때 감소하는 변수 (0이되면 클리어가됨) //
    //-- 바로클리어를 막기위해 bool변수 추가해주면좋을듯
    public bool _startCheck = false;
    public bool _bossCheck = false;
    Player _player;
    [SerializeField] DeongeonType _curLevel;
    public GameObject _bossSpawn;
    public GameObject _easyDungeonSpawn;
    public GameObject _normalDungeonSpawn;
    public GameObject _hardDungeonSpawn;
    public GameObject _bossDungeonWall;
    public GameObject _easyDungeonWall;
    public GameObject _normalDungeonWall;
    public GameObject _hardDungeonWall;
    BossHPBar _bossHPBar;
    [SerializeField] Transform _playerSpawnPos;
    [SerializeField] Transform _playerBossDungeonSpawnPos;
    GameManager _game;
    TextMeshProUGUI _remainMonsterValue;
    private void Awake()
    {
        _remainMonsterValue = GetComponentInChildren<TextMeshProUGUI>();
        PubAndSub.Subscrib("DungeonFail", FalseDungeon);
    }
    private void OnEnable()
    {
        _monsterCount = 0;
        _player = Managers.Game._player;
        _game = Managers.Game;
        _curLevel = _game._selecDungeonLevel;
        //DungeonCheck();
        SpawnCheck();
        DungeonCheck();

    }
    public void Clear()
    {
        _monsterCount = 0;

        if (_bossSpawn != null && _bossSpawn.activeSelf)
        {
            _bossSpawn.SetActive(false);
            _bossDungeonWall.SetActive(false);
            _bossHPBar?.CloseUI();
            
        }
        else if (_easyDungeonSpawn != null && _easyDungeonSpawn.activeSelf)
        {
            _easyDungeonSpawn.SetActive(false);
            _easyDungeonWall.SetActive(false);
        }
        else if (_normalDungeonSpawn != null && _normalDungeonSpawn.activeSelf)
        {
            _normalDungeonSpawn.SetActive(false);
            _normalDungeonWall.SetActive(false);
        }
        else if(_hardDungeonSpawn != null && _hardDungeonSpawn.activeSelf)
        {
            _hardDungeonSpawn.SetActive(false);
            _hardDungeonWall.SetActive(false);
        }
        Managers.Game._monsters.Clear();

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
        //FalseDungeon();
        BossCheck();
        BossHPBar();
    }
    public void SpawnCheck()
    {
        if (_curLevel == DeongeonType.Boss)
        {
            Managers.Game.PlayerPosSet(_playerBossDungeonSpawnPos.position);
        }
        else
        {
            Managers.Game.PlayerPosSet(_playerSpawnPos.position);
        }
    }
    public void DungeonCheck()
    {
        if (_curLevel == DeongeonType.Boss)
        {
            _bossSpawn.SetActive(true);
            _bossDungeonWall.SetActive(true);
        }
        else if (_curLevel == DeongeonType.Easy)
        {
            _easyDungeonSpawn.SetActive(true);
            _easyDungeonWall.SetActive(true);
        }
        else if (_curLevel == DeongeonType.Normal)
        {
            _normalDungeonSpawn.SetActive(true);
            _normalDungeonWall.SetActive(true);
        }
        else
        {
            _hardDungeonSpawn.SetActive(true);
            _hardDungeonWall.SetActive(true);
        }
    }
    public void BossCheck()
    {
        for (int i = 0; i < Managers.Game._monsters.Count; i++)
        {
            if (Managers.Game._monsters[i]._monsterID == 99999)
            {
                _bossCheck = true;
            }
            else
            {
                _bossCheck = false;
            }
        }
    }
    public void BossHPBar()
    {
        if (_bossCheck && Managers.Game._monsters[0]._mStat.ChaseRange >
            (Managers.Game._monsters[0].transform.position - Managers.Game._player.transform.position).magnitude)
        {
            if (_bossHPBar != null)
            {
                return;
            }
            BossHPBarData data = new BossHPBarData();
            data.Monster = Managers.Game._monsters[0];
            _bossHPBar = Managers.UI.OpenUI<BossHPBar>(data, false);
        }
        else
        {
            Logger.LogWarning("2312321");
            _bossHPBar?.CloseUI();
            _bossHPBar = null;
        }
    }
    public void ClearDungeon()
    {
        if (_monsterCount <= 0 && _startCheck == true)
        {
            //던전 UI활성화
            //for (int i = 0; i < Managers.Game._monsters.Count; i++)
            //{
            //    if (Managers.Game._monsters[i].gameObject.activeSelf && !Managers.Game._monsters[i]._dieCheck)
            //    {
            //        Managers.Game._monsters[i].Die(Managers.Game._monsters[i].gameObject);
            //    }
            //}
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
            Managers.Sound.Play("ETC/ui_dungeon_clear");
        }
    }
    public async void FalseDungeon()
    {
        if (_monsterCount > 0 && _player._curState == PlayerState.Dead && _startCheck == true)
        {
            //던전 UI활성화
            for (int i = 0; i < Managers.Game._monsters.Count; i++)
            {
                if (Managers.Game._monsters[i].gameObject.activeSelf)
                {
                    Managers.Game._monsters[i].Pooling(Managers.Game._monsters[i].gameObject);
                }
            }
            Managers.Game._monsters.Clear();
            await Task.Delay(1000);
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
        _remainMonsterValue.text = $"남은 몬스터 수 : {_monsterCount}";
    }
    public void CountMinus()
    {
        _monsterCount -= 1;
        _remainMonsterValue.text = $"남은 몬스터 수 : {_monsterCount}";
    }
}
