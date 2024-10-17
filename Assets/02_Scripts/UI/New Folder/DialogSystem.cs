using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogSystem : BaseUI
{

    //[SerializeField]
    //Speaker[] speakers;           //대화에 참여하는 캐릭터들의 UI 배열

    [SerializeField]
    DialogData[] dialogs;         //현재 분기의 대사 목록 배열

    [SerializeField]
    bool _isAutoStart = true;      //자동 시작 여부
    bool _isFirst = true;          //최초 1회만 호출하기 위한 변수
    int _currentDialogIndex = -1;  //현재 대사 순번
    int _currentSpeakerIndex = 0;  //현재 말을 하는 회자(Speaker)의 speakers 배열 순번
    float _typingSpeed = 0.03f;     //텍스트 타이핑 효과의 재생 속도
    bool _isTypingEffect = false;  //텍스트 타이핑 효과를 재생중인지

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

    private void Awake()
    {
        //Setup();
    }

    void Setup()
    {
        //모든 대화 관련 게임오브젝트 비활성화
        //for (int i = 0; i < speakers.Length; ++i)
        //{
        //    //비활성화
        //    SetActiveObjects(speakers[i], false);
        //    //캐릭터 이미지는 보이도록 설정
        //    //speakers[i].spriteRenderer.gameObject.SetActive(true);
        //}

        GetGameObject((int)GameObjects.Arrow).SetActive(false);
    }

    void SetActiveObjects(/*Speaker speaker,*/ bool visible)
    {
        //speaker.imageDialog.gameObject.SetActive(visible);
        //speaker.textName.gameObject.SetActive(visible);
        //speaker.textDialog.gameObject.SetActive(visible);

        //화살표는 대사가 종료 되었을 때만 활성화 하기 때문에 항상 false
        GetGameObject((int)GameObjects.Arrow).SetActive(false);
        //speaker.objectArrow.SetActive(false);

        //캐릭터 알파 값 변경
        //캐릭터의 대화 내용도 앞으로 땡겨주는 역할을 함
        //트루면 1 펄스면 0.2f의 알파값을 갖도록 함
        //Color color = speaker.spriteRenderer.color;
        //color.a = visible == true ? 1 : 0.2f;
        //speaker.spriteRenderer.color = color;
    }

    public bool UpdateDialog()//부울 값을 반환해주는 함수 
    {
        //대사 분기가 시작될 때 1회만 호출
        if (_isFirst == true)
        {
            //초기화. 캐릭터 이미지는 활성화 하고,
            //대사 관련 UI는 모두 비활성화
            Setup();
            //자동 재생(isAutoStart = true)으로 설정되어 있으면
            //첫 번째 대사 재생
            if (_isAutoStart)
            { SetNextDialog(); }

            _isFirst = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            //텍스트 타이핑 효과를 재생중일때
            //마우스 왼쪽 클릭하면 타이핑 효과 종료
            if (_isTypingEffect == true)
            {
                _isTypingEffect = false;

                //타이핑 효과를 중지하고, 현재 대사 전체를 출력한다.
                StopCoroutine("OnTypingText");

                //speaker = 0 dialog = -1
                //speakers[_currentSpeakerIndex].textDialog.text = dialogs[_currentDialogIndex].dialogue;
                GetText((int)DialogTexts.DialogText).text = dialogs[_currentDialogIndex].dialogue;
                //대사가 완료 되었을 때 출력되는 커서 활성화
                //speakers[_currentSpeakerIndex].objectArrow.SetActive(true);
                GetGameObject((int)GameObjects.Arrow).SetActive(true);

                return false;//false값을 반환 함
            }

            //대사가 남아 있을경우 다음 대사 진행
            if (dialogs.Length > _currentDialogIndex + 1)
            {
                SetNextDialog();
            }
            else
            {
                //대사가 더 이상 없을 경우
                //모든 오브젝트를 비활성화 하고 true 반환
                //for (int i = 0; i < speakers.Length; ++i)
                //{
                //    //현재 대화에 참여했던 모든 캐릭터,
                //    //대화 관련 UI를 보이지 않게 비활성화
                //    SetActiveObjects(speakers[i], false);
                //    //SetActiveObject()에 캐릭터 이미지를 보이지 않게 하는 부분이 없기 때문에 별도로 호출
                //    //speakers[i].spriteRenderer.gameObject.SetActive(false);
                //}
                _isFirst = true; 
                _currentDialogIndex = -1;
                return true;
            }
        }
        return false;
    }

    void SetNextDialog()
    {
        //이전 화자의 대화 관련 오브젝트 비활성화 말하는 사람키면 말안하는 사람 꺼주기
        //SetActiveObjects(speakers[currentSpeakerIndex], false);
        //다음 대사를 진행 하도록
        _currentDialogIndex++;
        //현재 화자 순번 설정
        _currentSpeakerIndex = dialogs[_currentDialogIndex].speakerIndex;
        //현재 화자의 대화 관련 오브젝트 활성화
        //SetActiveObjects(speakers[_currentSpeakerIndex], true);
        //현재 화자 이름 텍스트 설정
        GetText((int)DialogTexts.NpcName).text = dialogs[_currentDialogIndex].name;
        //speakers[_currentSpeakerIndex].textName.text = dialogs[_currentDialogIndex].name;
        //현재 화자의 대사 텍스트 설정
        //코루틴 텍스트 효과로 대처
        StartCoroutine("OnTypingText");
        /*speakers[currentSpeakerIndex].textDialog.text=
       dialogs[currentDialogIndex].dialogue;*/
    }

    IEnumerator OnTypingText()
    {
        int index = 0;

        _isTypingEffect = true;

        //텍스트를 한글자씩 타이핑 치듯 재생
        while (index <= dialogs[_currentDialogIndex].dialogue.Length)
        {
            GetText((int)DialogTexts.DialogText).text = dialogs[_currentDialogIndex].dialogue.Substring(0, index);
            //speakers[_currentSpeakerIndex].textDialog.text = dialogs[_currentDialogIndex].dialogue.Substring(0, index);
            //C# 문자열 정리 기능 참조
            index++;

            yield return new WaitForSeconds(_typingSpeed);
        }

        _isTypingEffect = false;

        //대사가 완료 되었을 때 출력 되는 커서 활성화
        GetGameObject((int)GameObjects.Arrow).SetActive(true);
        //speakers[_currentSpeakerIndex].objectArrow.SetActive(true);

    }
}
/*
[System.Serializable]
public struct Speaker
{
    //캐릭터 이미지(청자/화자 알파값 제어)
    //public SpriteRenderer spriteRenderer;
    //대화창 Image UI
    public Image imageDialog;
    //현재 대사중인 캐릭터 이름 출력 Text UI
    public Text textName;
    //현재 대사 출력 Text UI
    public Text textDialog;
    //대사가 완료 되었을 때 출력되는 커서 오브젝트
    public GameObject objectArrow;
}
*/

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