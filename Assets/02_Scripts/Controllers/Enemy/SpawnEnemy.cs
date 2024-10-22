using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SpawnEnemy : MonoBehaviour
{
    DeongeonType _curLevel;
    DataTableManager _tableManager;
    DungeonManager _dungeonManager;
    HashSet<int> _monsterType = new HashSet<int>();
    public Dictionary<int, int> _monsterMinValue = new Dictionary<int, int>();
    public Dictionary<int, int> _monsterMaxValue = new Dictionary<int, int>();
    public int _monsterData1;
    public int _monsterData2;
    Player _player;
    private void Awake()
    {
        _tableManager = Managers.DataTable;
        _player = Managers.Game._player;
    }
    // Start is called before the first frame update
    void Start()
    {
        _dungeonManager = FindObjectOfType<DungeonManager>();
        _curLevel = Managers.Game._selecDungeonLevel;
        //_player.transform.position = transform.position;
        Managers.Game.PlayerPosSet(transform);
        SetMonsterType();
        MonsterSpawn();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetMonsterType()
    {
        foreach (var data in _tableManager._MonsterData)
        {
            _monsterType.Add(data.MonsterType);
        }
    }
    public void MonsterSpawn()
    {
        //Logger.LogError("실험1");
        string monstername;
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            foreach (var MonsterData in _tableManager._MonsterData)
            {
                if (!_monsterMinValue.ContainsKey(MonsterData.MonsterType))
                {
                    _monsterMinValue.Add(MonsterData.MonsterType, MonsterData.MinSpawn);
                    _monsterMaxValue.Add(MonsterData.MonsterType, MonsterData.MaxSpawn);
                }
            }


            /*GameObject test = Managers.Resource.Instantiate($"Enemy/Slime", gameObject.transform);
            //Logger.LogError("생성안됨");
            test.transform.position = transform.position;
            Managers.Resource.Destroy(test);
            GameObject test1 = Managers.Resource.Instantiate($"Enemy/Slime", gameObject.transform);
            test1.transform.position = transform.position * 2;
            Managers.Resource.Destroy(test1);
            // Logger.LogError("생성안됨2");
            GameObject test2 = Managers.Resource.Instantiate($"Enemy/Slime", gameObject.transform);
            test2.transform.position = transform.position * -2;
            Managers.Resource.Destroy(test2);
            //Logger.LogError("생성안됨3");*/
        }
        for (int i = _monsterType.Min(); i <= _monsterType.Max() - 1; i++)
        {
            Logger.LogError($"{_monsterType.Min().ToString()},{_monsterType.Max().ToString()}최소 최댓값");
            int randomSpawn = UnityEngine.Random.Range(_monsterMinValue[i], _monsterMaxValue[i]);
            Logger.LogError($"{randomSpawn.ToString()}랜덤 숫자");
            switch (i)
            {
                case 1:
                    monstername = "Slime";
                    Logger.LogError($"{monstername}이름은들어가?");
                    MakeMonster(monstername,randomSpawn);
                    
                    break;
                case 2:
                    monstername = "Goblem";
                    MakeMonster(monstername, randomSpawn);
                    Logger.LogError($"{monstername}이름은들어가?2");
                    break;
                case 3:
                    monstername = "Ork";
                    MakeMonster(monstername, randomSpawn);
                    Logger.LogError($"{monstername}이름은들어가?3");
                    break;
            }
        }



    }
    public void MakeMonster(string monsterName, int randomValue)
    {
        for(int i = 0; i < randomValue; i++)
        {
            GameObject mon = Managers.Resource.Instantiate($"Enemy/{monsterName}",gameObject.transform);
            if (mon == null)
            {
                Logger.LogError($"Failed to instantiate monster: {monsterName}");
                
                return; // null인 경우 메서드 종료
            }
            Monster monster = mon.GetComponent<Monster>();
            monster._makeMonster += _dungeonManager.CountPlus;
            monster._makeMonster?.Invoke();
            monster._dieMonster += _dungeonManager.CountMinus;
            mon.transform.position = new Vector3(transform.position.x + i, transform.position.y, transform.position.z);
            Logger.LogError($"{mon.transform.position}");
        }
    }
}
