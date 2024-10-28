using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : BaseUI
{
    [Multiline(5)]
    [SerializeField] string descTxt;
    ConfirmUIData confirmUIData = new ConfirmUIData();
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
        
        descTxt = "던전을 포기 \n 하시겠습니까?";
      
        confirmUIData.DescTxt = descTxt;

        ConfirmUIData.confirmAction = async () =>
        {
            await Task.Delay(2000);
            Managers.Scene.SceneChange("main");
        };
        if (confirmUI == null)
        {
            Managers.UI.OpenUI<ConfirmUI>(confirmUIData);
        }
    }
    public void OnClickShutDownBtn()
    {
        ConfirmUI confirmUI = Managers.UI.GetActiveUI<ConfirmUI>() as ConfirmUI;
       
        descTxt = "게임을 종료 \n 하시겠습니까?";
        
        confirmUIData.DescTxt = descTxt;
        ConfirmUIData.confirmAction = () =>
        {
           // Animator _fadeAnim = GameObject.FindWithTag("SceneManager").GetComponent<Animator>();
            //_fadeAnim.SetTrigger("doFade");
            //await Task.Delay(2000);
            Application.Quit();
        };
        if (confirmUI == null)
        {
            Managers.UI.OpenUI<ConfirmUI>(confirmUIData);
        }
    }
}
