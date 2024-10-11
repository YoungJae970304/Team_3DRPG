using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestHUDInfoUI : BaseUI
{
    /// <summary>
    ///퀘스트창에서 보여질 내용을 작성한다.
    /// 진행중인 퀘스트를 화면에서 보여줄 내용
    /// 퀘스트 창에서는 Reward를 보여줘야함
    /// RewardType은 화면에 보여지지 않는다.
    /// 퀘스트 시작 레벨 요구 사항은 굳이 보여줄 필요가 없다.
    /// 퀘스트 이름에 타입 까지 적어서 표시
    /// </summary>

    //퀘스트 제목
    public Text _displayName;
    //퀘스트 완료 목표
    public Text _requirementTxt;
    //퀘스트 완료 목표에 따른 수량이라면 수량체크
    public Text _requirementAmountTxt;

    int _requriement;

    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);
    }

    public override void ShowUI()
    {
        base.ShowUI();
        
    }

    public override void CloseUI(bool isCloseAll = false)
    {
        base.CloseUI(isCloseAll);
    }
}
