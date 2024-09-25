using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterBaseState
{
    protected Monster _monster;
    protected Slime _slime;
    protected Ork _ork;
    protected Goblem _goblem;
    protected BossBear _bossBear;
    protected MonsterBaseState(Monster monster)
    {
        _monster = monster;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();

}
