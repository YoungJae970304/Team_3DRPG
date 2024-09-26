using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

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
    public enum MonsterType
    {
        Slime,
        Goblem,
        Ork,
    }
    public enum StageName
    {
        Easy,
        Normal,
        Hard,
    }
    private State _state;
    private MonsterType _mType;
    public GameObject _player;
    public NavMeshAgent _nav;
    public MonsterStat _mStat;
    public GameObject[] _weapon, _armor, _accesary, _product;
    public Dictionary<int,int> _probability = new Dictionary<int, int>();
    public Dictionary<string,MonsterType> _productItem = new Dictionary<string,MonsterType>();
    public int _randomValue = 0;
    public int _wR, _aR, _acR, _pR;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        _state = State.Idle;
        #region 확률변수 초기화
        _probability.Add(0,_wR);
        _probability.Add(1,_aR);
        _probability.Add(2,_acR);
        _probability.Add(3,_pR);
        #endregion
        #region 딕셔너리 초기화
        _productItem.Add("Slime", MonsterType.Slime);
        _productItem.Add("Goblem", MonsterType.Goblem);
        _productItem.Add("Ork", MonsterType.Ork);
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

    public IEnumerator DropItem(StageName level, Transform mTransform, GameObject[] itemMenu) 
    {
        //게임매니저에서 생성된 아이템을 pooling해야하는데 여기서는 아이템 키면서 가져와서 값만 넣어주면될듯
        DropProbability(); //가중치 랜덤

        level = StageName.Hard;
        if (level == StageName.Hard)
        {
            for (int i = 0; i < 4; i++)
            {
                itemMenu = itemtype(_probability[i]);
                if (_probability[i] == _pR)
                {
                    string mobName = mTransform.gameObject.name;
                    _productItem.TryGetValue(mobName, out MonsterType monstertype);
                    int productItem = (int)monstertype;
                    if (_randomValue <= 100) // 일단 100프로로 설정해야되니까
                    {
                        Instantiate(itemMenu[productItem], mTransform.position, itemMenu[productItem].transform.rotation);
                    }
                    yield return null;
                }
                if (_randomValue <= 70)
                {
                    Instantiate(itemMenu[0], mTransform.position, itemMenu[0].transform.rotation);
                }
                else if (_randomValue <= 90)
                {
                    Instantiate(itemMenu[1], mTransform.position, itemMenu[1].transform.rotation);
                }
                else
                {
                    Instantiate(itemMenu[2], mTransform.position, itemMenu[2].transform.rotation);
                }
            }
        }
        else if (level == StageName.Normal)
        {
            for (int i = 0; i < 4; i++)
            {
                itemMenu = itemtype(_probability[i]);
                if (_probability[i] == _pR)
                {
                    string mobName = mTransform.gameObject.name;
                    _productItem.TryGetValue(mobName, out MonsterType monstertype);
                    int productItem = (int)monstertype;
                    if (_randomValue <= 100) // 일단 100프로로 설정해야되니까
                    {
                        Instantiate(itemMenu[productItem], mTransform.position, itemMenu[productItem].transform.rotation);
                    }
                    yield return null;
                }
                if (_randomValue <= 80)
                {
                    Instantiate(itemMenu[0], mTransform.position, itemMenu[0].transform.rotation);
                }
                else if (_randomValue <= 20)
                {
                    Instantiate(itemMenu[1], mTransform.position, itemMenu[1].transform.rotation);
                }
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                itemMenu = itemtype(_probability[i]);
                if (_probability[i] == _pR)
                {
                    string mobName = mTransform.gameObject.name;
                    _productItem.TryGetValue(mobName, out MonsterType monstertype);
                    int productItem = (int)monstertype;
                    if (_randomValue <= 100) // 일단 100프로로 설정해야되니까
                    {
                        Instantiate(itemMenu[productItem], mTransform.position, itemMenu[productItem].transform.rotation);
                    }
                    yield return null;
                }
                if (_randomValue <= 100)
                {
                    Instantiate(itemMenu[0], mTransform.position, itemMenu[0].transform.rotation);
                }
            }
        }
        yield return new WaitForSeconds(1);

        GameObject mob = mTransform.gameObject;
        Die(mob);
    }
    public virtual void Die(GameObject mob)
    {
        
    }
    public GameObject[] itemtype(int type)
    {
        if (type == _wR)
        {
            return _weapon;
        }
        else if (type == _aR)
        {
            return _armor;
        }
        else if (type == _acR)
        {
            return _accesary;
        }
        else
        {
            return _product;
        }
    }
    
    public void DropProbability()
    {
        for (int i = 0; i <= 3; i++)
        {
            _randomValue = UnityEngine.Random.Range(0, 100);
        }
    }
}
