using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using static UnityEditor.Progress;
using static Cinemachine.DocumentationSortingAttribute;
public enum DeongeonLevel
{
    Easy,
    Normal,
    Hard,
}
// 아이템 클래스
public class Drop : MonoBehaviour
{
    public static Drop _drop;
    private void Awake()
    {
        _drop = this;
    }


    public List<string> sample = new List<string>();
    public Dictionary<int, int> dropValue;
    private void Start()
    {
        sample = new List<string>
        {
            "11001", "11002", "11003", "11004", // 1성 무기
            "12001", "12002", "12003", "12004", // 2성 무기
            "13001", "13002", "13003", "13004", // 3성 무기
            "21001", "21002", "21003", "21004", // 1성 방어구
            "22001", "22002", "22003", "22004", // 2성 방어구
            "23001", "23002", "23003", "23004", // 3성 방어구
            "31001", "31002", "31003", "31004", // 1성 악세서리
            "32001", "32002", "32003", "32004", // 2성 악세서리
            "33001", "33002", "33003", "33004"  // 3성 악세서리
        };
        dropValue = new Dictionary<int, int>
        {
            { 1, 60 },
            { 2, 30 },
            { 3, 10 },
        };

    }
    public static class WeightedRandomizer
    {
        public static WeightedRandomizer<R> From<R>(Dictionary<R, int> spawnRate)
        {
            return new WeightedRandomizer<R>(spawnRate);
        }
    }

    public class WeightedRandomizer<T>
    {
        private static System.Random _random = new System.Random();
        private Dictionary<T, int> _weights;

        public WeightedRandomizer(Dictionary<T, int> weights)
        {
            _weights = weights;
        }

        public T TakeOne()
        {
            var sortedSpawnRate = Sort(_weights);
            int sum = 0;
            foreach (var spawn in _weights)
            {
                sum += spawn.Value;
            }

            int roll = _random.Next(0, sum);

            T selected = sortedSpawnRate[sortedSpawnRate.Count - 1].Key;
            foreach (var spawn in sortedSpawnRate)
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
    public int GetDeongeonLevel(DeongeonLevel level)
    {
        return level switch
        {
            DeongeonLevel.Easy => 1,
            DeongeonLevel.Normal => 2,
            DeongeonLevel.Hard => 3,
            _ => -1
        };
    }
    public string DropItemSelect(DeongeonLevel level) //아이템 string랜덤으로 받아오기
    {
        int maxTier = GetDeongeonLevel(level); //아이템이 나올 최고 티어는 던젼레벨이 결정

        if (maxTier <= 0)
        {
            Logger.Log("이런 난이도는 없습니다.");
            return null;
        }

        Dictionary<string, int> itemDrop = new Dictionary<string, int>(); 
        foreach (var randomItem in sample)
        {
            int itemTier = int.Parse(randomItem[1].ToString());

            if (itemTier <= maxTier)
            {
                itemDrop[randomItem] = 10; // 드랍 확률 설정
            }
        }

        // 수정: 최종 드랍할 아이템을 저장할 변수
        string selectedItem = null;

        foreach (var itemTier in dropValue.Keys) //키를 itemTier에 담고
        {
            if (itemTier <= maxTier) //그 아이템 티어가 스테이지로 설정한 최대값보다 작거나 같다면
            {
                var randomValue = new Dictionary<string, int>(); // 새로운 딕셔너리를 만들고
                foreach (var result in sample) //샘플 list를 result에 담아서
                {
                    int currentTier = int.Parse(result[1].ToString()); //result의(sample에 속한 string화한것) 앞에서부터 2번째 순번의 글자를 currentTier에 int화해서 담음
                    if (currentTier == itemTier && itemDrop.ContainsKey(result))//null검사
                    {
                        randomValue[result] = dropValue[itemTier];//randomvalue에 result(string)값이 들어갈 때 그 int값이 dropvalue[itemTier]에서 나온 int값이 들어가도록 설정
                    }
                }

                if (randomValue.Count > 0) //randomValue에 값이 있는지 확인하고
                {
                    var randomizer = WeightedRandomizer.From(randomValue); //가중치를 기반으로 랜덤 선택을 준비
                    selectedItem = randomizer.TakeOne(); // 가중치를 고려하여 선택된 아이템을 저장
                    break; //아이템을 선택한 후 루프를 종료
                }
            }
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
