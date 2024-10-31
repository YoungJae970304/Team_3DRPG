using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class MonsterDieState : BaseState
{
    public MonsterDieState(Player player, Monster monster, ITotalStat stat) : base(player, monster, stat)
    {
    }

    public override void OnStateEnter()
    {
        _monster._nav.enabled = false;
        _monster._player.PlayerEXPGain(_monster._mStat._mStat.EXP);
        Logger.LogError($"{_monster._mStat.EXP.ToString()}");
        _monster._anim.SetTrigger("Die");
        Managers.Game._monsters.Remove(_monster);
        Logger.Log("몬스터 사망");
        //_monster.GetComponent<BoxCollider>().enabled = false;
        _monster._monsterDrop.DropItemSelect(_monster._deongeonLevel, _monster.sample);//임시 설정 추후 던전에서 받아오도록 변경
        _monster.MakeItem();
        _monster._dieMonster?.Invoke();
        QuestCheck();
        //_monster.StartCoroutine(IvokeDie());
        Action invokeDie = async () =>
        {
            await Task.Delay(2000);
            
            if(_monster.gameObject != null)
            {
                GameObject mob = _monster.gameObject;
                _monster._nav.enabled = true;
                _monster._anim.enabled = true;
                _monster.Die(mob);
            }
            
        };
        invokeDie.Invoke();
        // 영재 : 임시로 죽었을 때 게임매니저에서 제거하는 부분 추가
        //Managers.Game._monsters.Remove(_monster);
    }
    public void QuestCheck()
    {
        for(int i = 0; i < Managers.QuestManager._progressQuest.Count; i++)
        {
            if(Managers.QuestManager._targetCheck[Managers.QuestManager._progressQuest[i]] == _monster._monsterID)
            {
                PubAndSub.Publish<int>($"{Managers.QuestManager._progressQuest[i]}", Managers.QuestManager._progressQuest[i]);
            }
        }
    }
    public IEnumerator IvokeDie()
    {
        
        yield return new WaitForSeconds(2);
        GameObject mob = _monster.gameObject;
        _monster.Die(mob);
    }
    public override void OnStateExit()
    {
       
    }

    public override void OnStateUpdate()
    {
        
    }
}
