using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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


    enum GameObjects
    {
        RightPanel,
    }
    #endregion

    int _questID;
    int _questCount = 0;
    public GameObject _questList;

    List<QuestData> _LoadQuestDataList = new();
    [Header("받은 퀘스트 표시")]
    Dictionary<string, QuestListBtn> _QuestNameToListBtn = new Dictionary<string, QuestListBtn>();
    QuestState.State _questState;
    [SerializeField] RectTransform contentRectTrs;
    [SerializeField] RectTransform scrollRectTrs;
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        var questdataTable = Managers.DataTable._QuestData;
        _LoadQuestDataList.AddRange(questdataTable);

        Bind<TextMeshProUGUI>(typeof(QuestLogTexts));
        Bind<Image>(typeof(QuestImgs));
        Bind<GameObject>(typeof(GameObjects));
        QuestData questData = _LoadQuestDataList.Find(q => q.ID == _questID);
        Logger.LogWarning($"리스트 확인{questData}");
        //처음 받은 퀘스트의 정보창
        GetGameObject((int)GameObjects.RightPanel).gameObject.SetActive(false);

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
        GetText((int)QuestLogTexts.InfoTargetText).text = $"{questData.TargetID.ToString()} + {questData.TargetCount.ToString()}";
        //보상은 데이터에있는 텍스트가 아니기에
        GetText((int)QuestLogTexts.RewardText).text = "보상";
        //테스트아이콘은 지워야함
        Sprite sprite = Resources.Load<Sprite>("Icon/TestIcon");/*($"Icon/{id}" <- 이런 식으로*/
        //백그라운드 이미지(바꿔주면됨)
        GetImage((int)QuestImgs.BackgroundPanel).sprite = Managers.Resource.Load<Sprite>(default);
        //오른쪽 패널 이미지(바꿔주면됨)
        GetImage((int)QuestImgs.RightPanel).sprite = Managers.Resource.Load<Sprite>(default);
        //1번 아이콘에 포션이미지로 변경
        if (questData.RewardType1 == QuestData.RewardType.Potion) { GetImage((int)QuestImgs.ItemReward).sprite = sprite; }
        //리워드 타입 2 또는 3의 이미지
        if (questData.RewardType2 == QuestData.RewardType.Gold) { GetImage((int)QuestImgs.GoldReward).sprite = sprite; }
        if (questData.RewardType3 == QuestData.RewardType.Exp) { GetImage((int)QuestImgs.ExpReward).sprite = sprite; }
    }

    public void OpenSetInfo()
    {
        GetGameObject((int)GameObjects.RightPanel).gameObject.SetActive(true);
        _questCount++;
    }

    //퀘스트 포기 버튼
    public void GiveUpBtn(GameObject go)
    {
        //스크롤 뷰에있던 퀘스트 리스트 사라지게
        Managers.Resource.Destroy(go);
        //퀘스트를 받기전 상태로 돌리고, 퀘스트NPC 초기화
        if (_questCount == 0) return;
        _questCount--;
        Quest quest = null;
        quest.SetSatus(_questState = QuestState.State.CanStart);
    }

    //받은 퀘스트에 따라서 스크롤뷰에 퀘스트리스트 버튼 프리팹을 생성하는 함수
    public QuestListBtn InstantiateQuestListBtn(Quest quest, UnityAction selectAction)
    {
        QuestListBtn questListBtn = Managers.Resource.Instantiate("UI/QuestListBtn", _questList.transform).GetComponent<QuestListBtn>();

        questListBtn.name = quest._QuestData.ID.ToString();
        RectTransform btnRectTransform = questListBtn.GetComponent<RectTransform>();
        //버튼에 있는 초기화 함수 즉, 선택했을 때 데이터에서 받아온 퀘스트의 제목를 도출
        questListBtn.Initialize(quest._QuestData.Info, () =>
        {
            //생성된 애를 선택하는 액션
            selectAction();
            //업데이트 스크롤함수를 호출시켜서 그에 맞는 위치에 생성 시키기
            UpdateScrolling(btnRectTransform);
        });
        //딕셔너리에 추가하여 관리
        _QuestNameToListBtn[quest._QuestData.Name] = questListBtn;
        return questListBtn;
    }

    public QuestListBtn CreateBtnIfNotExists(Quest quest, UnityAction selectAction)
    {
        QuestListBtn questListBtn = null;
        if (!_QuestNameToListBtn.ContainsKey(quest._QuestData.ID.ToString()))
        {
            questListBtn = InstantiateQuestListBtn(quest, selectAction);
        }
        else
        {
            questListBtn = _QuestNameToListBtn[quest._QuestData.ID.ToString()];
        }

        return questListBtn;
    }

    public void UpdateScrolling(RectTransform btnRectTransform)
    {
        //선택된 버튼의 최소 y 최대 y 계산
        float btnYMin = Mathf.Abs(btnRectTransform.anchoredPosition.y);
        float btnYMax = btnYMin + btnRectTransform.rect.height;

        //Content영역의 y값
        float contentYMin = contentRectTrs.anchoredPosition.y;
        float contentYMax = contentYMin + scrollRectTrs.rect.height;

        if(btnYMax > contentYMin)
        {
            contentRectTrs.anchoredPosition = new Vector2 (contentRectTrs.anchoredPosition.x,btnYMax - scrollRectTrs.rect.height);
        }
        else if(btnYMax < contentYMin)
        {
            contentRectTrs.anchoredPosition = new Vector2(contentRectTrs.anchoredPosition.x, btnYMin);
        }
    }
}
