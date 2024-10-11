using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Cursor : BaseUI
{
    public enum CursorImages
    {
        None,       // 전부 비활성화 하는 용도 ( 디폴트 )
        AimCursor,  // 줌 모드에서 사용되는 커서
        ClickCursor // Esc키 등을 눌렀을때보이는 클릭용 커서
    }

    private void Awake()
    {
        Bind<Image>(typeof(CursorImages));
        InitCursorImages();
    }

    private void Start()
    {
        SetCursorImage(CursorImages.None);
    }

    // 시작 시 Cursor 프리팹 내부 이미지를 비활성화
    private void InitCursorImages()
    {
        foreach (CursorImages cursorType in Enum.GetValues(typeof(CursorImages)))
        {
            if (cursorType != CursorImages.None)
                GetImage((int)cursorType).gameObject.SetActive(false);
        }
    }

    // 매개변수로 들어간 커서 활성화
    public void SetCursorImage(CursorImages cursor)
    {
        foreach (CursorImages cursorType in Enum.GetValues(typeof(CursorImages)))
        {
            if (cursorType != CursorImages.None)
            {
                GetImage((int)cursorType).gameObject.SetActive(cursorType == cursor);
            }
        }
    }
}
