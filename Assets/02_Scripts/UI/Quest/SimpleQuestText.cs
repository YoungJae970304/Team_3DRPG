using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimpleQuestText : BaseUI
{
    int test123;
    enum QuestText
    {
        SimpleQuestText,
        QuestRequireText,
    }
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
 
        Bind<TextMeshProUGUI>(typeof(QuestText));
        test123 = Managers.QuestManager.test123;
        TextChange(test123);
    }
    public void TextChange(int i)
    {
        GetText((int)QuestText.SimpleQuestText).text = $"{Managers.QuestManager._questName[i]}";
        GetText((int)QuestText.QuestRequireText).text = $"{Managers.QuestManager._countCheck[i]} / {Managers.QuestManager._questrequirements[i]}";
    }
}
