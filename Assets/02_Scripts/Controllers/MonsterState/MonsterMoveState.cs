public class MonsterMoveState : BaseState
{
    public MonsterMoveState(Player player, Monster monster, ITotalStat stat) : base(player, monster, stat)
    {
    }

    float _timer = 0;
    public override void OnStateEnter()
    {
        Logger.LogError("3");
        _monster.StopAllCoroutines();
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

    public override void OnStateExit()
    {
        _monster._nav.stoppingDistance = 0;
    }

    public override void OnStateUpdate()
    {

        //_monster.LookPlayer();

        //플레이어 추격
        //_timer += _monster
        if (_monster._nav.enabled)
        {
            _monster.SetDestinationTimer(1);
        }
        
       

    }
}
