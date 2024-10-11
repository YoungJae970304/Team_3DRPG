using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITotalStat 
{
    public int HP { get; set; }

    public int MaxHP { get; set; }

    public int ATK { get; set; }

    public int DEF { get; set; }

    public float MoveSpeed { get; set; }

    public int RecoveryHP { get; set; }

    public int MP { get; set; }

    public int MaxMP { get; set; }

    public int RecoveryMP { get; set; }
}
