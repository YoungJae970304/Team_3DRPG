using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBearDamagedState : MonsterBaseState
{
    //생각해보니 보스이거 안밀리잖아 싹다 바꿔야하네
    public BossBearDamagedState(BossBear bossBear) : base(bossBear)
    {
        _bossBear = bossBear;
        _player = _bossBear._player.GetComponent<Player>();
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
