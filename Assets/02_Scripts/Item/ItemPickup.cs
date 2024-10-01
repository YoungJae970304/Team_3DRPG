using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    Transform _player;

    [SerializeField] float _pickupSpeed;
    ResourceManager _resourceManager;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void TryPickupItem()
    {
        //이동은 됨
        float distance = Vector3.Distance(transform.position, _player.position);
        transform.position = Vector3.MoveTowards(transform.position, _player.position, _pickupSpeed * Time.deltaTime);

        if(distance < 0.1f)
        {
            _resourceManager.Destroy(gameObject);
        }
    }
}
