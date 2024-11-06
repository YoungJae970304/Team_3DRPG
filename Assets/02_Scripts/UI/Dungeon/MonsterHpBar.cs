using UnityEngine;
using UnityEngine.UI;

public class MonsterHpBar : BaseUI
{
    enum Sliders
    {
        HpBar,
    }
    Monster _monster;
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        Bind<Slider>(typeof(Sliders));
        _monster = GetComponentInParent<Monster>();
        HpChanged();
    }
    private void OnEnable()
    {
        Init(transform);
    }
    private void HpChanged()
    {
        Get<Slider>((int)Sliders.HpBar).value = (float)_monster._mStat.HP / (float)_monster._mStat.MaxHP;
    }
}
