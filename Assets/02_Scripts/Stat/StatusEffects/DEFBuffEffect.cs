using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEFBuffEffect : StatusEffect
{
    int _buffAmount = 0;
    protected override string IconPath { get ; set ; }

    public override void Init(IStatusEffectAble target, float duration, params int[] value)
    {
        base.Init(target, duration, value);
        _buffAmount = value[0];
    }

    public override void AddEffect(float duration, params int[] value)
    {
        _duration = duration;
        if (_buffAmount < value[0]) {
            _buffAmount = value[0];
        }
        UnEffect();
        Effect();
    }

    public override void Effect()
    {
        _target.Targetstat.DEF = _buffAmount;
    }

    public override void UnEffect()
    {
        _target.Targetstat.DEF = -_buffAmount;
    }
}
