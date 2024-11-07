using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpBar : MonoBehaviour
{
    Monster _monster;
    Slider _hpBar;
    TextMeshProUGUI _hpText;
    private void Awake()
    {
        _monster = GetComponentInParent<Monster>();
        _hpBar = GetComponentInChildren<Slider>();
        _hpText = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        
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
        _hpText.text = $"{(((float)_monster._mStat.HP / (float)_monster._mStat.MaxHP) * 100).ToString("F1")}%";
    }
}
