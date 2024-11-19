using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TitleCanvasUI : BaseUI
{
    enum Buttons
    {
        BeginBtn,
        ContinueBtn,
        ShutDownBtn,
    }
    public static bool _isNewGame {  get; private set; }
    string _SavePath;
    private void Awake()
    {
        Bind<Button>(typeof(Buttons));
    }

    public void OnClickBeginBtn()
    {
        Logger.Log($"현재 첫 시작인지 확인{_isNewGame.ToString()}");
        _isNewGame = true;
        Managers.Game._firstTuto = _isNewGame;

        Managers.UI.CloseUI(this);
        SelectPlayerUI selectPlayerUI = Managers.UI.GetActiveUI<SelectPlayerUI>() as SelectPlayerUI;
        if (selectPlayerUI == null)
        {
            Managers.UI.OpenUI<SelectPlayerUI>(new BaseUIData());
        }
    }

    public void OnClickContinueBtn(string sceneName)
    {
        _SavePath = $"{Application.persistentDataPath}/SavePlayerData.json";
        if(!File.Exists(_SavePath))
        {
            GetButton((int)Buttons.ContinueBtn).interactable = false;
            return;
        }
        else
        {
            GetButton((int)Buttons.ContinueBtn).interactable = true;
        }
        Logger.Log($"현재 이어하기 인지 확인{_isNewGame.ToString()}");
        _isNewGame = false;
        Managers.Game._firstTuto = _isNewGame;
        //Managers.Scene.SceneChange(sceneName);
        Animator fadeAnim = GameObject.FindWithTag("SceneManager").GetComponent<Animator>();
        fadeAnim.SetTrigger("doFade");
        //CloseUI(true);
        //Managers.UI.CloseAllOpenUI();
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
