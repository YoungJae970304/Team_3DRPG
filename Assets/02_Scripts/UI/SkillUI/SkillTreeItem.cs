using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEditor;

public class SkillTreeItem : MonoBehaviour , IItemDragAndDropAble
{
    ScrollRect _parent;
    [Serializable]
    class SkillCondition {
        public SkillTreeItem _Item;
        public int _conditionLevel;
    }

    [SerializeField] List<SkillCondition> _conditions = new List<SkillCondition>();
    SkillBase _skill ;
    [SerializeField]public MonoScript _skillScript;
    public SkillBase Skill { get => _skill; }
    [SerializeField] int _skillLevel = 0;
    [SerializeField] bool isActive = false;
    [SerializeField] Image _image;
    internal bool SetSkill(MonoScript skillScript)
    {
        _skillScript = skillScript;
        if (_skill != null&& _skillScript.GetClass() == _skill.GetType()) { return false; }
        var typecClass = Activator.CreateInstance(_skillScript.GetClass());
        if (typecClass is SkillBase)
        { 
            
            _skill = typecClass as SkillBase;
            if (!gameObject.activeSelf) { gameObject.SetActive(true); }
            return true;
        }
        else {
            _skillScript = null;
            Logger.LogError("Type ERROR");
            gameObject.SetActive(false);
            return false;
        }

    }


    
    public Image Icon { get => _image; }
    private void Awake()
    {
        Init();
        Logger.LogError("123213");
    }
    public virtual void Init() {
        SetSkill(_skillScript);
        gameObject.SetActive(_skill != null);
        _parent = transform.GetComponentInParent<ScrollRect>();
        UpdateInfo();
        
    }
    protected virtual void UpdateInfo() {
        
        isActive = CheckCondition();
        if (_skill == null) { return; }
        _skill._level = _skillLevel;
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

    public void ItemInsert(IItemDragAndDropAble moveSlot)
    {
        return;
    }

    public void NullTarget()
    {
        return;
    }

    public bool DragEnter(Image icon)
    {
        if (!isActive) { return false; }
        _parent.enabled = false;
        icon.enabled = true;                        //마우스 따라다닐 이미지
        icon.sprite = _image.sprite;   //이미지 변경
        return true;
    }

    public void DragExit(Image icon)
    {
        _parent.enabled = true;
        icon.enabled = false;
    }
}
