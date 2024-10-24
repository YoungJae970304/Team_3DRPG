using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestDisplay : BaseUI
{
    /// <summary>
    ///퀘스트창에서 보여질 내용을 작성한다.
    /// 진행중인 퀘스트를 화면에서 보여줄 내용
    /// 퀘스트 창에서는 Reward를 보여줘야함
    /// RewardType은 화면에 보여지지 않는다.
    /// 퀘스트 시작 레벨 요구 사항은 굳이 보여줄 필요가 없다.
    /// 퀘스트 이름에 타입 까지 적어서 표시
    /// </summary>
    
    #region BIND
    enum DisplayTexts
    {
        //퀘스트 진행중
        DisplayName,
        //진행중인 퀘스트의 제목
        QuestInfoText,
        //진행 중인 퀘스트의 타겟Amount
        QuestRequireText,
    }

    enum DisplayImgs
    {
        QuestInfo,
    }
    #endregion

    List<QuestData> _LoadQuestDataList = new();
    int _questID;

    private void Awake()
    {
        Bind<TextMeshProUGUI>(typeof(DisplayTexts));
        Bind<Image>((typeof(DisplayImgs)));

        var questdataTable = Managers.DataTable._QuestData;

        _LoadQuestDataList.AddRange(questdataTable);

        QuestData questData = _LoadQuestDataList.Find(q => q.ID == _questID);

        AmountCheck(_questID);
    }

    void AmountCheck(int id)
    {
        QuestData questData = _LoadQuestDataList.Find(q => q.ID == id);

        GetText((int)DisplayTexts.DisplayName).text = "진행중인 퀘스트";
        GetText((int)DisplayTexts.QuestInfoText).text = questData.Name;
        GetText((int)DisplayTexts.QuestRequireText).text = questData.TargetCount.ToString();
        GetImage((int)DisplayImgs.QuestInfo).sprite = Managers.Resource.Load<Sprite>("sptrite/UI/T_TPI_UiQuest1_UIAtlas_1");
    }

    public void UpdateDisplay()
    {
        //모으거나 처치할 경우 업데이트 시켜주기..
        AmountCheck(_questID);
    }
}
