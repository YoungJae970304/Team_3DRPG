using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Jobs;

public class Slime : Monster, IDamageAlbe
{
    public int _slimeID;
    public int _slimeProduct = 0;
    
    public override async void StartDamege(Vector3 playerPosition, float delay, float pushBack)
    {
        _nav.enabled = false;
        // 넉백 방향 계산
        Vector3 diff = (transform.position - playerPosition).normalized; // 플레이어 반대 방향
        Vector3 force = diff * pushBack; // 넉백 힘

        // Rigidbody 설정
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false; // 물리 효과 활성화


        // 넉백 힘 적용
        rb.AddForce(force, ForceMode.Impulse);

        // 넉백 후 처리
        await Task.Delay((int)(delay * 1000)); // 넉백 지속 시간 (필요에 따라 조정)

        // 넉백이 끝나면 NavMeshAgent를 다시 활성화


        _nav.enabled = true;
        rb.isKinematic = true; // 다시 비활성화 (필요시)
        if (CanAttackPlayer())
            MChangeState(MonsterState.Attack);
        else
            MChangeState(MonsterState.Move);
    }

    protected override void BaseState()
    {
        switch (_curState)
        {
            case MonsterState.Idle:
                break;
            case MonsterState.Damage:
                if (CanAttackPlayer())
                    MChangeState(MonsterState.Attack);
                else if (_mStat.HP <= 0)
                    MChangeState(MonsterState.Die);
                else
                    MChangeState(MonsterState.Move);
                break;
            case MonsterState.Move:
                if (CanAttackPlayer())
                    MChangeState(MonsterState.Attack);
                else if (ReturnOrigin())
                    MChangeState(MonsterState.Return);
                break;
            case MonsterState.Attack:
                if (!CanAttackPlayer())
                {
                    if (!ReturnOrigin())
                    {
                        MChangeState(MonsterState.Move);
                    }
                    else
                    {
                        MChangeState(MonsterState.Return);
                    }
                }
                break;
            case MonsterState.Return:
                if ((_originPos - transform.position).magnitude <= 3f)
                    MChangeState(MonsterState.Idle);
                break;
            case MonsterState.Die:
                break;


        }
    }
    public override void itemtest(DeongeonLevel curGrade)
    {
        base.itemtest(curGrade);

        foreach(var slimeDrop in _dropManager._MonsterDropData)
        {
            List<int> slimeID = new List<int>();
            List<int> valueExp = new List<int>();
            List<int> slimeRandomStartGold = new List<int>();
            List<int> slimeRandomEndGold = new List<int>();
            slimeRandomEndGold.Add(slimeDrop.EndValue4);
            slimeRandomStartGold.Add(slimeDrop.StartValue4);
            valueExp.Add(slimeDrop.Value5);
            slimeID.Add(slimeDrop.ID);
            if(_slimeID.ToString("D0") == slimeDrop.Value6.ToString("D0") && _slimeProduct == 0)
            {
                _slimeProduct = slimeDrop.Value6;
                _mStat.EXP = valueExp[0];
            }
            
            switch (_slimeID.ToString("F1"))
            {
                case "1":
                    _mStat.Gold = UnityEngine.Random.Range(slimeRandomStartGold[0], slimeRandomEndGold[0]);
                    break;
                case "2":
                    _mStat.Gold = UnityEngine.Random.Range(slimeRandomStartGold[4], slimeRandomEndGold[4]);
                    break;
                case "3":
                    _mStat.Gold = UnityEngine.Random.Range(slimeRandomStartGold[7], slimeRandomEndGold[7]);
                    break;
            }
        }
        
        

    }
    public void SlimeIDCheck(DeongeonLevel curLevel)
    {
        foreach(var sID in _dropManager._MonsterDropData)
        {
            string iDCheck = sID.ID.ToString("D0");
            if (iDCheck == "1")
            {
                switch (curLevel)
                {
                    case DeongeonLevel.Easy:
                        if(sID.ID.ToString("F1") == "1")
                        {
                            _slimeID = sID.ID;
                        }
                        break;
                    case DeongeonLevel.Normal:
                        if (sID.ID.ToString("F1") == "2")
                        {
                            _slimeID = sID.ID;
                        }
                        break;
                    case DeongeonLevel.Hard:
                        if (sID.ID.ToString("F1") == "3")
                        {
                            _slimeID = sID.ID;
                        }
                        break;
                }
            }
        }
    }
}
