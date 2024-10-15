using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogDungeonUI : MonoBehaviour
{
    [SerializeField]
    DialogSystem[] _dialogSystem;
    public GameObject _dungeonUI;
    public Button _dungeonBtn;
   
    public void StartDialog()
    {
        //Managers.Input.KeyAction = null;
        //Managers.Input.MouseAction = null;
        StartCoroutine(DialogStart());
    }

    IEnumerator DialogStart()
    {
        //첫 대사 시작전에 버튼을 숨기고 있다가
        _dungeonBtn.gameObject.SetActive(false);
        //첫 대사 시작
        yield return new WaitUntil(() => _dialogSystem[0].UpdateDialog());
        //버튼 활성화 하면서
        _dungeonBtn.gameObject.SetActive(true);
        //두 번째 대사
        yield return new WaitUntil(() => _dialogSystem[1].UpdateDialog());
        //그냥 다이얼로그를 종료할경우 버튼만 비활성화
        yield return new WaitForSeconds(0.2f);
        _dungeonBtn.gameObject.SetActive(false);
        //Managers.Input.KeyAction = Managers.Input.Clear;
    }

    //던전 UI 오픈 함수
    public void OpenDungeonUI()
    {
        _dungeonUI.SetActive(true);
        _dungeonBtn.gameObject.SetActive(false);
        //Managers.Input.KeyAction = null;
    }
}
