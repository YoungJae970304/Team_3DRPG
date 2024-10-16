using UnityEngine;

public class Interectable : MonoBehaviour
{
    [SerializeField] public Canvas UI;
    [SerializeField] public Canvas DungeonDialogUI;
    [SerializeField] public DialogActive _dialogActive;
    public virtual void Interection(GameObject gameObject)
    {
        Debug.Log(name);
    }

    public virtual void UIPopUp(bool active)
    {
        UI.enabled = active;
    }

    public virtual void DungeonNpcDialog()
    {
        Logger.Log("UI생성 호출");
        DialogDungeonUI dialogDungeonUI = Managers.UI.GetActiveUI<DialogDungeonUI>() as DialogDungeonUI;
        Logger.Log("다이얼로그 UI 상태:"+ (dialogDungeonUI != null ? "활성화됨" : "비활성화됨"));
        if (dialogDungeonUI != null)
        {
            Logger.Log("다이얼로그 상태 변경 시도");
            _dialogActive.DialogActiveState(false);
            Logger.Log("다이얼로그UI닫힘");
            Managers.UI.CloseUI(dialogDungeonUI);
        }
        else
        {
            _dialogActive.DialogActiveState(true);
            Managers.UI.OpenUI<DialogDungeonUI>(new BaseUIData());
            Logger.Log("다이얼로그UI열림");
        }
    }
}
