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
        closeSureBox.onClick.AddListener(HideSureBox);
        surelySellButton.onClick.AddListener(StartBuyingEvent);

        Hide();
        HideSureBox();
    }
    private void BuyItem()
    {
        if (inventoryController.moneyService.MoneyAmount >= inventoryItemData.item.MoneyAmount)
        {
            BuyItemEvent?.Invoke(inventoryItemData);

        }
        else
        {
            inventoryController.soundService.PlaySound(Sound.Deny);
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
        sureBoxText.text = "BUY: " + inventoryItemData.item.name;
        return sureBoxText;
    }
    public void StartSureBoxClosingProcess()
    {
        StartCoroutine(inventoryController.DelayShopSureBoxClosing());
    }
}
