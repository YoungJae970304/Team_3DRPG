using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsItemData : ItemData
{
    public string FlavorText { get { return _flacorText; } set { _flacorText = value; } }
    //기타 아이템 설명
    [Multiline]
    string _flacorText;
}
