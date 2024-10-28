using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    public void OnSceneChange(string sceneName)
    {
        SelectPlayerUI selectPlayerUI = Managers.UI.GetActiveUI<SelectPlayerUI>() as SelectPlayerUI;

        if (selectPlayerUI != null)
        {
            Managers.Scene.SceneChange(sceneName);
        }

        Managers.UI.CloseAllOpenUI();
    }


    public override void Clear()
    {
        
    }
}
