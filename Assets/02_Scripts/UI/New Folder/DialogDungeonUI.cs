using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogDungeonUI : BaseUI
{
    [SerializeField]
    DialogSystem[] _dialogSystem;
    public GameObject _dungeonUI;
    public Button _dungeonBtn;
    public IEnumerator Start()
    {
        _dungeonUI.SetActive(false);
        _dungeonBtn.gameObject.SetActive(false);

        yield return new WaitUntil(() => _dialogSystem[0].UpdateDialog());

        _dungeonBtn.gameObject.SetActive(true);

        yield return new WaitUntil(() => _dialogSystem[1].UpdateDialog());

        _dungeonBtn.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }
    
    public void OpenDungeonUI()
    {
        if (_dungeonUI != null)
        {
            _dungeonUI.SetActive(true);
        }
        _dungeonBtn.gameObject.SetActive(false);
    }
}
