using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;

public class SelectPlayerUI : BaseUI
{   [Multiline(5)]
    [SerializeField] string descTxt;
    enum SelectObjects
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
        Bind<GameObject>(typeof(SelectObjects));
        Bind<Button>(typeof(SelectButtons));

        //Managers.Input.MouseAction -= PlayerSelectRay;
        //Managers.Input.MouseAction += PlayerSelectRay;
    }

    private void Update()
    {
        PlayerSelectRay();
    }

    // 선택되어 있는 토글에 따라 플레이어 타입을 결정 -> 이후 게임매니저에서 씬 로드될 때 마다 해당 플레이어 생성
    public void OnClickStartBtn()
    {
        //ConfirmUI confirmUI = Managers.UI.GetActiveUI<ConfirmUI>() as ConfirmUI;
        

        //confirmUIData.DescTxt = descTxt;// "게임 진입 후 캐릭터의 변경이 불가능 합니다!\r\n선택한 캐릭터로 진행 하시겠습니까?";
        //ConfirmUIData.confirmAction += () => {
        //    Animator fadeAnim= GameObject.FindWithTag("SceneManager").GetComponent<Animator>();
        //    fadeAnim.SetTrigger("doFade");
        //};

        //if (confirmUI == null)
        //{
        //    Managers.UI.OpenUI<ConfirmUI>(confirmUIData);
        //}

        //Toggle melee = GetToggle((int)SelectToggles.MeleePlayer);
        //Toggle mage = GetToggle((int)SelectToggles.MagePlayer);

        //if (melee.isOn)
        //{
        //    Managers.Game._playerType = Define.PlayerType.Melee;
        //}
        //else if (mage.isOn)
        //{
        //    Managers.Game._playerType = Define.PlayerType.Mage;
        //}
    }

    public void PlayerSelectRay()
    {
        Logger.LogWarning("클릭 확인");

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.NameToLayer("Player")))
            {
                Logger.LogWarning("진입 확인");

                ConfirmUI confirmUI = Managers.UI.GetActiveUI<ConfirmUI>() as ConfirmUI;

                confirmUIData.DescTxt = descTxt;// "게임 진입 후 캐릭터의 변경이 불가능 합니다!\r\n선택한 캐릭터로 진행 하시겠습니까?";
                ConfirmUIData.confirmAction += () => {
                    Animator fadeAnim = GameObject.FindWithTag("SceneManager").GetComponent<Animator>();
                    fadeAnim.SetTrigger("doFade");
                };

                if (confirmUI == null)
                {
                    Managers.UI.OpenUI<ConfirmUI>(confirmUIData);
                }

                GameObject melee = GetGameObject((int)SelectObjects.MeleePlayer);
                GameObject mage = GetGameObject((int)SelectObjects.MagePlayer);

                if (hit.collider.gameObject == melee)
                {
                    Logger.LogWarning("근거리 캐릭터 선택 확인");
                }
                else if (hit.collider.gameObject == mage)
                {
                    Logger.LogWarning("원거리 캐릭터 선택 확인");
                }
            }

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 10000f, Color.red);
        }
    }

    //private void OnDisable()
    //{
    //    Managers.Input.MouseAction -= PlayerSelectRay;
    //}
}
   
