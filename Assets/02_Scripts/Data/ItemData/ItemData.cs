using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    //아이템이 공용으로 사용할 정보들
    public enum ItemType
    {
        Equipment,//0, 1, 2
        Potion,//3
        Booty,//4
    }

    public int ID => _id;
    public string Name => _name;
    public Sprite IconSprite => _iconSprite;
    public int MaxAmount => _maxAmount;

    [SerializeField] ItemType _itemType;
    //최대 소지 갯수
    [SerializeField] int _maxAmount = 99;
    //아이템 번호(type)
    [SerializeField] int _id;
    //아이템 이름
    [SerializeField] string _name;
    //아이템 아이콘
    [SerializeField] Sprite _iconSprite;
}
