using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffect : StatusEffect
{
    int _healAmount = 0;
    protected override string IconPath { get ; set ; }

    public override void Init(IStatusEffectAble target, float duration, params int[] value)
    {
        _healAmount = value[0];
        base.Init(target, duration, value);
        
    }
    public override void AddEffect(float duration, params int[] value)
    {
       
        _healAmount = value[0];
        Effect();
    }
    public override void Effect()
    {
        _target.Targetstat.HP += ((_target.Targetstat.MaxHP*_healAmount)/100);
        Logger.Log(_target.Targetstat.HP);
        Logger.Log(_healAmount);
    }

    public override void UnEffect()
    {
        
    }

}
