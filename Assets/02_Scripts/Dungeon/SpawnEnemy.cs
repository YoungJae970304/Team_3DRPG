using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SpawnEnemy : MonoBehaviour
{
    
    DataTableManager _tableManager;
    public DungeonManager _dungeonManager;
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
    public virtual void Start()
    {
        _dungeonManager = FindObjectOfType<DungeonManager>();
        
        //_player.transform.position = transform.position;
        //Managers.Game.PlayerPosSet(transform);
        SetMonsterType();
        

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
            if (!_monsterMinValue.ContainsKey(data.MonsterType))
            {
                _monsterMinValue.Add(data.MonsterType, data.MinSpawn);
                _monsterMaxValue.Add(data.MonsterType, data.MaxSpawn);
            }
        }
    }
    public void MonsterSpawn(int i)
    {
        //Logger.LogError("실험1");
        string monstername;
 
        
            //Logger.LogError($"{_monsterType.Min().ToString()},{_monsterType.Max().ToString()}최소 최댓값");
            int randomSpawn = UnityEngine.Random.Range(_monsterMinValue[i], _monsterMaxValue[i]);
            //Logger.LogError($"{randomSpawn.ToString()}랜덤 숫자");
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
                    
                    break;
                case 4:
                    monstername = "BossBear";
                    MakeMonster(monstername, randomSpawn);
                    Logger.LogError($"{monstername}이름은들어가?3");
                break;
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
            Managers.Game._monsters.Add(monster);
            monster._makeMonster += _dungeonManager.CountPlus;
            monster._makeMonster?.Invoke();
            monster._dieMonster += _dungeonManager.CountMinus;
            monster.Init();
            
            mon.transform.position = new Vector3(transform.position.x + i, transform.position.y, transform.position.z);
            Logger.LogError($"{mon.transform.position}");
        }
    }
}
