using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimpleQuestText : BaseUI
{
    public int test123;
    enum QuestText
    {
        SimpleQuestText,
        QuestRequireText,
    }
    private void Awake()
    {
        test123 = Managers.QuestManager.test123;
    }
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
 
        Bind<TextMeshProUGUI>(typeof(QuestText));
        
        TextChange(test123);
    }
    public void TextChange(int i)
    {
        GetText((int)QuestText.SimpleQuestText).text = $"{Managers.QuestManager._targetName[i]}";
        GetText((int)QuestText.QuestRequireText).text = $"{Managers.QuestManager._countCheck[i]} / {Managers.QuestManager._questrequirements[i]}";
    }
}
