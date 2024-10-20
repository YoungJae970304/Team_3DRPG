using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkilllTreeItem : MonoBehaviour
{
    [Serializable]
    class SkillCondition {
        public SkilllTreeItem _Item;
        public int _conditionLevel;
    }

    [SerializeField] List<SkillCondition> _conditions = new List<SkillCondition>();
    SkillBase _skill ;
    [SerializeField] int _skillLevel=0;
    [SerializeField] bool isActive = false;
    private void Awake()
    {
        _skill = new NoneEffectSkill();
        _skill._level = 1;
        Init();
        Logger.LogError("123213");
    }
    public virtual void Init() {
        UpdateInfo();
    }
    protected virtual void UpdateInfo() {
        _skill._level = _skillLevel;
        isActive = CheckCondition();
    }

    protected virtual bool CheckCondition() {
        if (_conditions.Count == 0) {
            Logger.LogWarning("조건없음");
            return true; 
        }
        bool result = false;
        foreach (var condition in _conditions) {
            result = condition._Item._skillLevel>= condition._conditionLevel;//스킬레벨이 저장한것보다 크면
        }  
        return result;
    }
}
