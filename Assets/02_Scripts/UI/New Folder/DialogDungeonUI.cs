using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogDungeonUI : BaseUI
{
    [SerializeField]
    DialogSystem[] _dialogSystem;
    public GameObject _dungeonUI;
    public Button _dungeonBtn;
    IEnumerator Start()
    {
        _dungeonBtn.gameObject.SetActive(false);

        yield return new WaitUntil(() => _dialogSystem[0].UpdateDialog());

        _dungeonBtn.gameObject.SetActive(true);

        yield return new WaitUntil(() => _dialogSystem[1].UpdateDialog());

        yield return new WaitForSeconds(0.2f);
        this.gameObject.SetActive(false);
        _dungeonBtn.gameObject.SetActive(false);
    }

    public void OpenDungeonUI()
    {
        _dungeonUI.SetActive(true);
        _dungeonBtn.gameObject.SetActive(false);
    }
}
