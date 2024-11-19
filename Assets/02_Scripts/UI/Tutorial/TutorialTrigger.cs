using UnityEngine;
using System.Collections.Generic;

public class TutorialTrigger : MonoBehaviour
{
    // 튜토리얼 이미지가 담길 리스트
    public List<Sprite> _tutorialImages;

    // 이미 본 튜토리얼인지 체크하기 위한 bool 변수
    private bool _hasShownTuto = false;

    // 마지막 튜토리얼인지 체크하기 위한 bool 변수
    public bool _lastTutorial;

    // 튜토리얼 존에 진입했을 때 튜토리얼을 열 수 있도록 하기 위한 충돌 체크
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_hasShownTuto && Managers.Game._firstTuto)
        {
            OpenTutorialUI();
            _hasShownTuto = true;
        }
    }

    // 튜토리얼UI Data를 전달해 지정한 튜토리얼을 열기 위한 메서드
    private void OpenTutorialUI()
    {
        TutorialUI tutorialUI = Managers.UI.GetActiveUI<TutorialUI>() as TutorialUI;

        if (tutorialUI == null)
        {
            TutorialUIData tutorialUIData = new TutorialUIData();
            tutorialUIData._tutoImages = new List<Sprite>(_tutorialImages);
            tutorialUIData._lastTuto = _lastTutorial;

            Managers.UI.OpenUI<TutorialUI>(tutorialUIData);
        }
    }
}