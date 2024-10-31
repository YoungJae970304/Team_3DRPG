using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimpleQuestText : BaseUI
{
    enum QuestText
    {
        SimpleQuestText,
        QuestRequireText,
    }
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        Bind<TextMeshProUGUI>(typeof(QuestText));

    }
    public void TextChange(int i)
    {
        GetText((int)QuestText.SimpleQuestText).text = Managers.QuestManager._questID[i].ToString();
        GetText((int)QuestText.SimpleQuestText).text = $"{Managers.QuestManager._countCheck[i]} / {Managers.QuestManager._questrequirements}";
    }
}
