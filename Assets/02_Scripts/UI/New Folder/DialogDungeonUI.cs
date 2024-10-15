using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogDungeonUI : MonoBehaviour
{
    [SerializeField]
    DialogSystem _dialogSystem;
    public GameObject _dungeonUI;
    public Button _dungeonBtn;
    public Canvas DungeonUI;

    private void OnEnable()
    {
        StartCoroutine(DialogStart());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Start()
    {

    }

    IEnumerator DialogStart()
    {
        _dungeonBtn.gameObject.SetActive(false);

        yield return new WaitUntil(() => _dialogSystem.UpdateDialog());

        _dungeonBtn.gameObject.SetActive(true);

        yield return new WaitUntil(() => _dialogSystem.UpdateDialog());

        yield return new WaitForSeconds(0.2f);
        _dungeonBtn.gameObject.SetActive(false);
    }


    public void OpenDungeonUI()
    {
        _dungeonUI.SetActive(true);
        _dungeonBtn.gameObject.SetActive(false);
    }
}
