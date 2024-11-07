using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtkCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            if (Managers.Game._player._hitMobs.Contains(other)) return;
            Managers.Game._player._hitMobs.Add(other);
        }
    }

    private void OnDisable()
    {
        Managers.Game._player._hitMobs.Clear();
    }
}
