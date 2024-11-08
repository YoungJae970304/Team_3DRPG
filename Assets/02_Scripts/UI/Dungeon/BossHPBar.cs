using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class BossHPBarData : BaseUIData {
    public Monster Monster;
}

public class BossHPBar : BaseUI
{
    public Monster _target;
    enum Sliders
    {
        HPbar,
    }
    enum Texts
    {
        HpPer,
    }
    enum Transforms {
        StatusEffects
    }
    [SerializeField]RectTransform _transform;
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        Bind<Slider>(typeof(Sliders));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Transform>(typeof(Transforms));
        BossHpChanged(Managers.Game._monsters[0]._mStat.HP);
        PubAndSub.Subscrib<int>("BossHP", BossHpChanged);
    }
    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);
        if (uiData is BossHPBarData) {
            _target = (uiData as BossHPBarData).Monster;
            _target.StatusEffect._iconTr = _transform;

        }
    }
    private void BossHpChanged(int value)
    {
        //Logger.LogError($"{(float)value / (float)Managers.Game._monsters[0]._mStat.MaxHP}값 확인");
        Get<Slider>((int)Sliders.HPbar).value = (float)value / (float)Managers.Game._monsters[0]._mStat.MaxHP;
        Get<TextMeshProUGUI>((int)Texts.HpPer).text = $"{((float)value / (float)Managers.Game._monsters[0]._mStat.MaxHP)*100}%";
    }
}
