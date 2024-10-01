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
    public Transform UICanvasTrs;//UI화면을 랜더링할 캐너ㅡ 컴포넌트 트랜스폼
    //UI 화면을 이 UI 캔버스 트랜스폼 하위에 위치시켜주어야 하기 때문에 필요함

    public Transform CloseUITrs;//UI 화면을 닫을 때 비활성화 시킨 UI 화면들을 위치시켜줄 트랜스폼



    private BaseUI m_FrontUI; //UI화면에 열려있는 가장 상단에 열려있는 UI
    //활성화된 UI
    private Dictionary<Type, GameObject> m_OpenUIPool = new Dictionary<Type, GameObject>();
    //비활성화된 UI
    private Dictionary<Type, GameObject> m_CloseUIPool = new Dictionary<Type, GameObject>();
    //UI 화면이 열려있는지 닫혀있는지 구분이 필요하기 때문에 UI 풀을 2개의 변수로 관리

    LinkedList<BaseUI> baseUIs = new LinkedList<BaseUI>();
    public void Init()
    {
        if (UICanvasTrs == null)
        {
            // 풀링을 할 오브젝트가 있다면 @Pool_Root 산하에 들고 있게 할 예정
            UICanvasTrs = new GameObject { name = "@UI_Root" }.transform;
            UnityEngine.Object.DontDestroyOnLoad(UICanvasTrs);
        }
        if (CloseUITrs == null)
        {
            // 풀링을 할 오브젝트가 있다면 @Pool_Root 산하에 들고 있게 할 예정
            CloseUITrs = new GameObject { name = "@CloseUI_Root" }.transform;
            CloseUITrs.SetParent(UICanvasTrs);
        }
    }

    //열ㄱ를 원하는 UI화면의 인스턴스를 가져오는함수
    //out 함수에서는 한가지 값이나 참조만 반환할 수 있기 때문에
    //여러가지 값이나 참조를 반환하고 싶을 때 이렇게 out매개변수를 사용
    //이 함수는 BaseUI,IsAlreadyOpen 두가지 값을 반환 할 수 있다.
    private BaseUI GetUI<T>(out bool isAlreadyOpen, bool isNew = false)
    {
        System.Type uiType = typeof(T);

        BaseUI ui = null;
        isAlreadyOpen = false;

        if (!isNew&&m_OpenUIPool.ContainsKey(uiType) )//활성화된 UI면
        {
            ui = m_OpenUIPool[uiType].GetComponent<BaseUI>();
            isAlreadyOpen = true;
        }
        else if (!isNew && m_CloseUIPool.ContainsKey(uiType))//비활성화된 UI면
        {

            ui = m_CloseUIPool[uiType].GetComponent<BaseUI>();
            m_CloseUIPool.Remove(uiType);
        }
        else
        { //한번도 생성된 적이 없는 UI 면
            GameObject uiObj = Managers.Resource.Instantiate($"UI/{uiType}");
            //프리팹의 이름이 클래스명이랑 같아야함
            //클래스명을 기준으로 참조하기 때문
            ui = uiObj.GetComponent<BaseUI>();
        }
        return ui;
    }

    public T OpenUI<T>(BaseUIData uidata,bool sort=true,bool isNew =false) where T : BaseUI
    {
        System.Type uiType = typeof(T);

        Logger.Log($"{GetType()}::OpneUI({uiType})");

        bool isAlreadyOpen = false;


        var ui = GetUI<T>(out isAlreadyOpen, isNew);
        if (!ui)
        {
            Logger.Log($"{uiType} dose not exist");
            return null;
        }

        if (isAlreadyOpen)//이미 열려있으면 비정상적인 요청이라고 로그
        {
            Logger.Log($"{uiType} is Already Open ");
            return null;
        }


        var siblingIdx = UICanvasTrs.childCount - 1;//하위 오브젝트 개수
        ui.Init(UICanvasTrs);//화면 초기화   

        //ui.transform.SetSiblingIndex(siblingIdx);
        //하이어라키 창 우선순위변경

        ui.gameObject.SetActive(true);
        ui.SetInfo(uidata);
        
        ui.ShowUI();
        m_OpenUIPool[uiType] = ui.gameObject;
        SetCurrentUI(ui, sort); //소팅 우선순위 변경
        return ui as T;
    }

    public void CloseUI(BaseUI ui)
    {
        System.Type uiType = ui.GetType();
        Logger.Log($"{GetType()}::CloseUI({uiType})");

        ui.gameObject.SetActive(false);
        
        m_OpenUIPool.Remove(uiType);
        m_CloseUIPool[uiType] = ui.gameObject;
        
        baseUIs.Remove(ui);
        m_FrontUI = null;
        _order--;
        if (baseUIs.Count>0)
        {
            var lastChild = baseUIs.Last.Value;
            m_FrontUI = lastChild;
        }
        ui.transform.SetParent(CloseUITrs);
    }

    public void DeleteUI(BaseUI ui) {
        ui.CloseUI();
        UnityEngine.Object.Destroy(ui.gameObject);
    }
    //특정 UI화면이 열려있는지 확인하고 그 열려있는 UI화면을 가져오는 함수
    public BaseUI GetActiveUI<T>() //이름 신경쓰자 이후에 이름이 달라 에러가 발생했었다. -GetActivePopupUI이런 이름이였음
    {
        var uiType = typeof(T);
        //m_OpenUIPool에 특정 화면 인스턴스가 존재한다면 그 화면 인스턴스를 리턴해 주고 그렇지 않으면 널 리턴
        return m_OpenUIPool.ContainsKey(uiType) ? m_OpenUIPool[uiType].GetComponent<BaseUI>() : null;

    }

    //UI화면이 열린것이 하나라도 있는지 확인하는 함수
    public bool ExistsOpenUI()
    {
        return m_FrontUI != null; //m_FrontUI가 null인지 아닌지 확인해서 bool값을 반환
    }

    //현재 가장 최상단에 있는 인스턴스를 리턴하는 함수

    public BaseUI GetCurrentFrontUI()
    {
        return m_FrontUI;
    }

    //가장 최상단에 있는 UI화면 인스턴스를 닫는 함수
    public void CloseCurrFrontUI(bool delete=false)
    {
        if (delete)
        {
            DeleteUI(m_FrontUI);
        }
        else {
            m_FrontUI.CloseUI();
        }
    }

    //열려있는 모든 UI화면을 닫으라는 함수

    public void CloseAllOpenUI()
    {
        while (m_FrontUI)
        {
            m_FrontUI.CloseUI(true);
        }
    }
    public void SetCurrentUI(BaseUI ui, bool sort = true)
    {
        // 캔버스 추출
        Canvas canvas = Util.GetOrAddComponent<Canvas>(ui.gameObject);
        // 랜더모드는 무조건 ScreenSpaceOverlay ( 이 경우만 sorting되기 때문 )
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        // 캔버스 안에 캔버스가 중첩해서 있을 때 그 부모가 어떤 값을 가지던 자신은 무조건 내 sorting order를 가진다
        // overrideSorting을 통해 혹시라도 중첩 캔버스라 자식 캔버스가 있더라도 부모 캔버스가 어떤 값을 가지던
        // 자신은 자신의 오더값을 가지려 할 때 true;
        canvas.overrideSorting = true;
        if (baseUIs.Contains(ui)) {
            baseUIs.Remove(ui);
        }
        baseUIs.AddLast(ui);
        m_FrontUI = ui;
        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
            foreach (BaseUI baseUI in baseUIs) {
                baseUI.OnIsNotCurrent();
            }
            ui.OnIsCurrent();


        }
        else    // 팝업이랑 상관없는 일반 UI
        {
            canvas.sortingOrder = 0;
        }
    }
    public void Clear()
    {
        
    }
    /*
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
    */
}
