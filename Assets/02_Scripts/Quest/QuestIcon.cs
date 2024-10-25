using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class QuestIcon : BaseUI
{
    enum Images
    {
        QuestIcon,
    }

    private void Awake()
    {
        Bind<Image>(typeof(Images));
    }

    public void SetState(QuestState.State newState, bool startPoint, bool finishPont)
    {
        switch (newState)
        {
            case QuestState.State.CanStart:
                if (startPoint) GetImage((int)Images.QuestIcon).sprite = Managers.Resource.Load<Sprite>("ItemIcon/11012");
                break;
            case QuestState.State.CanFinish:
                if (finishPont) GetImage((int)Images.QuestIcon).sprite = Managers.Resource.Load<Sprite>("ItemIcon/11011");
                break;
            default:
                GetImage((int)Images.QuestIcon).gameObject.SetActive(false);
                break;
        }
    }

}
