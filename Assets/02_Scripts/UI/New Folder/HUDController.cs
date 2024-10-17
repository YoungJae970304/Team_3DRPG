using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUDController : BaseUI
{
    enum HUDButtons
    {
        Quest,
        Inventory,
        Skill,
        Option,
    }
    private void Awake()
    {
        Bind<Button>(typeof(HUDButtons));
        Bind<Button>(typeof(HUDButtons));
        GetButton((int)HUDButtons.Quest).onClick.AddListener(QuestLogOpenBtn);
        GetButton((int)HUDButtons.Inventory).onClick.AddListener(InventoryOpenBtn);
    }


    void QuestLogOpenBtn()
    {
        QuestLogUI questLog = Managers.UI.GetActiveUI<QuestLogUI>() as QuestLogUI;

        if (questLog != null)
        {
            Managers.UI.CloseUI(questLog);
        }
        else
        {
            Managers.UI.OpenUI<QuestLogUI>(new BaseUIData());
        }
    }

    void InventoryOpenBtn()
    {
        InventoryUI inventory = Managers.UI.GetActiveUI<InventoryUI>() as InventoryUI;
        if (inventory != null)
        {
            Managers.UI.CloseUI(inventory);
        }
        else
        {
            Managers.UI.OpenUI<InventoryUI>(new BaseUIData());
        }
    }
}
