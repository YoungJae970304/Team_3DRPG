using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDamagedState : MonsterBaseState
{
    public SlimeDamagedState(Slime slime) : base(slime) 
    {
        _slime = slime;
        _pStat = _player._playerStat;
    }
    PlayerStat _pStat;
    public override void OnStateEnter()
    {
        //넉백, 데미지 받기
        //임시로 몬스터의 데미지를 넣어놓음 추후 플레이어 데미지 값 받아오게 설정
        _slime._nav.enabled = false;
        _slime.GetComponent<Rigidbody>().isKinematic = false;
    }

    public override void OnStateExit()
    {
        //코루틴 멈추기?
        //상태는 냅둿다가 나중에 슬라임 스위치 데미지파트랑 비교하기
        _slime._nav.enabled = true;
        _slime.GetComponent<Rigidbody>().isKinematic = true;
    }

    public override void OnStateUpdate()
    {
        _slime.StartCoroutine(StartDamege(_pStat.ATK, _slime.transform.position, 0.5f, 0.5f));
    }
    public IEnumerator StartDamege(int damage, Vector3 playerPosition, float delay, float pushBack)//넉백처리 중요!
    {
        yield return new WaitForSeconds(delay);

        try//이걸 실행해보고 문제가 없다면 실행
        {

            Vector3 diff = playerPosition - _slime.transform.position;
            diff = diff / diff.sqrMagnitude;
            _slime._nav.isStopped = true;
            _slime.GetComponent<Rigidbody>().
            AddForce((_slime.transform.position - new Vector3(diff.x, diff.y, 0f)) * 50f * pushBack);

        }
        catch (MissingReferenceException e)// 문제가 있다면 에러메세지 출력
        {
            Debug.Log(e.ToString());
        }
        //예외처리문
    }
}
