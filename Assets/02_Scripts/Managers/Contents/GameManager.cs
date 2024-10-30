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
                    //첫 시작 플레이어 데이터 저장
                    Managers.Data.SaveData<PlayerSaveData>();
                }
                break;
            case Define.PlayerType.Mage:
                GameObject magePlayer = Managers.Resource.Instantiate("Player/MagePlayer");
                if(magePlayer != null)
                {
                    //첫 시작 플레이어 데이터 저장
                    Managers.Data.SaveData<PlayerSaveData>();
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
}
