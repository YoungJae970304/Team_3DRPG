using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : ITotalStat
{
    public PlayerStat _originStat;
    public PlayerStat _equipStat;
    public PlayerStat _buffStat;

    #region 값을 받을 수 있는 프로퍼티들
    // 값을 받을 수 있는 프로퍼티들
    public int Level { get {  return Mathf.Max(0, _originStat.Level); } set { _originStat.Level = value; } }

    public int HP { get { return Mathf.Max(0, _originStat.HP); } set { _originStat.HP = value; } }

    public int MP { get { return Mathf.Max(0, _originStat.MP); } set { _originStat.MP = value; } }

    public int EXP { get { return Mathf.Max(0, _originStat.EXP); } set { _originStat.EXP = value; } }

    public int MaxEXP { get { return Mathf.Max(0, _originStat.MaxEXP); } set { _originStat.MaxEXP = value; } }

    public int Gold { get { return Mathf.Max(0, _originStat.Gold); } set { _originStat.Gold = value; } }

    public int SpAddAmount { get { return Mathf.Max(0, _originStat.SpAddAmount); } set { _originStat.SpAddAmount = value; } }

    public int SP { get { return Mathf.Max(0, _originStat.SP); } set { _originStat.SP = value; } }
    #endregion


    #region 읽기 전용, 합산 프로퍼티들
    // 읽기 전용, 합산 프로퍼티들
    public int MaxHP { get { return Mathf.Max(0, _originStat.MaxHP + _equipStat.MaxHP + _buffStat.MaxHP); } }

    public int ATK { get { return Mathf.Max(0, _originStat.ATK + _equipStat.ATK + _buffStat.ATK); } }

    public int DEF { get { return Mathf.Max(0, _originStat.DEF + _equipStat.DEF + _buffStat.DEF); } }

    public float MoveSpeed { get { return Mathf.Max(0, _originStat.MoveSpeed + _equipStat.MoveSpeed + _buffStat.MoveSpeed); } }

    public int RecoveryHP { get { return Mathf.Max(0, _originStat.RecoveryHP + _equipStat.RecoveryHP + _buffStat.RecoveryHP); } }

    public int MaxMP { get { return Mathf.Max(0, _originStat.MaxMP + _equipStat.MaxMP + _buffStat.MaxMP); } }

    public int RecoveryMP { get { return Mathf.Max(0, _originStat.RecoveryMP + _equipStat.RecoveryMP + _buffStat.RecoveryMP); } }

    public float DodgeSpeed { get { return Mathf.Max(0, _originStat.DodgeSpeed + _equipStat.DodgeSpeed + _buffStat.DodgeSpeed); } }
    #endregion

    public void PlayerStatUpdate()
    {
        PlayerLevelData playerLevelData = Managers.DataTable.GetPlayerLevelData(Level);
        MaxEXP = playerLevelData.MaxEXP;
        SpAddAmount = playerLevelData.SpAddAmount;
    }
}
