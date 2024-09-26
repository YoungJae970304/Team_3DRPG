using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, IDamageAlbe
{
    private enum State
    {
        Idle,
        Move,
        Attack,
        Skill,
        Damage,
        Return,
        Die,
    }
    private State _state;
    public GameObject _player;
    public NavMeshAgent _nav;
    public MonsterStat _mStat;
    public GameObject[] _weapon, _armor, _accesary, _product;
    public int[] _probability;
    public int _wR, _aR, _acR, _pR;
    public int star;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        _state = State.Idle;
        #region 확률변수 초기화
        _probability[0] = _wR;
        _probability[1] = _aR;
        _probability[2] = _acR;
        _probability[3] = _pR;
        #endregion
        #region 딕셔너리 초기화

        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BaseState()
    {
        switch (_state)
        {
            case State.Idle:
                break;
            case State.Move:
                break;
            case State.Attack:
                break;
            case State.Skill:
                break;
            case State.Damage:
                break;
            case State.Return:
                break;
            case State.Die:
                break;
        }
    }

    public virtual void Damaged(int amount)
    {

    }

    public virtual void DropItem(string level, Transform mTransform, GameObject[] itemMenu) 
    {
        
    }

}
