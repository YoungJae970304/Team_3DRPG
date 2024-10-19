using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    DeongeonType _curLevel;
    DataTableManager _tableManager;

    public int _monsterData1;
    public int _monsterData2;
    public int _monsterData3;
    private void Awake()
    {
        _tableManager = Managers.DataTable;
    }
    // Start is called before the first frame update
    void Start()
    {

        _curLevel = Managers.Game._selecDungeonLevel;
        MonsterSpawn();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MonsterSpawn()
    {
        Logger.LogError("실험1");
        foreach (var Dungeon in _tableManager._DungeonData)
        {
            Logger.LogError("실험2 들어옴?");
            if (Dungeon.Index == (int)_curLevel)
            {
                _monsterData1 = Dungeon.MonsterType1;
                _monsterData2 = Dungeon.MonsterType2;
                _monsterData3 = Dungeon.MonsterType3;
                Logger.LogError($"{_monsterData1},{_monsterData2},{_monsterData3}잘 들어감");
            }

            Managers.Resource.Instantiate($"Prefabs/Enemy/{_monsterData1}", gameObject.transform);
            //Logger.LogError("생성안됨");
            Managers.Resource.Instantiate($"Prefabs/Enemy/{_monsterData2}", gameObject.transform);
            // Logger.LogError("생성안됨2");
            Managers.Resource.Instantiate($"Prefabs/Enemy/{_monsterData3}", gameObject.transform);
            //Logger.LogError("생성안됨3");

        }
    }

}
