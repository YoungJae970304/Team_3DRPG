using UnityEngine;
using System.Collections.Generic;

public class TutorialTrigger : MonoBehaviour
{
    public List<Sprite> _tutorialImages;
    private bool _hasShownTuto = false;
    public bool _lastTutorial;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_hasShownTuto && Managers.Game._firstTuto)
        {
            OpenTutorialUI();
            _hasShownTuto = true;
        }
    }

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