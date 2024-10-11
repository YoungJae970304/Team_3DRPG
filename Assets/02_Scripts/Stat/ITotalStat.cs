using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITotalStat 
{
    public int HP { get; set; }

    public int MaxHP { get; }

    public int ATK { get; }

    public int DEF { get; }

    public float MoveSpeed { get; }

    public int RecoveryHP { get; }

    public int MP { get; set; }

    public int MaxMP { get; }

    public int RecoveryMP { get; }

    public int EXP { get; set; }
    public int Gold { get; set; }
}
