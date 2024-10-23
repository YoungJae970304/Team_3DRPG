using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemConfirmData : BaseUIData
{
    public bool isBuy;                  //구매인지 판매인지 확인
    public Item Item;                   //구매or판매되는 아이템
    public ItemSlot ShopSlot;           //상점 슬롯
    public InventorySlot InventorySlot; //인벤토리 슬롯
}


public class ItemConfirm : ItemDragUI
{// 오브젝트 바인드
    #region bind
    enum Buttons
    {
        ConfirmButton,
        CancelButton
    }
    enum Texts
    {
        ItmeName,
        ItemAmountTxt,
        ConfirmButtonTxt,
        CancelButtonTxt,
        MoneyAmountTxt
    }

    enum Sliders
    {
        ItemAmount,
    }
    enum itemSlots
    {
        ItemSlot,
    }
    #endregion
    public ItemSlot _shopSlot;                  //상점 슬롯
    public InventorySlot _inventorySlot;        //인벤토리 슬롯
    bool isBuy;                                 //구매인지 판매인지
    public override void Init(Transform anchor)//오브젝트 바인드
    {
        base.Init(anchor);
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Slider>(typeof(Sliders));
        Bind<ShowOnlySlot>(typeof(itemSlots));
    }

    public override void SetInfo(BaseUIData uiData)//데이터 로드 후 세팅
    {
        base.SetInfo(uiData);
        ItemConfirmData data = uiData as ItemConfirmData;
        isBuy = data.isBuy;
        _shopSlot = data.ShopSlot;
        _inventorySlot = data.InventorySlot;
        Get<ShowOnlySlot>((int)itemSlots.ItemSlot).Setitem(data.Item);//아이템 배치
        Get<TextMeshProUGUI>((int)Texts.ItmeName).text = data.Item.Data.Name;//아이템 이름 출력
        Get<TextMeshProUGUI>((int)Texts.ConfirmButtonTxt).text = isBuy ? "구매" : "판매";//버튼 텍스트 설정

        GetButton((int)Buttons.ConfirmButton).onClick.AddListener(OnConfirmBtn);
        GetButton((int)Buttons.CancelButton).onClick.AddListener(() => CloseUI());

        if (data.Item is CountableItem)//여러개 쌓이는 아이템인지 확인하고 여부에 다라 슬라이더 활성화
        {
            Get<Slider>((int)Sliders.ItemAmount).gameObject.SetActive(true);
            if (!isBuy)
            {
                //판매시 아이템정보로 슬라이더최대치 설정
                Get<Slider>((int)Sliders.ItemAmount).maxValue = (data.Item as CountableItem).GetCurrentAmount();
            }
            else
            {
                //구매시 99로 설정
                Get<Slider>((int)Sliders.ItemAmount).maxValue = 99;
            }
            Get<Slider>((int)Sliders.ItemAmount).onValueChanged.AddListener(OnSliderChanged);
            OnSliderChanged(1);
        }
        else
        {
            Get<Slider>((int)Sliders.ItemAmount).gameObject.SetActive(false);
        }
    }

    public void OnSliderChanged(float value)
    {
        int money = 0;
        Get<TextMeshProUGUI>((int)Texts.ItemAmountTxt).text = $"{value}/{Get<Slider>((int)Sliders.ItemAmount).maxValue}";
        if (isBuy)
        {
            
            GetButton((int)Buttons.ConfirmButton).interactable = _inventorySlot.GetInventory().GetComponent<Player>()._playerStatManager.Gold <
                    value * _shopSlot.Item.Data.BuyingPrice;
            money = Get<ShowOnlySlot>((int)itemSlots.ItemSlot).Item.Data.BuyingPrice * (int)value;
        }
        else {
            money = Get<ShowOnlySlot>((int)itemSlots.ItemSlot).Item.Data.SellingPrice * (int)value;
        }
        GetText((int)Texts.MoneyAmountTxt).text = money.ToString();
        GetText((int)Texts.MoneyAmountTxt).color = GetButton((int)Buttons.ConfirmButton).interactable ? Color.black : Color.red;
    }
    public void OnConfirmBtn()
    {
        if (isBuy)//살때
        {
            int amount = 1;
            
            Item item = Item.ItemSpawn(_shopSlot.Item.Data.ID);
            
            if (item is CountableItem)//쌓일수있는 아이템은 갯수 설정후 구매
            {
                amount = (int)Get<Slider>((int)Sliders.ItemAmount).value;
                (item as CountableItem).SetAmount(Mathf.Max(1, amount));
            }
            //쌓이지 않는 아이템은 그냥 구매
            
            int overAmount =  _inventorySlot.GetInventory().InsertItem(item);
            int money = item.Data.BuyingPrice * (amount - overAmount);
            _inventorySlot.GetInventory().GetComponent<Player>()._playerStatManager.Gold -= money;
        }
        else
        { //팔때

            int amount = (int)Get<Slider>((int)Sliders.ItemAmount).value;
            int money = _inventorySlot.Item.Data.SellingPrice * amount;
            if (_inventorySlot.Item is CountableItem)//슬라이더의 value값 만큼 수량을 감소시키고 판매값*value로 금액획득
            {
                CountableItem countable = (_inventorySlot.Item as CountableItem);
                countable.AddAmount(-amount);
                if (countable._amount == 0)//감소시키고 남은 개수0이면 삭제
                {
                    _inventorySlot.RemoveItem();
                }
            }
            else
            {
                _inventorySlot.RemoveItem();
            }
            Logger.LogWarning(money.ToString());


        }
        CloseUI();


    }

    public override void CloseUI(bool isCloseAll = false)
    {
        base.CloseUI(isCloseAll);
        GetButton((int)Buttons.ConfirmButton).onClick.RemoveAllListeners();
        Get<Slider>((int)Sliders.ItemAmount).onValueChanged.RemoveAllListeners();
    }
}
