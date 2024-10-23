using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEffect : StatusEffect
{
    float _slowAmount;
    protected override string IconPath { get; set; } = "Icon/11012.png";

    public override void Init(ITotalStat target, float duration, params int[] value)
    {
        
        _slowAmount =  value[0];
        base.Init(target, duration, value);
        Logger.LogError(_slowAmount.ToString());
    }

    public override void AddEffect(float duration, params int[] value)
    {
        duration += duration;
        _slowAmount += value[0];
        _target.MoveSpeed = -value[0];
        Logger.LogError(_slowAmount.ToString());
    }

    public override void Effect()
    {
        _target.MoveSpeed = -_slowAmount;
        Logger.LogError(_slowAmount.ToString());
    }

    public override void UnEffect()
    {
        _target.MoveSpeed = _slowAmount;
    }

}
