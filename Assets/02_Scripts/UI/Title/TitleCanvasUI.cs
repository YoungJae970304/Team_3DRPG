using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleCanvasUI : BaseUI
{
    enum Buttons
    {
        BeginBtn,
        ContinueBtn,
    }

    private void Awake()
    {
        Bind<Button>(typeof(Buttons));
    }

    public void OnClickBeginBtn()
    {
        SelectPlayerUI selectPlayerUI = Managers.UI.GetActiveUI<SelectPlayerUI>() as SelectPlayerUI;
        if (selectPlayerUI == null)
        {
            Managers.UI.OpenUI<SelectPlayerUI>(new BaseUIData());
        }
    }

    public void OnClickContinueBtn(string sceneName)
    {
        Managers.Scene.SceneChange(sceneName);

        //CloseUI(true);
        Managers.UI.CloseAllOpenUI();
    }
}
