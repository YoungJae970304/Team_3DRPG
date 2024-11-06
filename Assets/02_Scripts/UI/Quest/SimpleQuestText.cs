using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimpleQuestText : BaseUI
{
    public int _questTextID;
    enum QuestText
    {
        SimpleQuestText,
        QuestRequireText,
    }
    private void Awake()
    {
        _questTextID = Managers.QuestManager._questTextID;
    }
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
 
        Bind<TextMeshProUGUI>(typeof(QuestText));
        
        TextChange(_questTextID);
    }
    public void TextChange(int i)
    {
        GetText((int)QuestText.SimpleQuestText).text = $"{Managers.QuestManager._targetName[i]}";
        GetText((int)QuestText.QuestRequireText).text = $"{Managers.QuestManager._countCheck[i]} / {Managers.QuestManager._questrequirements[i]}";
    }
}
