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
        
    }
    private void OnEnable()
    {
        Logger.LogError("여기 들어감?");
        Bind<Slider>(typeof(Sliders));
        Logger.LogError("여기는 들어감?");
        BossHpChanged(Managers.Game._monsters[0]._mStat.HP);
        Logger.LogError("여기도 들어감?");
        PubAndSub.Subscrib<int>("BossHP", BossHpChanged);
        Logger.LogError("구독은 됨?");
    }
    private void BossHpChanged(int value)
    {
        Logger.LogError($"{(float)value / (float)Managers.Game._monsters[0]._mStat.MaxHP}값 확인");
        Get<Slider>((int)Sliders.HPbar).value = (float)value / (float)Managers.Game._monsters[0]._mStat.MaxHP;
        
    }
}
