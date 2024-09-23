using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_Button : UI_Popup
{
    enum Buttons
    {
        PointButton
    }

    enum Texts
    {
        PointText,
        ScoreText,
    }

    enum GameObjects
    {
        TestObject,
    }

    enum Images
    {
        Image
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        // �θ��� Init �� ����
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        //test
        //GetText((int)Texts.ScoreText).text = "�׽�Ʈ";

        GetButton((int)Buttons.PointButton).gameObject.BindUIEvent(OnButtonClicked);

        GameObject go = GetImage((int)Images.Image).gameObject;
        // ���� ���
        BindUIEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);
    }

    int _score = 0;

    public void OnButtonClicked(PointerEventData data)
    {
        _score++;
        GetText((int)Texts.ScoreText).text = $"���� : {_score}��";
    }
}
