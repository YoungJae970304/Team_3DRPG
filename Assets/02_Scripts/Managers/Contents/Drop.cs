using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public enum DeongeonType
{
    Easy = 1,
    Normal = 2,
    Hard = 3,
    Boss = 4,
}
// 아이템 클래스
public class Drop : MonoBehaviour
{
    public static Drop _drop;
    private void Awake()
    {
        _drop = this;
    }
    
    public DeongeonType _deongeonLevel = DeongeonType.Easy;
    public Dictionary<int, float> dropValue;
    private void Start()
    {
        dropValue = new Dictionary<int, float> // 드랍 확률
        {
            { 1, 60 },
            { 2, 30 },
            { 3, 10 },
        };
        _deongeonLevel = Managers.Game._selecDungeonLevel;
    }
    public static class WeightedRandomizer
    {
        public static WeightedRandomizer<R> From<R>(Dictionary<R, float> spawnRate)
        {
            return new WeightedRandomizer<R>(spawnRate);
        }
    }
    public class WeightedRandomizer<T>
    {
        private static System.Random _random = new System.Random();
        private Dictionary<T, float> _weights;

        public WeightedRandomizer(Dictionary<T, float> weights)
        {
            _weights = weights;
        }

        public T TakeOne()
        {
            //var sortedSpawnRate = Sort(_weights);
            float sum = _weights.Values.Sum();
            float roll = (float)_random.NextDouble() * sum;
            
            //T selected = _weights.Keys.First();
            T selected = default;
            foreach (var spawn in _weights)
            {
                if (roll < spawn.Value)
                {
                    selected = spawn.Key;
                    break;
                }
                roll -= spawn.Value;
            }
            return selected;
        }

        private List<KeyValuePair<T, int>> Sort(Dictionary<T, int> weights)
        {
            var list = new List<KeyValuePair<T, int>>(weights);

            list.Sort(
                delegate (KeyValuePair<T, int> firstPair,
                            KeyValuePair<T, int> nextPair)
                {
                    return firstPair.Value.CompareTo(nextPair.Value);
                }
                );

            return list;
        }
    }
    public int GetDeongeonLevel(DeongeonType level)
    {
        return level switch
        {
            DeongeonType.Easy => 1,
            DeongeonType.Normal => 2,
            DeongeonType.Hard => 3,
            _ => -1
        };
    }
    public string DropItemSelect(DeongeonType level, List<string> sample) //아이템 string랜덤으로 받아오기
    {
        int maxTier = GetDeongeonLevel(level); //아이템이 나올 최고 티어는 던젼레벨이 결정
        if (maxTier <= 0)
        {
            Logger.Log("이런 난이도는 없습니다.");
            return null;
        }
        Dictionary<string, float> itemDrop = new Dictionary<string, float>();
        foreach (var randomItem in sample)
        {
            int itemTier = int.Parse(randomItem[1].ToString());
            if (itemTier <= maxTier)
            {

                itemDrop[randomItem] = 10; // 드랍 확률 설정
            }
        }
        float totalWeight = itemDrop.Values.Sum();
        foreach (var item in itemDrop.Keys.ToList())
        {
            int itemTier = int.Parse(item[1].ToString());
            itemDrop[item] *= dropValue[itemTier] / totalWeight;
           
        }
        string selectedItem = null;
        if (itemDrop.Count > 0) //randomValue에 값이 있는지 확인하고
        {
            var randomizer = WeightedRandomizer.From(itemDrop); //가중치를 기반으로 랜덤 선택을 준비
            selectedItem = randomizer.TakeOne(); // 가중치를 고려하여 선택된 아이템을 저장
        }
        // 선택된 아이템이 있을 때만 로그 출력
        if (selectedItem != null)
        {
            Logger.Log($"선택된 아이템: {selectedItem}");
        }
        else
        {
            Logger.Log("드랍된 아이템이 없습니다.");
        }
        return selectedItem; //선택된 아이템을 반환
    }
}