using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class QuestLogUI : BaseUI
{
    #region BIND
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
        ItemReward,
        GoldReward,
        ExpReward,
    }

    enum Buttons
    {
        GiveupBtn,
    }

    enum GameObjects
    {
        RightPanel,
    }
    #endregion

    int _questID;

    List<QuestData> _LoadQuestDataList = new();

    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        var questdataTable = Managers.DataTable._QuestData;
        _LoadQuestDataList.AddRange(questdataTable);

        Bind<TextMeshProUGUI>(typeof(QuestLogTexts));
        Bind<Image>(typeof(QuestImgs));
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        GetButton((int)Buttons.GiveupBtn).onClick.AddListener(GiveUpBtn);
        QuestData questData = _LoadQuestDataList.Find(q => q.ID == _questID);
        Logger.LogWarning($"리스트 확인{questData}");

        QuestInfoSet(_questID);
    }

    public void QuestInfoSet(int id)
    {
        QuestData questData = _LoadQuestDataList.Find(q => q.ID == id);
        //처음 받은 퀘스트의 정보창
        GetGameObject((int)GameObjects.RightPanel).gameObject.SetActive(true);
        //타이틀은 데이터에 잇는 텍스트가 아니기에
        GetText((int)QuestLogTexts.QuestLogTitle).text = "퀘스트 창";
        //퀘스트 제목
        GetText((int)QuestLogTexts.InfoTitle).text = questData.Name;
        //퀘스트 타겟 정보 (즉, 목표)
        GetText((int)QuestLogTexts.InfoTargetText).text = $"{questData.TargetID.ToString()} + {questData.TargetCount.ToString()}";
        //보상은 데이터에있는 텍스트가 아니기에
        GetText((int)QuestLogTexts.RewardText).text = "보상";
        //테스트아이콘은 지워야함
        Sprite sprite = Resources.Load<Sprite>("Icon/TestIcon");/*($"Icon/{id}" <- 이런 식으로*/
        //백그라운드 이미지(바꿔주면됨)
        GetImage((int)QuestImgs.BackgroundPanel).sprite = Managers.Resource.Load<Sprite>("none");
        //오른쪽 패널 이미지(바꿔주면됨)
        GetImage((int)QuestImgs.RightPanel).sprite = Managers.Resource.Load<Sprite>("none");
        //1번 아이콘을 장비이미지로 변경
        if (questData.ValType1 == QuestData.RewardType.Equipped) { GetImage((int)QuestImgs.ItemReward).sprite = sprite; }
        //1번 아이콘에 포션이미지로 변경
        if (questData.ValType1 == QuestData.RewardType.Potion) { GetImage((int)QuestImgs.ItemReward).sprite = sprite; }
        //리워드 타입 2 또는 3의 이미지
        if (questData.ValType2 == QuestData.RewardType.Gold) { GetImage((int)QuestImgs.GoldReward).sprite = sprite; }
        if (questData.ValType3 == QuestData.RewardType.Exp) { GetImage((int)QuestImgs.ExpReward).sprite = sprite; }
    }

    public void OpenSetInfo()
    {

        GetGameObject((int)GameObjects.RightPanel).gameObject.SetActive(true);
    }

    //퀘스트 포기 버튼
    public void GiveUpBtn()
    {
        //스크롤 뷰에있던 버튼을 비활성화로
        QuestListBtn questListBtn = Managers.UI.GetActiveUI<QuestListBtn>() as QuestListBtn;
        if (questListBtn != null)
        {
            Managers.UI.CloseUI(questListBtn);
        }
        //퀘스트를 받기전 상태로 돌리고, 퀘스트NPC 초기화

    }

    //받은 퀘스트에 따라서 스크롤뷰에 퀘스트리스트 버튼 프리팹을 생성하는 함수
    public QuestListBtn CreateQuestListBtn(Quest quest, UnityAction selectAction)
    {
        QuestListBtn questListBtn = Managers.UI.GetActiveUI<QuestListBtn>() as QuestListBtn;

        if(questListBtn != null)
        {
            Managers.UI.OpenUI<QuestListBtn>(new BaseUIData());
        }
    
        questListBtn.name = quest._QuestData.Name;
        RectTransform btnRectTransform = questListBtn.GetComponent<RectTransform>();
        //버튼에 있는 초기화 함수 즉, 선택했을 때 데이터에서 받아온 퀘스트의 제목를 도출
        questListBtn.Initialize(quest._QuestData.Info, () =>
        {
            //생성된 애를 선택하는 액션
            selectAction();
            //업데이트 스크롤함수를 호출시켜서 그에 맞는 위치에 생성 시키기
            UpdateScrolling(btnRectTransform);
        });
        return questListBtn;
    }

    public void UpdateScrolling(RectTransform btnRectTransform)
    {

    }
}
