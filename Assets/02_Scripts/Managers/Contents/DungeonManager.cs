using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager
{
    public Player _player;
    public GameObject StartPosition;
    public static int _monsterCount;
    public void Init()
    {
        _player = Managers.Game._player;

        _player.transform.position = StartPosition.transform.position;
    }
  
}
