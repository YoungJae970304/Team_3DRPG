using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : BaseUI
{
    enum Sliders
    {
        HPbar,
    }
    
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        Bind<Slider>(typeof(Sliders));
        BossHpChanged(Managers.Game._monsters[0]._mStat.HP);
        PubAndSub.Subscrib<int>("BossHP", BossHpChanged);
    }
    private void OnEnable()
    {
        Init(transform);
    }
    private void BossHpChanged(int value)
    {
        Logger.LogError($"{(float)value / (float)Managers.Game._monsters[0]._mStat.MaxHP}값 확인");
        Get<Slider>((int)Sliders.HPbar).value = (float)value / (float)Managers.Game._monsters[0]._mStat.MaxHP;
        
    }
}
