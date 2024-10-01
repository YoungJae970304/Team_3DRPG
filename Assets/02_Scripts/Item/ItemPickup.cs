using DG.Tweening;
using UnityEngine;
public class ItemPickup : MonoBehaviour
{
    Transform _player;
    Inventory _inventory;
    Item _item;
    [SerializeField] float _pickupSpeed = 10f;
    bool _isPickup = false;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _inventory = FindAnyObjectByType<Inventory>();
        if (_player == null)
        {
            Logger.LogError("플레이어가 없음");
        }
    }
    private void Update()
    {
         TryPickupItem();
    }

    void TryPickupItem()
    {
        float distance = Vector3.Distance(transform.position, _player.position);
        Renderer renderer = GetComponent<Renderer>();

        _isPickup = true;
        Sequence seq = DOTween.Sequence();
        //transform.position = Vector3.MoveTowards(transform.position, _player.position, _pickupSpeed * Time.deltaTime);
        //플레이어에게 이동
        seq.Append(transform.DOMove(_player.position, distance / _pickupSpeed).SetEase(Ease.OutQuad));
        //점점 작아짐
        //seq.Join(transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InBack));
        //이동하면서 점점 하얘짐
        seq.Join(renderer.material.DOColor(Color.white, _pickupSpeed));
        //시퀀스 완료 후 아이템 획득
        seq.OnComplete(() =>
        {
            // itemID를 string으로 받아옴
            string itemId = Drop._drop.DropItemSelect(DeongeonLevel.Easy);
            // 비어있는지 확인
            if (!string.IsNullOrEmpty(itemId))
            {
                // string을 int로 변환
                if (int.TryParse(itemId, out int itemID))
                {
                    // id를 전달
                    _item = Item.ItemSpawn(itemID);
                    if (_item != null) // null 체크
                    {
                        if (_inventory != null)
                        {
                            _inventory.InsertItem(_item);
                            Logger.Log($"{_item.Data.Name} 인벤토리에 추가");
                        }
                        else
                        {
                            Logger.Log("인벤토리에 못넣음");
                        }
                    }
                }
            }
            Destroy(gameObject);
        });
    }
}