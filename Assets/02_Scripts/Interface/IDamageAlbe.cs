using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageAlbe {
    public void Damaged(int amount);
    public StatusEffectManager StatusEffect {get;set;}
}
