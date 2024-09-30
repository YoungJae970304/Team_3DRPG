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
    public enum DeongeonRank
    {
        NomalWeapon = 11,
        NomalArmor = 21,
        NomalAccesary = 31,
        RareWeapon = 12,
        RareArmor = 22,
        RareAccesary = 32,
        UniqueWeapon = 13,
        UniqueArmor = 23,
        UniqueAccesary = 33,
    }
    public Dictionary<DropTable, int> _dropTable = new Dictionary<DropTable, int>();
    public DropTable _dropValue = DropTable.Noluck;
    public Dictionary<PDropTable, int> _pDropTable = new Dictionary<PDropTable, int>();
    public PDropTable _pDropValue = PDropTable.Noluck;
    public Dictionary<ItemType, int> _dropType = new Dictionary<ItemType, int>();
    public ItemType _itemType = ItemType.Weapon;
    public Dictionary<ItemGrade, int> _dropGrade = new Dictionary<ItemGrade, int>();
    public ItemGrade _itemGrade = ItemGrade.Nomal;
    public Dictionary<DeongeonRank, List<string>> iteming = new Dictionary<DeongeonRank, List<string>>();
    public List<string> sample = new List<string>();
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
        foreach(var item in sample)
        {
           int check = int.Parse(item)/1000;
            if(check == 11)
            {
                iteming.Add(DeongeonRank.NomalWeapon, sample);
            }
            else if(check == 12)
            {
                iteming.Add(DeongeonRank.RareWeapon, sample);
            }
            else if(check == 13)
            {
                iteming.Add(DeongeonRank.UniqueWeapon, sample);
            }
        }
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
    public (DropTable dropTable, int value) DropValue()
    {
        var selectedDropTable = WeightedRandomizer.From(_dropTable).TakeOne();
        int value = _dropTable[selectedDropTable];
        _dropValue = selectedDropTable;
        return (selectedDropTable, value);
    }
    public (ItemGrade itemGrade, int value) SelectGrade()
    {
        var selectedItemGrade = WeightedRandomizer.From(_dropGrade).TakeOne();
        int value = _dropGrade[selectedItemGrade];
        _itemGrade = selectedItemGrade;
        return (selectedItemGrade, value);
    }
    public (ItemType itemtype, int value) SelectType()
    {
        var selectedItemType = WeightedRandomizer.From(_dropType).TakeOne();
        int value = _dropType[selectedItemType];
        _itemType = selectedItemType;
        return (selectedItemType, value);
    }
    public (PDropTable pDropTable, int value) PDropValue()
    {
        var selectedPDropTable = WeightedRandomizer.From(_pDropTable).TakeOne();
        int value = _pDropTable[selectedPDropTable];
        _pDropValue = selectedPDropTable;
        return (selectedPDropTable, value);
    }
    public void DropItemSelect()
    {
        var (pDroppedValue, pValue) = PDropValue();
        var (droppedValue, value) = DropValue();
        if(pDroppedValue == PDropTable.Product)
        {
            //JsonUtility.FromJson<제이슨 값 가져오는 class>(가져올 값{몬스터일듯});
        }
        if(droppedValue == DropTable.Equipment)
        {
            SelectType();
            
            var(itemtype, randomvalue) = SelectType();
            switch (itemtype)
            {
                case ItemType.Weapon:
                    Debug.Log("무기소환");
                    ItemGradeSelect();
                    break;
                case ItemType.Armor:
                    Debug.Log("방어구 소환");
                    ItemGradeSelect();
                    break;
                case ItemType.Accesary:
                    Debug.Log("악세서리 소환");
                    ItemGradeSelect();
                    break;
            }
            Debug.Log($"{SelectGrade().ToString()}{SelectType().ToString()}");
            //이제 아이템 값을 받아와서 땅에 떨구는 코드만 작성하면됨.
        }
    }
    public void ItemGradeSelect()
    {
        var (itemGrade, gradeValue) = SelectGrade();
        switch (itemGrade)
        {
            case ItemGrade.Nomal:
                Debug.Log("1성");
                //아이템 생성 후 별값 넣기
                break;
            case ItemGrade.Rare:
                Debug.Log("2성");
                //아이템 생성 후 별값 넣기
                break;
            case ItemGrade.Unique:
                Debug.Log("3성");
                //아이템 생성 후 별값 넣기
                break;
        }
    }
}
