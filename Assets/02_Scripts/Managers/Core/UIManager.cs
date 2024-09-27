using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager
{
    int _order = 10;    // 혹시 모르니깐 여유를 두고 먼저 생성할게 있다면 10보다 작은 수로 팝업할 수 있게함
    GameObject _root = null;
    public GameObject Root
    {
        get
        {
            if (_root == null)
            {
                _root = GameObject.Find("@UI_Root");
                if (_root == null)
                {
                    _root = new GameObject { name = "@UI_Root" };
                }
                _root = new GameObject { name = "@UI_Root" };
            }
            return _root;
        }
    }

    // 가장 마지막에 띄운 팝업이 가장 먼저 사라져야하기 때문에 Stack사용
    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    //Stack<UI_Scene> _scnenStack = new Stack<UI_Scene>();

    UI_Scene _sceneUI = null;

    // 씬 UI를 생성해주는 함수 ( 매개변수의 타입을 미리 결정하지 않고, 사용 시 결정 )
    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
        // T는 아무 T나 받는게 아니라 무조건 UI 팝업을 상속받는 애로 만들자
    {
        if (string.IsNullOrEmpty(name)) // 이름을 안받았으면
            name = typeof(T).Name;      // T의 이름으로

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");  // 팝업 생성
        T sceneUI = Util.GetOrAddComponent<T>(go);    // 컴퍼넌트가 붙어있지 않다면 추가
        _sceneUI = sceneUI;

        go.transform.SetParent(Root.transform);

        return sceneUI;
    }

    // 팝업 UI를 생성해주는 함수 ( 매개변수의 타입을 미리 결정하지 않고, 사용 시 결정 )
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
        // T는 아무 T나 받는게 아니라 무조건 UI 팝업을 상속받는 애로 만들자
    {
        if (string.IsNullOrEmpty(name)) // 이름을 안받았으면
            name = typeof(T).Name;      // T의 이름으로

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");  // 팝업 생성
        T popup = Util.GetOrAddComponent<T>(go);    // 컴퍼넌트가 붙어있지 않다면 추가
        _popupStack.Push(popup);

        go.transform.SetParent(Root.transform);

        return popup;
    }

    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty (name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/SubItem/{name}");  // 팝업 생성
        if (parent != null)
            go.transform.SetParent(parent);

        return Util.GetOrAddComponent<T>(go);
    }
    public T MakeWorldSpace<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/WorldSpace/{name}");  // 팝업 생성
        if (parent != null)
            go.transform.SetParent(parent);

        Canvas canvas = go.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        return Util.GetOrAddComponent<T>(go);
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;
        _order--;
    }

    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        // 스택 맨 위에 있는 것과 내가 지정한 popup이랑 같을때만 Close되도록 해서 안전하게 삭제 가능
        if (_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed!");
            return;
        }

        ClosePopupUI();
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        // 캔버스 추출
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        // 랜더모드는 무조건 ScreenSpaceOverlay ( 이 경우만 sorting되기 때문 )
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        // 캔버스 안에 캔버스가 중첩해서 있을 때 그 부모가 어떤 값을 가지던 자신은 무조건 내 sorting order를 가진다
        // overrideSorting을 통해 혹시라도 중첩 캔버스라 자식 캔버스가 있더라도 부모 캔버스가 어떤 값을 가지던
        // 자신은 자신의 오더값을 가지려 할 때 true;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else    // 팝업이랑 상관없는 일반 UI
        {
            canvas.sortingOrder = 0;
        }
    }

    public void Clear()
    {
        CloseAllPopupUI();
        _sceneUI = null;
    }
}
