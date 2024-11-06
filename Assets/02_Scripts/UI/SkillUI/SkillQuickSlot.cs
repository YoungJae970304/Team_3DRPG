using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillQuickSlot : MonoBehaviour,IItemDragAndDropAble
{
    public Image _image;
    public SkillBase _skill;
    public SkillBase Skill { get => _skill; set {
            _skill = value;
            UpdateInfo();
        } }
    private void Awake()
    {
        PubAndSub.Subscrib<SkillBase>("QuickSlotRemove", RemoveQuickSlot);
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
            _image.sprite = skillTreeItem.Icon.sprite; // 아이콘 업데이트
            _image.enabled = true;  // 아이콘 활성화

            Logger.LogWarning(Skill.GetType().ToString());
        }
        else if (moveSlot is SkillQuickSlot) {
            SkillQuickSlot skillQuickSlot = moveSlot as SkillQuickSlot;
            SkillBase skill = _skill;
            Skill = skillQuickSlot._skill;

            _image.sprite = skillQuickSlot._image.sprite; // 아이콘 업데이트
            _image.enabled = true; // 아이콘 활성화

            skillQuickSlot.Skill = skill;
            Logger.LogWarning("14");
        }
    }

    public void NullTarget()
    {
        Skill = null;
    }

    private void RemoveQuickSlot(SkillBase skill)
    {
        if (this.Skill == skill)
        {
            NullTarget();
        }
    }

}
