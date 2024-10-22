using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillQuickSlot : MonoBehaviour,IItemDragAndDropAble
{
    [SerializeField] Image _image;
    public SkillBase _skill;
    public SkillBase Skill { get => _skill; set {
            _skill = value;
            UpdateInfo();
        } }
    private void Awake()
    {
        UpdateInfo();
    }
    public void UpdateInfo()
    {
        //_image = _skill.icon;
        if (Skill != null)
        {
            _image.enabled = true;
        }
        else
        {
            _image.enabled = false;
        }
        Logger.LogWarning("슬롯갱신");

    }
    public bool DragEnter(Image icon)
    {
        if (_skill == null) { return false; }
        icon.enabled = true;                        //마우스 따라다닐 이미지
        icon.sprite = _image.sprite;   //이미지 변경
        _image.enabled = false;
        return true;
    }

    public void DragExit(Image icon)
    {
        icon.enabled = false;
        _image.enabled = true;
    }

    public void ItemInsert(IItemDragAndDropAble moveSlot)
    {
        
        if (moveSlot is SkillTreeItem)
        {
            SkillTreeItem skillTreeItem = moveSlot as SkillTreeItem;
            Skill = skillTreeItem.Skill;
            
            
            Logger.LogWarning("23");
        }
        else if (moveSlot is SkillQuickSlot) {
            SkillQuickSlot skillQuickSlot = moveSlot as SkillQuickSlot;
            SkillBase skill = _skill;
            Skill = skillQuickSlot._skill;
            skillQuickSlot.Skill = skill;
            Logger.LogWarning("14");

        }
        
    }

    public void NullTarget()
    {
        Skill = null;
    }

   
}
