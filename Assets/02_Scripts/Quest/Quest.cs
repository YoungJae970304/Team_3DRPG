using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    //퀘스트 데이터의 퀘스트 정보
    public QuestData QuestData { get; set; }

    //새로운 퀘스트를 초기화하는 생성자
    public Quest(QuestData questData)
    {
        QuestData = questData;
    }

    public static Quest CreateQuest(int id)
    {
        DataTableManager dataTableManager = Managers.DataTable;

        QuestData questData = null;

        foreach (var newQuest in dataTableManager._QuestData)
        {
            if(newQuest.ID == id)
            {
                questData = newQuest;
                break;
            }
        }
        //아이디에 따라 퀘스트 생성하는데 메인인지 서브인지 구분지어 생성
        if(questData != null)
        {
            switch (questData.Type)
            {
                case Define.QuestType.Main:
                    return new Quest(questData);
                case Define.QuestType.Sub:
                    return new Quest(questData);
                default:
                    Logger.Log($"없는 타입의 퀘스트: {questData.Type}");
                    return null;
            }
        }
        else
        {
            Logger.LogError("해당 ID의 퀘스트를 생성하지 못했습니다" + id);
            return null;
        }
    }
}
