using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
public class StatusEffectManager : MonoBehaviour
{
    List<StatusEffect> _buff=new List<StatusEffect>();
    List<StatusEffect> _deBuff = new List<StatusEffect>();
    List<Type> _immunitys = new List<Type>();
    public ITotalStat _totalStat;
    [SerializeField]RectTransform _iconTr;

    public void SpawnEffect<T>(int duration,params int[] value) where T : StatusEffect
    {
        if (_immunitys.Contains(typeof(T))) { return; }
        //버프와디버프를 합산하고 그 안에 새로 생성하려는 타입이 이미 있으면 효과를 더하고 없을경우 새로 생성한다.
        StatusEffect newEffect =  _buff.Union(_deBuff).Where(effect => effect.GetType() == typeof(T)).FirstOrDefault();
        if (newEffect != null) {
            newEffect.AddEffect(duration, value);
        }
        else{
            GameObject effectIcon = Managers.Resource.Instantiate("StatusEffect", _iconTr);
            newEffect = effectIcon.AddComponent<T>();
            newEffect.Init(_totalStat, duration, value);
            newEffect._removeEffectAction += DeleteEffect;
            switch (newEffect.type)
            {
                case Define.StatusEffectType.Buff:
                    _buff.Add(newEffect);
                    break;
                case Define.StatusEffectType.DeBuff:
                    _deBuff.Add(newEffect);
                    break;
            }
        }
        
    }
    [ContextMenu("슬로우 테스트")]
    public void test() {
        SpawnEffect<SlowEffect>(100,5);
    }

    //상태이상이 삭제될때 실행될 함수
    private void DeleteEffect(StatusEffect statusEffect) {
        switch (statusEffect.type) {
            case Define.StatusEffectType.Buff:
                _buff.Remove(statusEffect);
                break;

                case Define.StatusEffectType.DeBuff:
                _deBuff.Remove(statusEffect);
                break;
            
        }
    }
}
