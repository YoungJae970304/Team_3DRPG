using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : BaseUI
{
    [Multiline(5)]
    [SerializeField] string descTxt;
    enum SelectButtons
    {
        Option,
        GiveUp,
        ShutDownGame,
    }
    public void Awake()
    {
        Bind<Button>(typeof(SelectButtons));
    }
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        
    }
    
    public void OnClickOptionBtn()
    {

    }
    public void OnClickGiveUpBtn()
    {
        ConfirmUI confirmUI = Managers.UI.GetActiveUI<ConfirmUI>() as ConfirmUI;
        ConfirmUIData confirmUIData = new ConfirmUIData();

        confirmUIData.DescTxt = descTxt;
        confirmUIData.confimAction = () =>
        {
            Animator _fadeAnim = GameObject.FindWithTag("SceneManager").GetComponent<Animator>();
            _fadeAnim.SetTrigger("doFade");
        };
        if (confirmUI == null)
        {
            Managers.UI.OpenUI<ConfirmUI>(confirmUIData);
        }
    }
}
