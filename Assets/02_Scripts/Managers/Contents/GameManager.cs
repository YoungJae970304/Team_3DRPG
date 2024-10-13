using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager 
{
    public Player _player;

    public List<Monster> _monsters = new List<Monster>();

    public void AddMonsterOnNowScene()
    {
        _monsters.Clear();
        _monsters.AddRange(Object.FindObjectsOfType<Monster>());
    }

    // 플레이어와의 거리에 따라 Linq 사용해 몬스터 List를 정렬
    public List<Monster> SortMonsterList()
    {
        if (_player == null)
        {
            return _monsters;
        }

        Vector3 playerPos = _player.transform.position;

        _monsters = _monsters.OrderBy(monster => (monster.transform.position - playerPos).sqrMagnitude).ToList();

        return _monsters;
    }
}
