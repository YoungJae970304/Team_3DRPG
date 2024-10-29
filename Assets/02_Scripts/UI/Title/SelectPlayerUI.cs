using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;

public class SelectPlayerUI : BaseUI
{   [Multiline(5)]
    [SerializeField] string descTxt;
    enum SelectToggles
    {
        MeleePlayer,
        MagePlayer,
    }
    ConfirmUIData confirmUIData = new ConfirmUIData();
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
        

        confirmUIData.DescTxt = descTxt;// "게임 진입 후 캐릭터의 변경이 불가능 합니다!\r\n선택한 캐릭터로 진행 하시겠습니까?";
        ConfirmUIData.confirmAction += () => {
            Animator _fadeAnim= GameObject.FindWithTag("SceneManager").GetComponent<Animator>();
            _fadeAnim.SetTrigger("doFade");
        };

        if (confirmUI == null)
        {
            Managers.UI.OpenUI<ConfirmUI>(confirmUIData);
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
   
