using DG.Tweening;
using UnityEngine;
public class ItemPickup : MonoBehaviour
{
    Transform _player;
    Inventory _inventory;

    public Item _newItem;
    public string _itemId;
    [SerializeField] float _pickupDuration = 1f;
    bool _isPickup = false;
    Sequence _seq;
    Tweener _tweener;
    private void Awake()
    {
    }

    private void Start()
    {
        _player = Managers.Game._player.transform;
        _inventory = _player.gameObject.GetOrAddComponent<Inventory>();
        PickupItemEffect();
        if (_player == null)
        {
            Logger.LogError("플레이어가 없음");
        }
    }

    void PickupItemEffect()
    {
        //float distance = Vector3.Distance(transform.position, _player.position);
        Renderer renderer = GetComponent<Renderer>();
        _seq = DOTween.Sequence();

        //플레이어에게 이동
        //점점 작아짐
        //이동하면서 점점 하얘짐
        _seq.Append(transform.DOMove(_player.position, _pickupDuration).SetEase(Ease.OutQuad))
            .Join(transform.DOScale(Vector3.zero, _pickupDuration - 0.1f).SetEase(Ease.InBack))
            .Join(renderer.material.DOColor(Color.white, _pickupDuration))
            .OnUpdate(() =>
            {
                if(_isPickup == true)
                {
                    if (Vector3.Distance(transform.position, _player.position) > 0.1f)
                    {
                        _tweener.Kill();
                        FollowPlayer();
                    }
                }
                else
                {
                    PickupItem();
                    _seq.Complete();
                    Destroy(gameObject);
                }
            });

        void FollowPlayer()
        {
            if (_isPickup == true) { return; }

            _seq.Append(transform.DOMove(_player.position, _pickupDuration).SetEase(Ease.OutQuad))
            .Join(transform.DOScale(Vector3.zero, _pickupDuration - 0.1f).SetEase(Ease.InBack))
            .Join(renderer.material.DOColor(Color.white, _pickupDuration));
        }

        void PickupItem()
        {
            if (!string.IsNullOrEmpty(_itemId))
            {
                _tweener.Kill();
                // string을 int로 변환
                if (int.TryParse(_itemId, out int itemID))
                {
                    // id를 전달
                    _newItem = Item.ItemSpawn(itemID);
                    if (_newItem != null) // null 체크
                    {
                        Logger.Log("아이템 생성");
                        if (_inventory != null)
                        {
                            _isPickup = true;
                            _inventory.InsertItem(_newItem);
                            Managers.Data.SaveData<InventorySaveData>();
                            Logger.Log("인벤토리 저장 확인");
                            Logger.Log($"{_newItem.Data.Name} 인벤토리에 추가");
                        }
                        else
                        {
                            Logger.Log("인벤토리에 못넣음");
                        }
                    }
                }
            }
        }
    }
}