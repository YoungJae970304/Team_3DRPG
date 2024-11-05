using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUIData : BaseUIData
{
    public List<Sprite> _tutoImages;
    public bool _lastTuto;
}

public class TutorialUI : BaseUI
{
    enum Buttons
    {
        PrevBtn,
        NextBtn,
    }

    enum Images
    {
        TutoImage,
    }

    enum Texts
    {
        QuitText,
    }

    private TutorialUIData _data;
    private int _curImageIndex = 0;

    public override void Init(Transform anchor)
    {
        base.Init(anchor);

        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));
    }

    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);

        _data = uiData as TutorialUIData;
        _curImageIndex = 0;
        Managers.Game._cantInputKey = true;

        if (_data != null && _data._tutoImages.Count > 0)
        {
            UpdateImage();
            UpdateButtons();
        }

        GetButton((int)Buttons.PrevBtn).onClick.AddListener(OnPrevButtonClick);
        GetButton((int)Buttons.NextBtn).onClick.AddListener(OnNextButtonClick);

        GetText((int)Texts.QuitText).gameObject.SetActive(false);
    }

    private void UpdateImage()
    {
        GetImage((int)Images.TutoImage).sprite = _data._tutoImages[_curImageIndex];
    }

    private void UpdateButtons()
    {
        GetButton((int)Buttons.PrevBtn).gameObject.SetActive(_curImageIndex > 0);
        GetButton((int)Buttons.NextBtn).gameObject.SetActive(_curImageIndex < _data._tutoImages.Count - 1);
        GetText((int)Texts.QuitText).gameObject.SetActive(_curImageIndex == _data._tutoImages.Count - 1);
    }

    private void OnPrevButtonClick()
    {
        if (_curImageIndex > 0)
        {
            _curImageIndex--;
            UpdateImage();
            UpdateButtons();
        }
    }

    private void OnNextButtonClick()
    {
        if (_curImageIndex < _data._tutoImages.Count - 1)
        {
            _curImageIndex++;
            UpdateImage();
            UpdateButtons();
        }
    }

    private void OnDisable()
    {
        Managers.Game._cantInputKey = false;

        if (_data._lastTuto)
        {
            Managers.Game._firstTuto = false;
        }
    }
}
