using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager 
{
    public Player _player = null;

    public Define.PlayerType _playerType;

    public List<Monster> _monsters = new List<Monster>();

    public bool _isActiveDialog = true;
    public bool _cantInputKey = false;
    public bool _firstTuto = true;

    public DeongeonType _selecDungeonLevel;

    public void AddMonsterOnNowScene()
    {
        _monsters.Clear();
        _monsters.AddRange(Object.FindObjectsOfType<Monster>());
    }

    // 플레이어와의 거리에 따라 Linq 사용해 몬스터 List를 정렬
    public List<Monster> SortMonsterList()
    {
        if (_player == null)
        {
            return _monsters;
        }

        Vector3 playerPos = _player.transform.position;

        _monsters = _monsters.OrderBy(monster => (monster.transform.position - playerPos).sqrMagnitude).ToList();

        return _monsters;
    }

    // 타입에 맞는 캐릭터 생성
    public void PlayerCreate()
    {
        Logger.LogWarning($"플레이어 생성 시점 확인{Managers.Game._playerType}");
        GameObject meleePlayer = Managers.Resource.Instantiate("Player/MeleePlayer");
        GameObject magePlayer = Managers.Resource.Instantiate("Player/MagePlayer");

        // 데이터 로드
        Managers.Data.LoadData<PlayerSaveData>();
 
        switch (Managers.Game._playerType)
        {
            case Define.PlayerType.Melee:
                Managers.Game._player = meleePlayer.GetComponent<Player>();
                meleePlayer.SetActive(true);
                magePlayer.SetActive(false);
                break;
            case Define.PlayerType.Mage:
                Managers.Game._player = magePlayer.GetComponent<Player>();
                meleePlayer.SetActive(false);
                magePlayer.SetActive(true);
                break;
            default:
                Logger.LogError("생성할 플레이어가 없습니다.");
                break;
        }

        if (!TitleCanvasUI._isNewGame)
        {
            Vector3 loadPlayerPos = PlayerPosSetData.PlayerPosSetLoad();
            var playerTransfrom = Managers.Game._player?.transform;
            if (playerTransfrom != null)
            {
                playerTransfrom.position = loadPlayerPos;
                //Logger.Log($"저장된 위치로 이동{loadPlayerPos}");
            }
            else
            {
                Logger.LogError("플레이어를 못찾았습니다.");
            }
        }
        Managers.Resource.Instantiate("Player/VirtualCameras");
    }

    public void PlayerPosSet(Transform spawnPos)
    {
        _player._cc.enabled = false;

        _player.transform.position = spawnPos.position;

        _player._cc.enabled = true;
    }
}
