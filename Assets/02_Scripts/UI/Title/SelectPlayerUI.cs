using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;

public class SelectPlayerUI : BaseUI
{
    enum SelectToggles
    {
        MeleePlayer,
        MagePlayer,
    }

    enum SelectButtons
    {
        StartBtn,
        TitleBtn
    }

    private void Awake()
    {
        Bind<Toggle>(typeof(SelectToggles));
        Bind<Button>(typeof(SelectButtons));
    }

    // 선택되어 있는 토글에 따라 플레이어 타입을 결정 -> 이후 게임매니저에서 씬 로드될 때 마다 해당 플레이어 생성
    public void OnClickStartBtn()
    {
        ConfirmUI confirmUI = Managers.UI.GetActiveUI<ConfirmUI>() as ConfirmUI;

        if (confirmUI == null)
        {
            Managers.UI.OpenUI<ConfirmUI>(new BaseUIData());
        }


        Toggle melee = GetToggle((int)SelectToggles.MeleePlayer);
        Toggle mage = GetToggle((int)SelectToggles.MagePlayer);

        if (melee.isOn)
        {
            Managers.Game._playerType = Define.PlayerType.Melee;
        }
        else if (mage.isOn)
        {
            Managers.Game._playerType = Define.PlayerType.Mage;
        }
    }
}
