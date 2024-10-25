using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEffect : StatusEffect
{
    protected override string IconPath { get; set ; }

    public override void AddEffect(float duration, params int[] value)
    {
        _duration += duration;
    }

    public override void Effect()
    {
        _target.ChangeStateToString("StatusEffect");
    }

    public override void UnEffect()
    {
        _target.ChangeStateToString("Idle");
    }

}
