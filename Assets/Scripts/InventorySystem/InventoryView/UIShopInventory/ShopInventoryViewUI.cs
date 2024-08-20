using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopInventoryViewUI : InventoryViewUI
{
    private ShopInventoryController shopInventoryController;
    [SerializeField] private Button BuyItemButton;
    public UnityAction<InventoryItemData> BuyItemEvent;
    public UnityAction<InventoryItemData, int> BuyItemEventSureBox;


    private void Start()
    {
        BuyItemButton.onClick.AddListener(BuyItem);
        BuyItemEvent += ShowSureBox;
        closeSureBox.onClick.AddListener(HideSureBox);
        surelySellButton.onClick.AddListener(StartBuyingEvent);
        BuyItemButton.gameObject.SetActive(false);
        Hide();
        HideSureBox();
    }

    private void BuyItem()
    {
        if (shopInventoryController.moneyService.MoneyAmount >= inventoryItemData.item.MoneyAmount)
        {
            BuyItemEvent?.Invoke(inventoryItemData);
        }
        else
        {
            shopInventoryController.soundService.PlaySound(Sound.Deny);
        }
    }

    private void StartBuyingEvent()
    {
        if (CheckItemAvailability())
        {
            BuyItemEventSureBox?.Invoke(inventoryItemData, GetItemIfAvailable());
        }
    }

    private void OnDestroy()
    {
        BuyItemEvent -= ShowSureBox;
    }

    protected override void OnInventoryItemButtonPressed(InventoryItemData itemID)
    {
        shopInventoryController.soundService.PlaySound(Sound.Open);
        SetBuyButtonActive(true);
        base.OnInventoryItemButtonPressed(itemID);
    }

    public void SetController(ShopInventoryController shopInventoryController)
    {
        this.shopInventoryController = shopInventoryController;
    }

    public void SetBuyButtonActive(bool status)
    {
        BuyItemButton.gameObject.SetActive(status);
    }
    public override TextMeshProUGUI GetSureBoxText(InventoryItemData inventoryItemData)
    {
        sureBoxText.text = "BUY: " + inventoryItemData.item.name;
        return sureBoxText;
    }

    public void StartSureBoxClosingProcess()
    {
        StartCoroutine(DelayShopSureBoxClosing());
    }

    public IEnumerator DelayShopSureBoxClosing()
    {
        HideSureBoxButton();
        yield return new WaitForSeconds(2f);
        HideSureBox();
    }

    public override void Show()
    {
        shopInventoryController.soundService.PlaySound(Sound.Open);
        base.Show();
    }

    
}
