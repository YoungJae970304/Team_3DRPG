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
    public string DescTxt;
    public Action confimAction;
}

public class ConfirmUI : BaseUI
{
    enum ConfirmTexts
    {
        DescTxt,
    }

    enum ConfirmButtons
    {
        OKBtn,
        CancelBtn,
    }

    Animator _fadeAnim;
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        Bind<TextMeshProUGUI>(typeof(ConfirmTexts));
        Bind<Button>(typeof(ConfirmButtons));
    }
    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);
        ConfirmUIData confirmData = uiData as ConfirmUIData;
        GetText((int)ConfirmTexts.DescTxt).text = confirmData.DescTxt;
        GetButton((int)ConfirmButtons.OKBtn).onClick.AddListener(() => OnClickOKBtn(confirmData.confimAction));
    }
    public void OnClickOKBtn(Action confirm)
    {
        confirm?.Invoke();
    }
}
