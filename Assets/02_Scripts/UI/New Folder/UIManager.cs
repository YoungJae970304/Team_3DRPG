using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager2 
{
    public Transform UICanvasTrs;//UI화면을 랜더링할 캐너ㅡ 컴포넌트 트랜스폼
    //UI 화면을 이 UI 캔버스 트랜스폼 하위에 위치시켜주어야 하기 때문에 필요함

    public Transform CloseUITrs;//UI 화면을 닫을 때 비활성화 시킨 UI 화면들을 위치시켜줄 트랜스폼



    private BaseUI m_FrontUI; //UI화면에 열려있는 가장 상단에 열려있는 UI
    //활성화된 UI
    private Dictionary<System.Type, GameObject> m_OpenUIPool = new Dictionary<System.Type, GameObject>();
    //비활성화된 UI
    private Dictionary<System.Type, GameObject> m_CloseUIPool = new Dictionary<System.Type, GameObject>();
    //UI 화면이 열려있는지 닫혀있는지 구분이 필요하기 때문에 UI 풀을 2개의 변수로 관리

    public void Init()
    {

    }

    //열ㄱ를 원하는 UI화면의 인스턴스를 가져오는함수
    //out 함수에서는 한가지 값이나 참조만 반환할 수 있기 때문에
    //여러가지 값이나 참조를 반환하고 싶을 때 이렇게 out매개변수를 사용
    //이 함수는 BaseUI,IsAlreadyOpen 두가지 값을 반환 할 수 있다.
    private BaseUI GetUI<T>(out bool isAlreadyOpen) {
        System.Type uiType = typeof(T);

        BaseUI ui = null;
        isAlreadyOpen = false;

        if (m_OpenUIPool.ContainsKey(uiType))//활성화된 UI면
        {
            ui = m_OpenUIPool[uiType].GetComponent<BaseUI>();
            isAlreadyOpen = true;
        }
        else if (m_CloseUIPool.ContainsKey(uiType))//비활성화된 UI면
        {

            ui = m_CloseUIPool[uiType].GetComponent<BaseUI>();
            m_CloseUIPool.Remove(uiType);
        }
        else { //한번도 생성된 적이 없는 UI 면
            var uiObj = Managers.Resource.Instantiate($"UI/{uiType}");
            //프리팹의 이름이 클래스명이랑 같아야함
            //클래스명을 기준으로 참조하기 때문
            ui = uiObj.GetComponent<BaseUI>();
        }
        return ui;
    }

    public void OpenUI<T>(BaseUIData uidata) {
        System.Type uiType = typeof(T);

        Logger.Log($"{GetType()}::OpneUI({uiType})");

        bool isAlreadyOpen = false;


        var ui = GetUI<T>(out isAlreadyOpen);
        if (!ui) {
            Logger.Log($"{uiType} dose not exist");
            return;
        }

        if (isAlreadyOpen)//이미 열려있으면 비정상적인 요청이라고 로그
        {
            Logger.Log($"{uiType} is Already Open ");
            return;
        }

        var siblingIdx = UICanvasTrs.childCount-1;//하위 오브젝트 개수
        ui.Init(UICanvasTrs);//화면 초기화

        ui.transform.SetSiblingIndex(siblingIdx);
        //하이어라키 창 우선순위변경

        ui.gameObject.SetActive(true);
        ui.SetInfo(uidata);
        ui.ShowUI();

        m_FrontUI = ui;
        m_OpenUIPool[uiType] = ui.gameObject;
    }

    public void CloseUI(BaseUI ui) {
        System.Type uiType = ui.GetType();
        Logger.Log($"{GetType()}::CloseUI({uiType})");

        ui.gameObject.SetActive(false);

        m_OpenUIPool.Remove(uiType);
        m_CloseUIPool[uiType] = ui.gameObject;
        ui.transform.SetParent(CloseUITrs);

        m_FrontUI = null;

        var lastChild = UICanvasTrs.GetChild(UICanvasTrs.childCount - 1);

        if (lastChild)
        {
            m_FrontUI = lastChild.gameObject.GetComponent<BaseUI>();
        }
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
    public void CloseCurrFrontUI()
    {
        m_FrontUI.CloseUI();
    }

    //열려있는 모든 UI화면을 닫으라는 함수

    public void CloseAllOpenUI()
    {
        while (m_FrontUI)
        {
            m_FrontUI.CloseUI(true);
        }
    }

}
