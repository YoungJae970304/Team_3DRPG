using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InDungeonUI : BaseUI
{
    public TextMeshProUGUI _loadText;
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
