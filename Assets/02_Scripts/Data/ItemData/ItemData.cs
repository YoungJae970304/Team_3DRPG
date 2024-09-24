using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    //아이템이 공용으로 사용할 정보들
    public int ID => _id;
    public string Name => _name;
    public string Tooltip => _tooltip;
    public Sprite IconSprite => _iconSprite;

    //아이템 번호
    [SerializeField] int _id;
    //아이템 이름
    [SerializeField] string _name;
    [Multiline]
    //아이템 설명
    [SerializeField] string _tooltip;
    //아이템 아이콘
    [SerializeField] Sprite _iconSprite;
}
