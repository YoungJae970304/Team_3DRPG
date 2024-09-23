using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Factory : MonoBehaviour
{

    // 씬 UI를 생성해주는 함수 ( 매개변수의 타입을 미리 결정하지 않고, 사용 시 결정 )
    public static T ShowSceneUI<T>(string name = null) where T : UI_Scene
        // T는 아무 T나 받는게 아니라 무조건 UI 팝업을 상속받는 애로 만들자
    {
        return Managers.UI.ShowSceneUI<T>(name);
    }

    // 팝업 UI를 생성해주는 함수 ( 매개변수의 타입을 미리 결정하지 않고, 사용 시 결정 )
    public static T ShowPopupUI<T>(string name = null) where T : UI_Popup
        // T는 아무 T나 받는게 아니라 무조건 UI 팝업을 상속받는 애로 만들자
    {
        return Managers.UI.ShowPopupUI<T>(name);
    }

    public static T MakeSubItem<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        return Managers.UI.MakeSubItem<T>(parent, name);
    }
    public static T MakeWorldSpace<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        return Managers.UI.MakeWorldSpace<T>(parent, name);
    }

    public static void ClosePopupUI()
    {
        Managers.UI.ClosePopupUI();
    }

    public static void CloseAllPopupUI()
    {
        Managers.UI.CloseAllPopupUI();
    }

    public static void ClosePopupUI(UI_Popup popup)
    {
        Managers.UI.ClosePopupUI(popup);
    }

    public static void SetCanvas(GameObject go, bool sort = true)
    {
        Managers.UI.SetCanvas(go, sort);
    }
}
