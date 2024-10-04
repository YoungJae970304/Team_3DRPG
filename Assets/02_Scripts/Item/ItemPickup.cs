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

    private void Awake()
    {
        _inventory = FindAnyObjectByType<Inventory>();
        if (_player == null)
        {
            Logger.LogError("플레이어가 없음");
        }
    }

    private void Start()
    {
        _player = Managers.Game._player.transform;
    }

    private void Update()
    {
         TryPickupItem();
    }

    //드랍된 아이템의 정보를 가져올 함수
    public void GetDropItemID(DeongeonLevel level)
    {
        //_itemId = Drop._drop.DropItemSelect(level);
    }

    void TryPickupItem()
    {
        //float distance = Vector3.Distance(transform.position, _player.position);
        Renderer renderer = GetComponent<Renderer>();
      
        Sequence seq = DOTween.Sequence();
        //transform.position = Vector3.MoveTowards(transform.position, _player.position, _pickupSpeed * Time.deltaTime);
        //플레이어에게 이동
        seq.Append(transform.DOMove(_player.position, _pickupDuration).SetEase(Ease.OutQuad));
        //점점 작아짐
        //seq.Join(transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InBack));
        //이동하면서 점점 하얘짐
        seq.Join(renderer.material.DOColor(Color.white, _pickupDuration));
        //시퀀스 완료 후 아이템 획득
        seq.OnComplete(() =>
        {
            Logger.Log($"{_itemId}:소환 안됨");
            // 비어있는지 확인
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
        });
    }
}