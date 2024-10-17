using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using TMPro;
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

    List<QuestData> _LoadQuestDataList = new();


    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);
    }

    private void Awake()
    {
        var questdataTable = Managers.DataTable._QuestData;

        _LoadQuestDataList.AddRange(questdataTable);
        _questScrollList = GetComponent<ScrollView>();
        Bind<TextMeshProUGUI>((typeof(QuestLogTexts)));
        Bind<Button>((typeof(Buttons)));
        Bind<Image>((typeof(QuestImgs)));

        GetButton((int)Buttons.GiveupBtn).onClick.AddListener(GiveUpBtn);
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
        //백그라운드 이미지
        GetImage((int)QuestImgs.BackgroundPanel).sprite = Managers.Resource.Load<Sprite>/*($"Icon/{id}"*/("Icon/TestIcon");
        //오른쪽 패널 이미지
        GetImage((int)QuestImgs.RightPanel).sprite = Managers.Resource.Load<Sprite>/*($"Icon/{id}"*/("Icon/TestIcon");
        //각 아이템 이미지
        GetImage((int)QuestImgs.EquipReward).sprite = Managers.Resource.Load<Sprite>/*($"Icon/{id}"*/("Icon/TestIcon");
        GetImage((int)QuestImgs.PotionReward).sprite = Managers.Resource.Load<Sprite>/*($"Icon/{id}"*/("Icon/TestIcon");
        GetImage((int)QuestImgs.GoldReward).sprite = Managers.Resource.Load<Sprite>/*($"Icon/{id}"*/("Icon/TestIcon");
        GetImage((int)QuestImgs.ExpReward).sprite = Managers.Resource.Load<Sprite>/*($"Icon/{id}"*/("Icon/TestIcon");
    }

    //퀘스트 포기 버튼
    public void GiveUpBtn()
    {

    }
}
