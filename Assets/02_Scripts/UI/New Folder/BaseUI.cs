using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BaseUIData 
{

    public Action OnShow;
    public Action OnClose;
}

public class BaseUI : MonoBehaviour {
    public Animation M_UIOpenAnimation;


    private Action m_OnShow;
    private Action m_OnClose;


    public virtual void Init(Transform anchor) {
        Logger.Log($"{GetType()} init");

        m_OnShow = null;
        m_OnClose = null;

        transform.SetParent(anchor);

        var rectTransform = GetComponent<RectTransform>();

        if (!rectTransform) {
            Logger.LogError("UI does not Have RectTransform");
            return;
        }

        rectTransform.localPosition = Vector3.zero;
        rectTransform.localScale = Vector3.one;
        rectTransform.offsetMax = Vector2.zero;
        rectTransform.offsetMin = Vector2.zero;
    }

    public virtual void SetInfo(BaseUIData uiData) {
        Logger.Log($"{GetType()} Set info");

        m_OnShow = uiData.OnShow;
        m_OnClose = uiData.OnClose;
    }

    public virtual void ShowUI() {
        if (M_UIOpenAnimation) {
            M_UIOpenAnimation.Play();
        }

        m_OnShow?.Invoke();

        m_OnShow = null;
    }

    public virtual void CloseUI(bool isCloseAll = false) {
        //isCloseAll : 씬을 전환하거나 할 때 열려있는화면을
        //전부 다 닫아줄 필요가 있을 때
        //true를 넘겨줘ㅓ 화면을 닫을 때 필요한 처리들을
        //다 무시하고 화면만 닫아주기 위해서 사용할것

        if (!isCloseAll) {
            m_OnClose?.Invoke();
        
        }
        m_OnClose = null;

        //UIManager.Instance.CloseUI(this);
    }

    public void OnClosedButton() {
        CloseUI();
    }
}
