using UnityEngine;
using UnityEngine.UI;

public class MonsterHpBar : MonoBehaviour
{
    Monster _monster;
    Slider _hpBar;
 
    private void OnEnable()
    {
        _monster = GetComponentInParent<Monster>();
        _hpBar = GetComponentInChildren<Slider>();
        if (_monster == null)
        {
            Logger.LogError("Monster component not found in parent.");
        }
        HpChanged();
    }
    public void HpChanged()
    {
        if (_monster._mStat == null)
        {
            Logger.LogError("Monster stats (_mStat) is null.");
        }
        if (_hpBar == null)
        {
            Logger.LogError("싯팔 이게 왜없어");
        }
        _hpBar.value = (float)_monster._mStat.HP / (float)_monster._mStat.MaxHP;
    }
}
