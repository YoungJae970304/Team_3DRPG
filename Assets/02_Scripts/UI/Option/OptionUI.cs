using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
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
        Get<Button>((int)SelectButtons.GiveUp).interactable = false; ;
        CheckInDungeon();
    }
    public void CheckInDungeon()
    {
        if (Managers.Scene.DungeonSceneCheck())
        {
            Get<Button>((int)SelectButtons.GiveUp).interactable = true;
        }
    }
    public void OnClickOptionBtn()
    {
        VolumeUI volumeUI = Managers.UI.GetActiveUI<VolumeUI>() as VolumeUI;
        VolumeUIData volumeUIData = new VolumeUIData();
        if(volumeUI == null)
        {
            Managers.UI.OpenUI<VolumeUI>(volumeUIData);
        }
    }
    public void OnClickGiveUpBtn()
    {
        ConfirmUI confirmUI = Managers.UI.GetActiveUI<ConfirmUI>() as ConfirmUI;
        
        descTxt = "던전을 포기 \n 하시겠습니까?";
       
        confirmUIData.DescTxt = descTxt;

        ConfirmUIData.confirmAction = () =>
        {
           for(int i = 0; i < Managers.Game._monsters.Count; i++)
            {
                Managers.Resource.Destroy(Managers.Game._monsters[i].gameObject);
            }
            
            Managers.Scene.SceneChange("main");
            Get<Button>((int)SelectButtons.GiveUp).interactable = false;
            CloseUI();
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
            CloseUI();
            Application.Quit();
        };
        if (confirmUI == null)
        {
            Managers.UI.OpenUI<ConfirmUI>(confirmUIData);
        }
    }

}
