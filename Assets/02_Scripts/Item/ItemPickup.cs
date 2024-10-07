using DG.Tweening;
using UnityEngine;
public class ItemPickup : MonoBehaviour
{
    Transform _player;
    Inventory _inventory;
    public Item _newItem;
    // itemID를 string으로 받아옴
    public string _itemId;
    [SerializeField] float _pickupDuration = 1f;
    Tweener _tweener;
    private void Awake()
    {
        _inventory = FindAnyObjectByType<Inventory>();
    }

    private void Start()
    {
        _player = Managers.Game._player.transform;
        TryPickupItemEffect();
        if (_player == null)
        {
            Logger.LogError("플레이어가 없음");
        }
    }

    void TryPickupItemEffect()
    {
        //float distance = Vector3.Distance(transform.position, _player.position);
        Renderer renderer = GetComponent<Renderer>();

        Sequence seq = DOTween.Sequence();
        //transform.position = Vector3.MoveTowards(transform.position, _player.position, _pickupSpeed * Time.deltaTime);
        //플레이어에게 이동
        seq.Append(transform.DOMove(_player.position, _pickupDuration).SetEase(Ease.OutQuad));
        //점점 작아짐
        seq.Join(transform.DOScale(Vector3.zero, _pickupDuration - 0.1f).SetEase(Ease.InBack));
        //이동하면서 점점 하얘짐
        seq.Join(renderer.material.DOColor(Color.white, _pickupDuration));
        //근데 시퀀스가 종료되기전에 플레이어가 움직이면 플레이어를 향해서 다시 경로 재탐색 
        if (Vector3.Distance(transform.position, _player.position) < 0.1f)
        {
            //연출 효과 종료하고 경로 재 탐색
            //Kill = Complete를 펄스로 해버림
            _tweener.Kill();
            FollowPlayer();
            //시퀀스 종료.
            seq.Complete();
        }

        void FollowPlayer()
        {
            transform.position = Vector3.MoveTowards(transform.position, _player.position, 10f * Time.deltaTime);
            PickupItem();
        }

        void PickupItem()
        {

            if (!string.IsNullOrEmpty(_itemId))
            {
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
                            _inventory.InsertItem(_newItem);
                            Logger.Log($"{_newItem.Data.ID} 인벤토리에 추가");
                        }
                        else
                        {
                            Logger.Log("인벤토리에 못넣음");
                        }
                    }
                }
            }
            Destroy(gameObject);
        }
    }
}