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

    private void Awake()
    {
        Bind<TextMeshProUGUI>(typeof(ConfirmTexts));
        Bind<Button>(typeof(ConfirmButtons));

        GetText((int)ConfirmTexts.DescTxt).text = "게임 진입 후 캐릭터의 변경이 불가능 합니다!\r\n선택한 캐릭터로 진행 하시겠습니까?";
        GetButton((int)ConfirmButtons.OKBtn).onClick.AddListener(() => OnClickOKBtn("main"));
    }

    public void OnClickOKBtn(string sceneName)
    {
        // 현재 열려있는 UI에 따라 다른 처리가 필요?
        SelectPlayerUI selectPlayerUI = Managers.UI.GetActiveUI<SelectPlayerUI>() as SelectPlayerUI;

        if (selectPlayerUI != null)
        {
            Managers.Scene.SceneChange(sceneName);
        }

        //CloseUI(true);
        Managers.UI.CloseAllOpenUI();
    }
}
