using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public abstract class StatusEffect : MonoBehaviour
{
    public Define.StatusEffectType type;
    protected IStatusEffectAble _target;
    protected float _duration;
    public bool isNovation = true;
    protected abstract string IconPath { get; set; }
    protected Image _effectIcon;
    protected Image _effectTimerImg;
    protected TextMeshProUGUI _effectTimerTxt;

    public Action<StatusEffect> _removeEffectAction;
    public virtual void Init(IStatusEffectAble target,float duration, params int[] value ) {
        _duration = duration;
        _target = target; 
        _effectIcon = GetComponent<Image>();
        
        _effectIcon.sprite = Managers.Resource.Load<Sprite>(IconPath);
        _effectTimerTxt=GetComponentInChildren<TextMeshProUGUI>();
        _effectTimerTxt.text = _duration.ToString(); ;
        if (duration <= 0) {
            _effectIcon.enabled = false;
            _effectTimerTxt.text = "";
        }
        Effect();
    }
    private void Update()
    {
        Timer();
    }
    protected void OnDisable()
    {
        UnEffect();
    }
    public abstract void Effect();
    public abstract void UnEffect();
    public abstract void AddEffect(float duration, params int[] value);
    protected void Timer() {
        _duration -= Time.deltaTime;
        _effectTimerTxt.text = _duration.ToString();
        if (_duration <= 0) {
            _removeEffectAction?.Invoke(this);
            Managers.Resource.Destroy(this.gameObject);
        }
    }
}
