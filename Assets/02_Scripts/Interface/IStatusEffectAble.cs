using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatusEffectAble
{
    public ITotalStat Targetstat { get;}
    public Transform TargetTr { get;}
    public bool ChangeStateToString(string state);
}
