using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GoodsItemData : ItemData
{
    public string FlavorText;
    //기타 아이템 설명
    [Multiline]
    string _flavorText;
}
