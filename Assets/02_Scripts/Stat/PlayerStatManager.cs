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



    #region 값을 받을 수 있는 프로퍼티
    // 값을 받을 수 있는 프로퍼티들 및 각각의 액션


    public int Level { get {  return _originStat.Level; } set { _originStat.Level = value; PubAndSub.Publish<int>("Level", Level); } }

    public int HP { get { return _originStat.HP; } set { _originStat.HP = Mathf.Clamp(value, 0, MaxHP); PubAndSub.Publish<int>("HP", HP); } }
    public int MP { get { return _originStat.MP; } set { _originStat.MP = Mathf.Clamp(value, 0, MaxMP); PubAndSub.Publish<int>("MP", MP); } }
    public int EXP { get { return _originStat.EXP; } set { _originStat.EXP = value; PubAndSub.Publish<int>("EXP", EXP); } }
    public int MaxEXP { get { return _originStat.MaxEXP; } set { _originStat.MaxEXP = Mathf.Max(0, value); PubAndSub.Publish<int>("MaxEXP", MaxEXP); } }
    public int Gold { get { return _originStat.Gold; } set { _originStat.Gold = Mathf.Max(0, value); PubAndSub.Publish<int>("Gold", Gold); } }
    public int SpAddAmount { get { return _originStat.SpAddAmount; } set { _originStat.SpAddAmount = Mathf.Max(0, value); PubAndSub.Publish<int>("SpAddAmount", SpAddAmount); } }
    public int SP { get { return _originStat.SP; } set { _originStat.SP = Mathf.Max(0, value); PubAndSub.Publish<int>("SP", SP); } }
    #endregion


    #region 합산 프로퍼티 설정시 그 값만큼 버프수치가 증가
    // 읽기 전용, 합산 프로퍼티들
    //public int MaxHP { get { return (int)(Mathf.Max(0, _originStat.MaxHP + _equipStat.MaxHP + _buffStat.MaxHP)*1.1f); } set { _buffStat.MaxHP += value; } }
    public int MaxHP { get { return Mathf.Max(0, _originStat.MaxHP + _equipStat.MaxHP + _buffStat.MaxHP); } set { _buffStat.MaxHP += value; PubAndSub.Publish<int>("MaxHP", MaxHP); } }

    public int ATK { get { return Mathf.Max(0, _originStat.ATK + _equipStat.ATK + _buffStat.ATK); } set { _buffStat.ATK += value; PubAndSub.Publish<int>("ATK", ATK); } }

    public int DEF { get { return Mathf.Max(0, _originStat.DEF + _equipStat.DEF + _buffStat.DEF); } set { _buffStat.DEF += value; PubAndSub.Publish<int>("DEF", DEF); } }

    public float MoveSpeed { get { return Mathf.Max(0, _originStat.MoveSpeed + _equipStat.MoveSpeed + _buffStat.MoveSpeed); } set { _buffStat.MoveSpeed += value; PubAndSub.Publish<float>("MoveSpeed", MoveSpeed); } }

    public int RecoveryHP { get { return _originStat.RecoveryHP + _equipStat.RecoveryHP + _buffStat.RecoveryHP; } set { _buffStat.RecoveryHP += value; } }

    public int MaxMP { get { return Mathf.Max(0, _originStat.MaxMP + _equipStat.MaxMP + _buffStat.MaxMP); } set { _buffStat.MaxMP += value; PubAndSub.Publish<int>("MaxMP", MaxMP); } }

    public int RecoveryMP { get { return _originStat.RecoveryMP + _equipStat.RecoveryMP + _buffStat.RecoveryMP; } set { _buffStat.RecoveryMP += value; PubAndSub.Publish<int>("RecoveryMP", RecoveryMP); } }

    public float DodgeSpeed { get { return Mathf.Max(0, _originStat.DodgeSpeed + _equipStat.DodgeSpeed + _buffStat.DodgeSpeed); } set { _buffStat.DodgeSpeed += value; } }
    #endregion

    #region 스탯 관련 함수
    [Header("자동 회복 시간 설정")]
    public float _recoveryInterval = 5f;
    public float _lastTime = 0;

    private void Update()
    {
        if (Time.time - _lastTime >= _recoveryInterval && Time.timeScale > 0)
        {
            Logger.Log($"HP 재생 : {RecoveryHP} / MP 재생 : {RecoveryMP}");
            HP += RecoveryHP;
            MP += RecoveryMP;

            _lastTime = Time.time;
        }
    }

    public void PlayerStatUpdate()
    {
        PlayerLevelData playerLevelData = Managers.DataTable.GetPlayerLevelData(Level);
        MaxEXP = playerLevelData.MaxEXP;
        SpAddAmount = playerLevelData.SpAddAmount;
    }
    #endregion

    [ContextMenu("체력소모 테스트")]
    public void HpTest() {
        HP -= 50;
    }
}
