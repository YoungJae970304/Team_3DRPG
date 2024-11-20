using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public abstract class StatusEffect : MonoBehaviour
{
    public Define.StatusEffectType type;            //상태효과 타입 buff,debuff
    protected IStatusEffectAble _target;            //상태효과 적용대상
    protected float _duration;                      //지속시간
    protected abstract string IconPath { get; set; }//아이콘경로
    protected Image _effectIcon;                    //아이콘 이미지
    protected TextMeshProUGUI _effectTimerTxt;      //지속시간 텍스트

    public Action<StatusEffect> _removeEffectAction;//상태효과 종료시 실행될 콜백

    //상태효과 시작시 매개변수로 효과초기화
    public virtual void Init(IStatusEffectAble target,float duration, params int[] value ) {
        _duration = duration;
        _target = target; 
        _effectIcon = GetComponent<Image>();
        _effectTimerTxt = GetComponentInChildren<TextMeshProUGUI>();
        if (transform.parent == null)//부모가 없으면 표시X
        {
            _effectIcon.enabled = false;
            _effectTimerTxt.enabled = false;
        }
        else {
            //아이콘 경로가 있으면 이미지 로드
            if (!string.IsNullOrEmpty(IconPath)) {
                string[] path = IconPath.Split("_");
                Sprite[] icon = Resources.LoadAll<Sprite>(path[0] + "_" + path[1]);

                _effectIcon.sprite = icon[int.Parse(path[2])];
            }

            _effectTimerTxt.text = _duration.ToString("F1"); ;
            if (duration <= 0)//지속시간이 0보다 작으면 표시X
            {
                _effectIcon.enabled = false;
                _effectTimerTxt.text = "";
            }
        }
        //효과실행
        Effect();
    }
    //아이콘의 부모를 변경하기 위한 함수
    public void ChangeParent(Transform transform) {
        if (transform == null)
        {
            _effectIcon.enabled = false;
            _effectTimerTxt.enabled = false;
        }
        else
        {
            transform.SetParent(transform);
            _effectIcon.sprite = Managers.Resource.Load<Sprite>(IconPath);

            _effectTimerTxt.text = _duration.ToString(); ;
        }
        
    }

    private void Update()
    {
        //지속시간 타이머
        Timer();
    }
    protected void OnDisable()
    {
        //효과 비활성화
        UnEffect();
    }
    public abstract void Effect();//효과 적용 함수
    public abstract void UnEffect();//효과 해제 함수
    public abstract void AddEffect(float duration, params int[] value);//효과 중첩시 처리할 함수
    protected void Timer() {//지속시간 타이머 함수
        _duration -= Time.deltaTime;
        _effectTimerTxt.text = _duration.ToString("F1");
        if (_duration <= 0) {
            _removeEffectAction?.Invoke(this);
            Managers.Resource.Destroy(this.gameObject);
        }
    }
}
