using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SkillTreeData : BaseUIData {
    public string path;
    public SkillTreeData(Define.PlayerType playerType)
    {
        path = playerType == Define.PlayerType.Melee ? "MeleeSkillTree" : "MageSkillTree";
    }
}


public class SkillTree : ItemDragUI
{
    ScrollRect _scrollRect;         //스킬들이 보여질 스크롤 렉트
    SkillTreeItem _currentItem;     //현재 선택된 스킬
    public SkillTreeItem CurrentItem {get=>_currentItem;set {//정보갱신이 포함된 프로퍼티
            _currentItem = value;
            UpdateInfo();
        } }
    List<SkillTreeItem> _skillTreeItems = new List<SkillTreeItem>();//스킬들을 모아둔 리스트
    #region bind
    enum ScrollView {
        Skills
    }
    enum Buttons {
        PlusBtn,
        MinusBtn
    }

    enum Texts {
        SkillTxt,
        LevelTxt,
        SpTxt,
    }
    #endregion
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        Bind<ScrollRect>(typeof(ScrollView));
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        _scrollRect = Get<ScrollRect>((int)ScrollView.Skills);
        GetButton((int)Buttons.PlusBtn).onClick.AddListener(OnSkillLevelPlusBtn);
        GetButton((int)Buttons.MinusBtn).onClick.AddListener(OnSkillLevelMinusBtn);
    }
    //데이터에서 스킬트리 프리팹의 경로를 받아 초기화
    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);
        if (uiData is SkillTreeData) {
            
            var tree = Managers.Resource.Instantiate("UI/"+(uiData as SkillTreeData).path, _scrollRect.content).GetComponent<RectTransform>(); 

            _scrollRect.content.sizeDelta = tree.sizeDelta;
            tree.transform.localPosition = Vector3.zero;
            _skillTreeItems = new List<SkillTreeItem>( _scrollRect.content.transform.GetComponentsInChildren<SkillTreeItem>());
            foreach (var item in _skillTreeItems) {
                item.Init(this);
            }
            UpdateInfo();
        }
    }
    //스킬 정보 표시 갱신
    private void UpdateInfo()
    {
        if (_skillTreeItems == null) return;

        GetText((int)Texts.SkillTxt).text = _currentItem == null ? "": _currentItem.Skill.GetType().ToString();//우선 정보 대신 이름 표시 -> 추후 스킬설명으로 변경
        if (_currentItem == null)
        {
            GetText((int)Texts.LevelTxt).gameObject.SetActive(false);
        }
        else {
            GetText((int)Texts.LevelTxt).text = _currentItem.SkillLevel.ToString();
            GetText((int)Texts.LevelTxt).gameObject.SetActive(true);
            GetButton((int)Buttons.MinusBtn).interactable = _currentItem.SkillLevel > 0;
            GetButton((int)Buttons.PlusBtn).interactable = _currentItem.SkillLevel < _currentItem._maxLevel;
        }
        
        GetText((int)Texts.SpTxt).text = $"sp소모:{5}";

    }
    public override void CloseUI(bool isCloseAll = false)
    {
        base.CloseUI(isCloseAll);
        GetButton((int)Buttons.PlusBtn).onClick.RemoveAllListeners();
        GetButton((int)Buttons.MinusBtn).onClick.RemoveAllListeners();
    }
    //스킬레벨 증가
    public void OnSkillLevelPlusBtn() {
        if (_currentItem == null) { return; }
        if (SpCheck())
        {
            Logger.LogError($"플러스 진입 확인 : {_currentItem.Skill._skillName}");
            //sp 수치 감소 처리 필요
            _currentItem.Skill._prevLevel = _currentItem.SkillLevel;
            Managers.Game._player._playerStatManager.SP -= 1;
            _currentItem.SkillLevel += 1;
            _currentItem.Skill.PassiveEffect(Managers.Game._player._playerStatManager);
            UpdateInfo();
        }
        
    }
    //스킬레벨 감소
    public void OnSkillLevelMinusBtn()
    {
        if (_currentItem == null) { return; }
        if (_currentItem.SkillLevel > 0) {
            Logger.LogError($"마이너스 진입 확인:{_currentItem.Skill._skillName}");
            _currentItem.Skill._prevLevel = _currentItem.SkillLevel;
            Managers.Game._player._playerStatManager.SP += 1;
            _currentItem.SkillLevel -= 1;
            _currentItem.Skill.PassiveEffect(Managers.Game._player._playerStatManager);
            //sp 수치 증가 처리 필요
            UpdateInfo();
        }
        
    }
    //스킬 레벨 증가시 sp 조건 확인 
    private bool SpCheck() {
        if (Managers.Game._player._playerStatManager.SP > 0)
        {
            return true;
        }

        return false;
    }
}
