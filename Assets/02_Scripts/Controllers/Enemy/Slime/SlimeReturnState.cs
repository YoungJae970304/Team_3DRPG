using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

public class SlimeReturnState : MonsterBaseState
{
    public SlimeReturnState(Slime slime) : base(slime) 
    {
        _slime = slime;
        _sStat = _slime._sStat;
    }
    SlimeStat _sStat;
    public override void OnStateEnter()
    {
        //origin포스 찾아서 이동하기
        _slime._nav.destination = _slime._originPos;
    }
    public override void OnStateExit()
    {
        //originPos라면 Idle로 상태 변환
        //아니라면 플레이어 추격(Move로 상태 변환) // 이조건은 필요없는듯 Idle로만 변하면됨
        //얘도 조건확인하기
        //뭐 넣을게 없어서 일단 비워두기
        //아마 애니메이션 종료 들어갈듯
    }

    public override void OnStateUpdate()
    {
        //이걸 좀 고민해야할듯
        //리턴을 계속 진행하는게 맞다하니 지속적으로 체력회복 + 리턴장소까지 계속 복귀 // 
        //이러면 데미지 함수를 호출할 때 return상태이면 스테이트 변환을 안하게 조건걸어야됨 // 조건 걸려있음
        _slime._nav.SetDestination(_slime._originPos);
        //_slime.ReturnHeal(); //구현은 되어있으나 스텟이 없어서 작동이 안됨
    }
    public void ReturnHeal()
    {
        _sStat.Hp = _sStat.MaxHp;
    }
}
