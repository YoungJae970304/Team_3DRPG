using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State { }

public class IdleState : MonsterBaseState
{
    public IdleState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        
    }

    public override void OnStateExit()
    {
      
    }

    public override void OnStateUpdate()
    {
        
    }
}
public class MoveState : MonsterBaseState
{
    public MoveState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        
    }
}

public class AttackState : MonsterBaseState
{
    public AttackState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
       
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        
    }
}

public class SkillState : MonsterBaseState
{
    public SkillState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        
    }

    public override void OnStateExit()
    {
       
    }

    public override void OnStateUpdate()
    {
       
    }
}

public class DamagedState : MonsterBaseState
{
    public DamagedState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
       
    }
}

public class DieState : MonsterBaseState
{
    public DieState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        
    }
}

public class ReturnState : MonsterBaseState
{
    public ReturnState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        
    }
}