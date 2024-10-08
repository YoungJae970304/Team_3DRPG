using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

[Serializable]
public class GoldItemData
{
    public int Gold { get { return _gold; } set { _gold = value; } }

    int _gold;
}
