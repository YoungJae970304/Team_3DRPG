using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLogUI : BaseUI
{
    //왼쪽에서 스크롤뷰에 버튼으로 생성 버튼 누를시 오른쪽에 패널이 열리면서
    //해당 퀘스트의 내용들을 표시해줄 예정
    public Text _questInfoTilte, _questStatus1, _questStatus2;
    public RawImage[] _questRewardImgs;

    //퀘스트 포기 버튼
    public void GiveUpBtn()
    {

    }

    //나가기 버튼
    public void ExitBtn()
    {
        this.gameObject.SetActive(false);
    }
}
