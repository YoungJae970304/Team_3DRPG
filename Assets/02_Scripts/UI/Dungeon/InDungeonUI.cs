using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InDungeonUI : BaseUI
{
    enum Buttons
    {
        MainButton,
    }
    private void Awake()
    {
        Bind<Button>(typeof(Buttons));
    }
    public void OnClickMainButton(string sceneName)
    {
        Managers.Scene.SceneChange(sceneName);
        Managers.UI.CloseAllOpenUI();
    }
 
}
