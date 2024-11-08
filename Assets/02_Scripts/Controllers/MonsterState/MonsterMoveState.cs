public class MonsterMoveState : BaseState
{
    public MonsterMoveState(Player player, Monster monster, ITotalStat stat) : base(player, monster, stat)
    {
    }

    //float _timer = 0;
    public override void OnStateEnter()
    {
        _monster._nav.ResetPath();
        _monster.StopAllCoroutines();
        if (_monster._monsterID == 99999)
        {
            if ((_monster.transform.position - Managers.Game._player.transform.position).magnitude < _monster._mStat.AttackRange)
            {
                _monster.LookPlayer();
            }
            else
            {
                _monster.SetDestinationTimer(1);
            }
        }
        if (_monster._monsterID != 99999)
        {
            if (_monster._nav.enabled)
            {
                //_monster._nav.enabled = true;
                //플레이어 찾기(슬라임에서 찾아둠)
                _monster._anim.SetBool("BeforeChase", true);
                
                _monster._nav.stoppingDistance = _monster._mStat.AttackRange - 0.5f;
                _monster._nav.destination = _monster._player.transform.position;
                _monster._nav.SetDestination(_monster._nav.destination);
            }
        }
        
        
        
    }

    public override void OnStateExit()
    {
        if(_monster._monsterID != 99999)
        {
            _monster._nav.stoppingDistance = _monster._mStat.AttackRange / 2;
        }
        else
        {
            _monster._nav.stoppingDistance = _monster._mStat.AttackRange - 1;
        }
    }

    public override void OnStateUpdate()
    {

        //_monster.LookPlayer();

        //플레이어 추격
        //_timer += _monster
        if(_monster._monsterID == 99999)
        {
            if ((_monster.transform.position - Managers.Game._player.transform.position).magnitude < _monster._mStat.AttackRange)
            {
                _monster.LookPlayer();
            }
            else
            {
                _monster.SetDestinationTimer(1);
            }
        }
        
        if (_monster._nav.enabled)
        {
            _monster.SetDestinationTimer(1);
        }
        
       

    }
}
