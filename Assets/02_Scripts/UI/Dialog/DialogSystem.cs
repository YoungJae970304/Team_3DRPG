using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct DialogData
{
    //이름과 대사를 출력할 현재 dialogSystem의 speakers 배열 순번
    public int speakerIndex;//누가 말하고 다음 말 할 순서
    //캐릭터 이름
    public string name;
    [TextArea(3, 5)]
    //대사
    public string dialogue;
}

public class DialogSystem : BaseUI
{
    [SerializeField]
    DialogData[] dialogs;                //현재 분기의 대사 목록 배열

    [SerializeField]
    bool _isAutoStart = true;           //자동 시작 여부
    bool _isFirst = true;                    //최초 1회만 호출하기 위한 변수
    int _currentDialogIndex = -1;    //현재 대사 순번
    int _currentSpeakerIndex = 0;   //현재 말을 하는 회자(Speaker)의 speakers 배열 순번
    float _typingSpeed = 0.03f;       //텍스트 타이핑 효과의 재생 속도
    bool _isTypingEffect = false;     //텍스트 타이핑 효과를 재생중인지

    enum DialogTexts
    {
        NpcName,
        DialogText,
    }

    enum DialogImgs
    {
        DialogImage,
    }

    enum GameObjects
    {
        Arrow,
    }

    void Setup()
    {
        Bind<TextMeshProUGUI>(typeof(DialogTexts));
        Bind<Image>(typeof(DialogImgs));
        Bind<GameObject>(typeof(GameObjects));
        GetGameObject((int)GameObjects.Arrow).SetActive(false);
    }

    //부울 값을 반환해주는 함수 
    public bool UpdateDialog()
    {
        //대사 분기가 시작될 때 1회만 호출
        if (_isFirst)
        {
            Setup();
            if (_isAutoStart)
            { SetNextDialog(); }
            _isFirst = false;
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.F))
        {
            //텍스트 타이핑 효과를 재생중일때 클릭 또는 F 누르면 타이핑 효과 종료
            if (_isTypingEffect)
            {
                _isTypingEffect = false;
                //타이핑 효과를 중지하고, 현재 대사 전체를 출력한다.
                StopCoroutine(OnTypingText());
                GetText((int)DialogTexts.DialogText).text = dialogs[_currentDialogIndex].dialogue;
                GetGameObject((int)GameObjects.Arrow).SetActive(true);
                //return false;
            }
            //대사가 남아 있을경우 다음 대사 진행
            if (dialogs.Length > _currentDialogIndex + 1)
            {
                SetNextDialog();
            }
            else
            {
                //대사가 더이상 없을 경우 처음으로 되돌림
                _isFirst = true;
                _currentDialogIndex = -1;
                return true;
            }
        }
        return false;
    }

    void SetNextDialog()
    {
        if (_currentDialogIndex + 1 >= dialogs.Length) return;
        //다음 대사를 진행 하도록
        _currentDialogIndex++;
        //현재 화자 순번 설정
        _currentSpeakerIndex = dialogs[_currentDialogIndex].speakerIndex;
        //현재 화자 이름 텍스트 설정
        GetText((int)DialogTexts.NpcName).text = dialogs[_currentDialogIndex].name;
        //코루틴 텍스트 효과로 대처
        StartCoroutine(OnTypingText());
    }

    IEnumerator OnTypingText()
    {
        if (_currentDialogIndex < 0 || _currentDialogIndex >= dialogs.Length)
        {
            yield break;
        }

        int index = 0;
        _isTypingEffect = true;
        string currDIalogue = dialogs[_currentDialogIndex].dialogue;
        //텍스트를 한글자씩 타이핑 치듯 재생
        while (index <= currDIalogue.Length)
        {
            GetText((int)DialogTexts.DialogText).text = currDIalogue.Substring(0, index);
            index++;
            yield return new WaitForSeconds(_typingSpeed);
        }
        _isTypingEffect = false;
        //대사가 완료 되었을 때 출력 되는 커서 활성화
        GetGameObject((int)GameObjects.Arrow).SetActive(true);
    }
}
