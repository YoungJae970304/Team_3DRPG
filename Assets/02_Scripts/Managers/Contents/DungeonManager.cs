using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class DungeonManager : MonoBehaviour
{
    public static int _monsterCount; //던전 클리어 조건을 위한 변수
    //시작할때 던전에서 몬스터가 생성될때 더해지고
    //죽을 때 감소하는 변수 (0이되면 클리어가됨) //
    //-- 바로클리어를 막기위해 bool변수 추가해주면좋을듯
    public bool _startCheck = false;
    
    private void Start()
    {
        
    }
    private void Update()
    {
        ClearDungeon();
    }

    public void ClearDungeon()
    {
        if (_monsterCount <= 0 && _startCheck == true)
        {
            //던전 UI활성화
            InDungeonUI inDungeonUI = Managers.UI.GetActiveUI<InDungeonUI>() as InDungeonUI;
            if(inDungeonUI != null)
            {
                Managers.UI.CloseUI(inDungeonUI);
            }
            else
            {
                Managers.UI.OpenUI<InDungeonUI>(new BaseUIData());
            }
            _startCheck = false;
        }
    }
    public void CountPlus()
    {
        _monsterCount++;
        Logger.LogError($"{_monsterCount.ToString()}일단 더한숫자확인");
    }
    public void CountMinus()
    {
        _monsterCount--;
        Logger.LogError($"{_monsterCount.ToString()}일단 뺀숫자확인");
    }
    public void DecideMonster(int ID)
    {

    }
}
