using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopDialogUI : BaseUI
{
    enum Buttons
    {
        YesBtn,
    }

    public DialogSystem[] _dialogSystem;

    bool _isOpenUI = false;

    public override void Init(Transform anchor)
    {
        base.Init(anchor);

        //대화 시작 시 모든 유아이 닫아버리기
        Managers.UI.CloseAllOpenUI();
        StartCoroutine(ShopDialog());
    }

    private void Awake()
    {
        Bind<Button>(typeof(Buttons));
    }

    private void OnEnable()
    {
        GetButton((int)Buttons.YesBtn).onClick.AddListener(() =>
        {
            OpenShopUI();
            _isOpenUI = true;
        });
    }

    private void OnDisable()
    {
        GetButton((int)Buttons.YesBtn).onClick.RemoveAllListeners();
        Managers.Game._isActiveDialog = false;
        Managers.Game._player._isMoving = true;
    }

    IEnumerator ShopDialog()
    {
        Managers.Game._player._isMoving = false;
        yield return new WaitUntil(() => _dialogSystem[0].UpdateDialog());
        _isOpenUI = false;
        yield return new WaitUntil(() => _isOpenUI);
        Managers.UI.CloseUI(this);
    }

    //던전 UI 오픈 함수 버튼 클릭시 생성
    public void OpenShopUI()
    {
        ShopUI shopUI = Managers.UI.GetActiveUI<ShopUI>() as ShopUI;

        if (shopUI != null)
        {
            ShopUIData shopUIData = new ShopUIData();
            //_TEMP
            shopUIData._itemCode = new List<(int, int)>();
            shopUIData._itemCode.Remove((11005, 1));
            shopUIData._itemCode.Remove((11006, 1));
            shopUIData._itemCode.Remove((11007, 1));
            Managers.UI.CloseUI(shopUI);
        }
        else
        {
            ShopUIData shopUIData = new ShopUIData();
            //_TEMP
            shopUIData._itemCode = new List<(int,int)>();
            shopUIData._itemCode.Add((11005, 1));
            shopUIData._itemCode.Add((11006, 1));
            shopUIData._itemCode.Add((11007, 1));
            Managers.UI.OpenUI<ShopUI>(shopUIData);
            _isOpenUI = true;
        }
    }
}
