using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeData : BaseUIData {
    public string path;

}


public class SkillTree : ItemDragUI
{
    ScrollRect _scrollRect;
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        _scrollRect = GetComponentInChildren<ScrollRect>();
    }
    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);
        if (uiData is SkillTreeData) {
            
            var tree = Managers.Resource.Instantiate("UI/"+(uiData as SkillTreeData).path, _scrollRect.content).GetComponent<RectTransform>(); 

            _scrollRect.content.sizeDelta = tree.sizeDelta;
            tree.transform.localPosition = Vector3.zero;
        }
        
    }
}
