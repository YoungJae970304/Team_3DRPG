using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEditor;

public class SkillTreeItem : MonoBehaviour , IItemDragAndDropAble
{
    ScrollRect _parent; //드래그시 스크롤 렉트 비활성화를 위한 부모
    SkillTree _skillTree;//이 스킬이 담길 스킬트리
    [Serializable]
    class SkillCondition {//선행조건을 위한 클래스 선행스킬,조건레벨로 구성
        public SkillTreeItem _Item;
        public int _conditionLevel;
    }

    [SerializeField] List<SkillCondition> _conditions = new List<SkillCondition>();//선행조건을 모아둔 리스트
    SkillBase _skill ;//담을 스킬
    [SerializeField]public MonoScript _skillScript;//스크립트를 넣으면 그 타입의 스킬로 설정
    public SkillBase Skill { get => _skill; }//프로퍼티
    [SerializeField] int _skillLevel = 0;   //스킬의 레벨
    [SerializeField] public int _maxLevel = 5;//최대레벨
    [SerializeField] int _skillId;
    public int SkillLevel { get => _skillLevel;set {//최대 레벨 제한이걸린 스킬 레펠 프로퍼티
            if (Skill == null|| value> _maxLevel) { return; }
            _skillLevel = value;
            //Skill._level = value;   // 이곳에서 해당 스킬의 현재 레벨도 적용을 시켜주네
            Skill._level = _skillLevel;
            UpdateInfo();
        } }
    [SerializeField] bool isActive = false;         //슬롯 활성화 및 비활성화 표시
    [SerializeField] Image _image;                  //스킬 이미지
    public Image Icon { get => _image; }            //외부 접근용 프로퍼티
    //스크립트 타입으로 새로운 클래스 생성해서 스킬에 할당하는 함수
    internal bool SetSkill(MonoScript skillScript)
    {
        //_skillScript = skillScript;
        //if (_skill != null&& _skillScript.GetClass() == _skill.GetType()) { return false; }//이전과 같으면 무시
        //var typecClass = Activator.CreateInstance(_skillScript.GetClass());
        //if (typecClass is SkillBase)
        //{ 
        //    _skill = typecClass as SkillBase;
        //    if (!gameObject.activeSelf) { gameObject.SetActive(true); }
        //    _maxLevel = _skill._maxLevel;
        //    return true;
        //}
        //else {
        //    _skillScript = null;
        //    Logger.LogError("Type ERROR");
        //    gameObject.SetActive(false);
        //    return false;
        //}

        _skillScript = skillScript;
        if (_skill != null && _skillScript.GetClass() == _skill.GetType()) { return false; }

        SkillData skillData = Managers.DataTable.GetSkillData(_skillId);
        if (skillData != null)
        {
            if (skillData.SkillType == SkillData.SkillTypes.Passive)
            {
                _skill = new PassiveSkill(_skillId);
            }
            else
            {
                Type skillType = _skillScript.GetClass();
                if (typeof(SkillBase).IsAssignableFrom(skillType))
                {
                    _skill = Activator.CreateInstance(skillType, _skillId) as SkillBase;
                }
            }

            if (_skill != null)
            {
                if (!gameObject.activeSelf) { gameObject.SetActive(true); }
                _maxLevel = skillData.MaxLevel;
                return true;
            }
        }

        _skillScript = null;
        Logger.LogError("Skill creation failed");
        gameObject.SetActive(false);
        return false;

    }


    
    
    //초기화 함수
    public virtual void Init(SkillTree skillTree ) {
        _skillTree = skillTree;
        SetSkill(_skillScript);
        gameObject.SetActive(_skill != null);
        _parent = transform.GetComponentInParent<ScrollRect>();
        UpdateInfo();
        
    }
    //정보 갱신용 함수 
    protected virtual void UpdateInfo() {
        
        isActive = CheckCondition();
        if (_skill == null) { return; }
        _skill._level = _skillLevel;
    }
    //선행조건을 확인하고 달성시 스킬 활성화
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
    //드래그 앤 드롭을 위한 인터페이스 구현부
    //드롭을 비활성화 하기위해 ItemInsert은 구현하지 않음
    public void ItemInsert(IItemDragAndDropAble moveSlot)
    {
        return;
    }

    public void NullTarget()
    {
        return;
    }
    //드래그 시작시 및 클릭시
    public bool DragEnter(Image icon)
    {
        _skillTree.CurrentItem = this;
        if ( !isActive || _skillTree.CurrentItem.Skill._skillType == SkillData.SkillTypes.Passive || SkillLevel == 0 ) { return false; }

        _parent.enabled = false;
        icon.enabled = true;                        //마우스 따라다닐 이미지
        icon.sprite = _image.sprite;   //이미지 변경
        return true;
    }
    //드래그 종료시
    public void DragExit(Image icon)
    {
        _parent.enabled = true;
        icon.enabled = false;
    }
}
