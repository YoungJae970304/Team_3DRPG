using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEffect : StatusEffect
{
    float _slowAmount;
    protected override string IconPath { get; set; } = "Icon/11012.png";

    public override void Init(IStatusEffectAble target, float duration, params int[] value)
    {
        base.Init(target, duration, value);
        _slowAmount =  value[0];
    }

    public override void AddEffect(float duration, params int[] value)
    {
        duration += duration;
        _slowAmount += value[0];
        _target.Targetstat.MoveSpeed = -value[0];
        Logger.LogError(_slowAmount.ToString());
    }

    public override void Effect()
    {

        _target.Targetstat.MoveSpeed = -_slowAmount;
        Logger.LogError(_slowAmount.ToString());
    }

    public override void UnEffect()
    {
        _target.Targetstat.MoveSpeed += _slowAmount;
    }

}
