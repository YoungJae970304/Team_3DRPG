using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoldItemData
{
    public int Gold => _gold;

    [SerializeField] int _gold;
}
