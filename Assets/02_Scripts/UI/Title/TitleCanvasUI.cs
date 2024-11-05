using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleCanvasUI : BaseUI
{
    public static bool _IsNewGame { get; set; }

    enum Buttons
    {
        BeginBtn,
        ContinueBtn,
        ShutDownBtn,
    }

    private void Awake()
    {
        Bind<Button>(typeof(Buttons));
    }

    public void OnClickBeginBtn()
    {
        _IsNewGame = true;
        Managers.Game._firstTuto = _IsNewGame;

        Managers.UI.CloseUI(this);
        SelectPlayerUI selectPlayerUI = Managers.UI.GetActiveUI<SelectPlayerUI>() as SelectPlayerUI;
        if (selectPlayerUI == null)
        {
            Managers.UI.OpenUI<SelectPlayerUI>(new BaseUIData());
        }
    }

    public void OnClickContinueBtn(string sceneName)
    {
        _IsNewGame = false;
        Managers.Game._firstTuto = _IsNewGame;

        //Managers.Scene.SceneChange(sceneName);
        Animator fadeAnim = GameObject.FindWithTag("SceneManager").GetComponent<Animator>();
        fadeAnim.SetTrigger("doFade");
        //CloseUI(true);
        Managers.UI.CloseAllOpenUI();
    }

    public void OnClickShutDownBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
