using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialUIData : BaseUIData
{
    public List<Sprite> _tutoImages;    // 튜토리얼 이미지 리스트
    public bool _lastTuto;              // 마지막 튜토리얼인지 체크
}

// 튜토리얼 UI를 관리하는 클래스
public class TutorialUI : BaseUI
{
    // UI 버튼 열거형
    enum Buttons
    {
        PrevBtn,    // 이전 버튼
        NextBtn,    // 다음 버튼
    }

    // UI 이미지 열거형
    enum Images
    {
        TutoImage,  // 튜토리얼 이미지
    }

    // UI 텍스트 열거형
    enum Texts
    {
        QuitText,   // 종료 텍스트
    }

    private TutorialUIData _data;           // 튜토리얼 데이터
    private int _curImageIndex = 0;         // 현재 보여지는 이미지 인덱스

    // UI 초기화
    public override void Init(Transform anchor)
    {
        base.Init(anchor);

        // UI 요소들 바인딩
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));
    }

    // UI 정보 설정
    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);

        _data = uiData as TutorialUIData;
        _curImageIndex = 0;
        Managers.Game._cantInputKey = true;  // 튜토리얼 중 키 입력 방지

        // 튜토리얼 이미지가 있을 경우 초기 설정
        if (_data != null && _data._tutoImages.Count > 0)
        {
            UpdateImage();
            UpdateButtons();
        }

        // 버튼 이벤트 리스너 설정
        GetButton((int)Buttons.PrevBtn).onClick.RemoveAllListeners();
        GetButton((int)Buttons.NextBtn).onClick.RemoveAllListeners();
        GetButton((int)Buttons.PrevBtn).onClick.AddListener(OnPrevButtonClick);
        GetButton((int)Buttons.NextBtn).onClick.AddListener(OnNextButtonClick);

        GetText((int)Texts.QuitText).gameObject.SetActive(false);
    }

    // 현재 인덱스에 해당하는 이미지로 업데이트
    private void UpdateImage()
    {
        GetImage((int)Images.TutoImage).sprite = _data._tutoImages[_curImageIndex];
    }

    // 버튼 상태 업데이트
    private void UpdateButtons()
    {
        // 첫 페이지에서는 이전 버튼 비활성화
        GetButton((int)Buttons.PrevBtn).gameObject.SetActive(_curImageIndex > 0);
        // 마지막 페이지에서는 다음 버튼 비활성화
        GetButton((int)Buttons.NextBtn).gameObject.SetActive(_curImageIndex < _data._tutoImages.Count - 1);
        // 마지막 페이지에서만 종료 텍스트 활성화
        GetText((int)Texts.QuitText).gameObject.SetActive(_curImageIndex == _data._tutoImages.Count - 1);
    }

    // 이전 버튼 클릭 처리
    private void OnPrevButtonClick()
    {
        if (_curImageIndex > 0)
        {
            _curImageIndex--;
            UpdateImage();
            UpdateButtons();
        }
    }

    // 다음 버튼 클릭 처리
    private void OnNextButtonClick()
    {
        if (_curImageIndex < _data._tutoImages.Count - 1)
        {
            _curImageIndex++;
            UpdateImage();
            UpdateButtons();
        }
    }

    // UI가 비활성화될 때 호출
    private void OnDisable()
    {
        Managers.Game._cantInputKey = false;  // 키 입력 잠금 해제

        // 마지막 튜토리얼이었다면 첫 튜토리얼 완료 처리
        if (_data._lastTuto)
        {
            Managers.Game._firstTuto = false;
        }
    }
}