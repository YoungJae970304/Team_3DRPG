using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
public enum ConfirmType { 
    OK,//확인용 팝업
    OK_CANCEL,//유저 선택지 제시
}
public class ConfirmUIData : BaseUIData {

    public ConfirmType ConfirmType;
    public string TitleTxt;
    public string DescTxt;
    public string OKBtnTxt;
    public Action OnclickOKButton;
    public string CancelTxt;
    public Action OnclickCancelButton;
}


public class ConfirmUI : BaseUI
{
    public TextMeshProUGUI TitleTxt = null;
    public TextMeshProUGUI DescTxt = null;
    public Button OKBtn;
    public Button CancelBtn;
    public TextMeshProUGUI OKBtnTxt = null;
    public TextMeshProUGUI CancelBtnTxt = null;

    ConfirmUIData m_ConfirmUIData = null;

    public Action m_OnclickOKButton = null;
    public Action m_OnclickCancelButton =null;

    public override void SetInfo(BaseUIData uiData)
    {//데이터 초기화
        base.SetInfo(uiData);
        m_ConfirmUIData = uiData as ConfirmUIData;
        TitleTxt.text = m_ConfirmUIData.TitleTxt;
        DescTxt.text = m_ConfirmUIData.DescTxt;
        m_OnclickOKButton = m_ConfirmUIData.OnclickOKButton;
        m_OnclickCancelButton = m_ConfirmUIData.OnclickCancelButton;
        OKBtnTxt.text = m_ConfirmUIData.OKBtnTxt;
        CancelBtnTxt.text = m_ConfirmUIData.CancelTxt;
        // ok버튼과 cancel 버튼 활성화
        // ConfirmType이 OK면 OK버튼만, OK_CANCEL이면 버튼 둘다 활성화
        OKBtn.gameObject.SetActive(true);
        CancelBtn.gameObject.SetActive(m_ConfirmUIData.ConfirmType == ConfirmType.OK_CANCEL);
    }

    public void OnClickOkBtn() {
        m_OnclickOKButton?.Invoke();
        CloseUI();
    }
    public void OnClickCancelcBtn()
    {
        m_OnclickCancelButton?.Invoke();
        CloseUI();
    }
}
