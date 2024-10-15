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
        StartCoroutine(DialogStart());
    }

    IEnumerator DialogStart()
    {
        //첫 대사 시작전에 버튼을 숨기고 있다가
        _dungeonBtn.gameObject.SetActive(false);

        yield return new WaitUntil(() => _dialogSystem[0].UpdateDialog());

        _dungeonBtn.gameObject.SetActive(true);

        yield return new WaitUntil(() => _dialogSystem[1].UpdateDialog());

        yield return new WaitForSeconds(0.2f);
        _dungeonBtn.gameObject.SetActive(false);
    }

    public void OpenDungeonUI()
    {
        _dungeonUI.SetActive(true);
        _dungeonBtn.gameObject.SetActive(false);
    }
}
