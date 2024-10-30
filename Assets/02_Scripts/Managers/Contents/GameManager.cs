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

    public DeongeonType _selecDungeonLevel;
    //인벤토리는 세이브 따로 해줄 예정이라 게임매니저에서 해줄
    public InventorySaveData _inventorySaveData = new InventorySaveData();
    public PlayerSaveData _playerSave = new PlayerSaveData();

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
        Managers.Resource.Instantiate("Player/VirtualCameras");
        

        switch (Managers.Game._playerType)
        {
            case Define.PlayerType.Melee:
                GameObject meleePlayer = Managers.Resource.Instantiate("Player/MeleePlayer");
                if(meleePlayer != null)
                {
                    _playerSave.SaveData();
                }
                break;
            case Define.PlayerType.Mage:
                GameObject magePlayer = Managers.Resource.Instantiate("Player/MagePlayer");
                if(magePlayer != null)
                {
                    _playerSave.SaveData();
                }
                break;
            default:
                Logger.LogError("생성할 플레이어가 없습니다.");
                break;
        }
    }

    public void PlayerPosSet(Transform spawnPos)
    {
        _player._cc.enabled = false;

        _player.transform.position = spawnPos.position;

        _player._cc.enabled = true;
    }

    public void SaveData<T>() where T : class, IData
    {
        T dataToSave = GetData<T>();
        if(dataToSave != null)
        {
            bool success = dataToSave.SaveData();
            if (success)
            {
                Logger.Log($"{typeof(T).Name} 저장");
            }
            else
            {
                Logger.LogError($"{typeof(T).Name} 저장 실패");
            }
        }
        else
        {
            Logger.LogWarning($"{typeof(T).Name} 저장할 데이터가 없음");
        }
    }

    public void LoadData<T>() where T : class, IData
    {
        T dataToLoad = GetData<T>();
        if (dataToLoad != null)
        {
            bool success = dataToLoad.LoadData();
            if (success)
            {
                Logger.Log($"{typeof(T).Name} 로드");
            }
            else
            {
                Logger.LogError($"{typeof(T).Name} 로드 실패");
            }
        }
        else
        {
            Logger.LogWarning($"{typeof(T).Name} 로드할 데이터가 없음");
        }
    }

    T GetData<T>() where T : class, IData
    {
        if (typeof(T) == typeof(InventorySaveData))
        {
            return _inventorySaveData as T;
        }
        else if (typeof(T) == typeof(PlayerSaveData))
        {
            return _playerSave as T;
        }
        return null;
    }
}
