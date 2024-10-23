using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour, ITotalStat
{
    [HideInInspector]
    public PlayerStat _originStat;
    [HideInInspector]
    public PlayerStat _equipStat;
    [HideInInspector]
    public PlayerStat _buffStat;



    #region 값을 받을 수 있는 프로퍼티들 및 변경시 작동할 콜백
    // 값을 받을 수 있는 프로퍼티들 및 각각의 액션


    public int Level { get {  return Mathf.Max(0, _originStat.Level); } set { _originStat.Level = value; PubAndSub.Publish<int>("Level", value); } }

    public int HP { get { return Mathf.Max(0, _originStat.HP); } set { _originStat.HP = value; PubAndSub.Publish<int>("HP", value); } }
    public int MP { get { return Mathf.Max(0, _originStat.MP); } set { _originStat.MP = value; PubAndSub.Publish<int>("MP", value); } }
    public int EXP { get { return Mathf.Max(0, _originStat.EXP); } set { _originStat.EXP = value; PubAndSub.Publish<int>("EXP", value); } }
    public int MaxEXP { get { return Mathf.Max(0, _originStat.MaxEXP); } set { _originStat.MaxEXP = value; PubAndSub.Publish<int>("MaxEXP", value); } }

    public int Gold { get { return Mathf.Max(0, _originStat.Gold); } set { _originStat.Gold = value; PubAndSub.Publish<int>("Gold", value); } }
    public int SpAddAmount { get { return Mathf.Max(0, _originStat.SpAddAmount); } set { _originStat.SpAddAmount = value; PubAndSub.Publish<int>("SpAddAmount", value); } }
    public int SP { get { return Mathf.Max(0, _originStat.SP); } set { _originStat.SP = value; PubAndSub.Publish<int>("SP", value); } }
    #endregion


    #region 합산 프로퍼티 설정시 그 값만큼 버프수치가 증가
    // 읽기 전용, 합산 프로퍼티들
    public int MaxHP { get { return (int)(Mathf.Max(0, _originStat.MaxHP + _equipStat.MaxHP + _buffStat.MaxHP)*1.1f); } set { _buffStat.MaxHP += value; } }

    public int ATK { get { return Mathf.Max(0, _originStat.ATK + _equipStat.ATK + _buffStat.ATK); } set { _buffStat.ATK += value; } }

    public int DEF { get { return Mathf.Max(0, _originStat.DEF + _equipStat.DEF + _buffStat.DEF); } set { _buffStat.DEF += value; } }

    public float MoveSpeed { get { return Mathf.Max(0, _originStat.MoveSpeed + _equipStat.MoveSpeed + _buffStat.MoveSpeed); } set { _buffStat.MoveSpeed += value; } }

    public int RecoveryHP { get { return Mathf.Max(0, _originStat.RecoveryHP + _equipStat.RecoveryHP + _buffStat.RecoveryHP); } set { _buffStat.RecoveryHP += value; } }

    public int MaxMP { get { return Mathf.Max(0, _originStat.MaxMP + _equipStat.MaxMP + _buffStat.MaxMP); } set { _buffStat.MaxMP += value; } }

    public int RecoveryMP { get { return Mathf.Max(0, _originStat.RecoveryMP + _equipStat.RecoveryMP + _buffStat.RecoveryMP); } set { _buffStat.RecoveryMP += value; } }

    public float DodgeSpeed { get { return Mathf.Max(0, _originStat.DodgeSpeed + _equipStat.DodgeSpeed + _buffStat.DodgeSpeed); } set { _buffStat.DodgeSpeed += value; } }
    #endregion

    public void PlayerStatUpdate()
    {
        PlayerLevelData playerLevelData = Managers.DataTable.GetPlayerLevelData(Level);
        MaxEXP = playerLevelData.MaxEXP;
        SpAddAmount = playerLevelData.SpAddAmount;
    }
}
