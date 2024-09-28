using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

// 아이템 클래스
public class Drop : MonoBehaviour
{
    public static Drop _drop;
    private void Awake()
    {
        _drop = this;
    }
    // 아이템 타입 열거형
    public enum ItemType
    {
        Weapon,
        Armor,
        Accesary,
    }
    public enum DropTable
    {
        Equipment,
        Noluck,
    }
    public enum PDropTable
    {
        Product,
        Noluck,
    }
    public enum ItemGrade
    {
        Nomal,
        Rare,
        Unique,
    }

    public Dictionary<DropTable, int> _dropTable = new Dictionary<DropTable, int>();
    public DropTable _dropValue = DropTable.Noluck;
    public Dictionary<PDropTable, int> _pDropTable = new Dictionary<PDropTable, int>();
    public PDropTable _pDropValue = PDropTable.Noluck;
    public Dictionary<ItemType, int> _dropType = new Dictionary<ItemType, int>();
    public ItemType _itemType = ItemType.Weapon;
    public Dictionary<ItemGrade, int> _dropGrade = new Dictionary<ItemGrade, int>();
    public ItemGrade _itemGrade = ItemGrade.Nomal;
    private void Start()
    {
        #region 드랍 테이블 더하기
        _dropTable.Add(DropTable.Equipment, 10);
        _dropTable.Add(DropTable.Noluck, 90);
        #endregion
        #region
        _pDropTable.Add(PDropTable.Product, 40);
        _pDropTable.Add(PDropTable.Noluck, 60);
        #endregion
        #region 드랍 타입 설정
        _dropType.Add(ItemType.Weapon, 30);
        _dropType.Add(ItemType.Armor, 30);
        _dropType.Add(ItemType.Accesary, 30);
        #endregion
        #region
        _dropGrade.Add(ItemGrade.Nomal, 60);
        _dropGrade.Add(ItemGrade.Rare, 30);
        _dropGrade.Add(ItemGrade.Unique, 10);
        #endregion
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
    public void DropValue()
    {
        _dropValue = WeightedRandomizer.From(_dropTable).TakeOne();
    }
    public void SelectGrade()
    {
        _itemGrade = WeightedRandomizer.From(_dropGrade).TakeOne();
    }
    public void SelectType()
    {
        _itemType = WeightedRandomizer.From(_dropType).TakeOne();
    }
    public void PDropValue()
    {
        _pDropValue = WeightedRandomizer.From(_pDropTable).TakeOne();
    }
}


