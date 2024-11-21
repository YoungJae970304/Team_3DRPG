using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
public class StatusEffectManager : MonoBehaviour
{
    [SerializeField] List<StatusEffect> _buff=new List<StatusEffect>();     //버프 관리 리스트
    [SerializeField] List<StatusEffect> _deBuff = new List<StatusEffect>(); //디버프 관리 리스트
    List<Type> _immunitys = new List<Type>();                               //상태효과 면역타입 리스트
    public IStatusEffectAble _target;                                       //상태효과 적용대상
    public RectTransform _iconTr;                                           //상태효과 아이콘 표시 위치
    private void Awake()
    {
        _target = GetComponent<IStatusEffectAble>();
    }
    //제네릭으로 상태효과를 적용하는 함수
    public void SpawnEffect<T>(float duration,params int[] value) where T : StatusEffect
    {
        if (_immunitys.Contains(typeof(T))) { return;}
        //버프와디버프를 합산하고 그 안에 새로 생성하려는 타입이 이미 있으면 효과를 더하고 없을경우 새로 생성한다.
        StatusEffect newEffect =  _buff.Union(_deBuff).Where(effect => effect.GetType() == typeof(T)).FirstOrDefault();
        if (newEffect != null) {//중복되는 상태효과면 효과 중첩
                newEffect.AddEffect(duration, value);
        }
        else{//중복된 상태효과가 없으면 효과 생성
            //프리팹을 생성하고 프리팹에 상태에 맞는 스크립트를 붙이고 타입에 따라 리스트에 삽입
            GameObject effectIcon = Managers.Resource.Instantiate("StatusEffect", _iconTr);
            newEffect = effectIcon.AddComponent<T>();
            newEffect.Init(_target, duration, value);
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
