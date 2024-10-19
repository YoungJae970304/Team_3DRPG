using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class QuestLogUI : BaseUI
{
    //왼쪽에서 스크롤뷰에 버튼으로 생성 버튼 누를시 오른쪽에 패널이 열리면서
    //해당 퀘스트의 내용들을 표시해줄 예정
    [SerializeField] public ScrollView _questScrollList;

    enum QuestLogTexts
    {
        QuestLogTitle,
        RewardText,
        InfoTitle,
        InfoTargetText,
    }

    enum QuestImgs
    {
        //백그라운드 패널인데 이미지로 해서 넣어두 되나..
        BackgroundPanel,
        RightPanel,
        EquipReward,
        PotionReward,
        GoldReward,
        ExpReward,
    }

    enum Buttons
    {
        GiveupBtn,
    }

    int _questID;
    List<QuestData> _LoadQuestDataList = new();

    private void Awake()
    {
        var questdataTable = Managers.DataTable._QuestData;

        _LoadQuestDataList.AddRange(questdataTable);
        _questScrollList = GetComponent<ScrollView>();

        Bind<TextMeshProUGUI>(typeof(QuestLogTexts));
        Bind<Image>(typeof(QuestImgs));
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.GiveupBtn).onClick.AddListener(GiveUpBtn);

        QuestData questData = _LoadQuestDataList.Find(q => q.ID == _questID);

        QuestInfoSet(_questID);
    }

    public void QuestInfoSet(int id)
    {
        QuestData questData = _LoadQuestDataList.Find(q => q.ID == id);

        //타이틀은 데이터에 잇는 텍스트가 아니기에
        GetText((int)QuestLogTexts.QuestLogTitle).text = "퀘스트 창";
        //퀘스트 제목
        GetText((int)QuestLogTexts.InfoTitle).text = questData.Name;
        //퀘스트 타겟 정보 (즉, 목표)
        GetText((int)QuestLogTexts.InfoTargetText).text = questData.TargetCount.ToString();
        //보상은 데이터에있는 텍스트가 아니기에
        GetText((int)QuestLogTexts.RewardText).text = "보상";
        //테스트아이콘은 지워야함
        var sprite = Managers.Resource.Load<Sprite>("Icon/TestIcon");/*($"Icon/{id}"*/
        //백그라운드 이미지
        GetImage((int)QuestImgs.BackgroundPanel).sprite = sprite;
        //오른쪽 패널 이미지
        GetImage((int)QuestImgs.RightPanel).sprite = sprite;
        //각 아이템 이미지
        GetImage((int)QuestImgs.EquipReward).sprite = sprite;
        GetImage((int)QuestImgs.PotionReward).sprite = sprite;
        GetImage((int)QuestImgs.GoldReward).sprite = sprite;
        GetImage((int)QuestImgs.ExpReward).sprite = sprite;
    }

    //퀘스트 포기 버튼
    public void GiveUpBtn()
    {

    }
}
