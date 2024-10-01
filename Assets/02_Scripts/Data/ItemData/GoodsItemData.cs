using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsItemData : ItemData
{
    public string FlavorText { get { return _flavorText; } set { _flavorText = value; } }
    //기타 아이템 설명
    [Multiline]
    string _flavorText;
}
