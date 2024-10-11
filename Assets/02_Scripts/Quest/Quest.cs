using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
   public QuestData _QuestData { get; set; }

    public Quest(QuestData questData)
    {
        _QuestData = questData;
    }
}
