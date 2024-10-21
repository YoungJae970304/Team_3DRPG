using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DungeonManager : MonoBehaviour
{
    public static int _monsterCount; //던전 클리어 조건을 위한 변수
    //시작할때 던전에서 몬스터가 생성될때 더해지고
    //죽을 때 감소하는 변수 (0이되면 클리어가됨) //
    //-- 바로클리어를 막기위해 bool변수 추가해주면좋을듯
  
    public bool _startCheck = false;
    public void Init()
    {
        //Managers.Resource.Instantiate("Enemy/Slime");
    }
    private void Update()
    {
        if(_startCheck && _monsterCount <= 0)
        {
            //클리어 UI활성화
            //버튼 클릭시 홈화면 // 확정
          
        }
    }
}
