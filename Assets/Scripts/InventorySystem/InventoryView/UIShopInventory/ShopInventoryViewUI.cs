using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopInventoryViewUI : InventoryViewUI
{
    [SerializeField] private Button BuyItemButton;
    public UnityAction<InventoryItemData> BuyItemEvent;
    public UnityAction<InventoryItemData,int> BuyItemEventSureBox;
    private void Start()
    {
        BuyItemButton.onClick.AddListener(BuyItem);
        BuyItemEvent += ShowSureBox;
        CloseSureBox.onClick.AddListener(HideSureBox);
        SurelySellButton.onClick.AddListener(StartBuyingEvent);

        Hide();
        HideSureBox();
    }
    private void BuyItem()
    {
        if (GameService.Instance.MoneyService.GetMoneyAmount() >= inventoryItemData.item.MoneyAmount)
        {
            BuyItemEvent?.Invoke(inventoryItemData);

        }
        else
        {
            GameService.Instance.SoundService.PlaySound(Sound.Deny);
        }
    }
    private void StartBuyingEvent()
    {
        if (CheckItemAvailability())
        {
            BuyItemEventSureBox?.Invoke(inventoryItemData,GetItemIfAvailable());
        }
    }
    private void OnDestroy()
    {
        BuyItemEvent -= ShowSureBox;
    }

    public override TextMeshProUGUI GetSureBoxText(InventoryItemData inventoryItemData)
    {
        SureBoxText.text = "BUY: " + inventoryItemData.item.name;
        return SureBoxText;
    }

}
