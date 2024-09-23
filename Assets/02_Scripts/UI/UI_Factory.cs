using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Factory : MonoBehaviour
{

    // �� UI�� �������ִ� �Լ� ( �Ű������� Ÿ���� �̸� �������� �ʰ�, ��� �� ���� )
    public static T ShowSceneUI<T>(string name = null) where T : UI_Scene
        // T�� �ƹ� T�� �޴°� �ƴ϶� ������ UI �˾��� ��ӹ޴� �ַ� ������
    {
        return Managers.UI.ShowSceneUI<T>(name);
    }

    // �˾� UI�� �������ִ� �Լ� ( �Ű������� Ÿ���� �̸� �������� �ʰ�, ��� �� ���� )
    public static T ShowPopupUI<T>(string name = null) where T : UI_Popup
        // T�� �ƹ� T�� �޴°� �ƴ϶� ������ UI �˾��� ��ӹ޴� �ַ� ������
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
