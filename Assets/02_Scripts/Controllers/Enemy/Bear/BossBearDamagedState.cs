using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBearDamagedState : MonsterBaseState
{
    //�����غ��� �����̰� �ȹи��ݾ� �ϴ� �ٲ���ϳ�
    public BossBearDamagedState(BossBear bossBear) : base(bossBear)
    {
        _bossBear = bossBear; 
        _pStat = _player._playerStat;
    }
    PlayerStat _pStat;
    public override void OnStateEnter()
    {
        
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
       
    }
    
}
