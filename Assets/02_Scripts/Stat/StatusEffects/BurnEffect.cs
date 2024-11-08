using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnEffect : StatusEffect
{
    int damage = 0;
    float dot = 1f;
    protected override string IconPath { get; set; } = "";
    public override void Init(IStatusEffectAble target, float duration, params int[] value)
    {
        base.Init(target, duration, value);
        damage = value[0];
        type = Define.StatusEffectType.DeBuff;
    }

    public override void AddEffect(float duration, params int[] value)
    {
        _duration += duration;
        damage += value[0];
    }

    public override void Effect()
    {
        StartCoroutine(burn());
    }

    public override void UnEffect()
    {
        StopAllCoroutines();
    }

    IEnumerator burn() {
        IDamageAlbe damageAlbe;
        while (true) {
            yield return new WaitForSeconds(dot);
            damageAlbe =  _target.TargetTr.GetComponent<IDamageAlbe>();
            if (damageAlbe != null) {
                damageAlbe.Damaged(damage);
            }

        }
    
    }
   
}
