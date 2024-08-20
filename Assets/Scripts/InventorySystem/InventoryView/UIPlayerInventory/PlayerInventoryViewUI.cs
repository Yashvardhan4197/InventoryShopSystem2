
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerInventoryViewUI : InventoryViewUI
{
    private PlayerInventoryController playerInventoryController;
    [SerializeField] private Button useButton;
    [SerializeField] private Button sellButton;
    public UnityAction<InventoryItemData> SellItemEvent;
    public UnityAction<InventoryItemData,int> SellItemEventSureBox;

    private void Start()
    {
        useButton.onClick.AddListener(UseItem);
        sellButton.onClick.AddListener(SellItem);
        surelySellButton.onClick.AddListener(StartSellingEvent);
        SellItemEvent += ShowSureBox;
        closeSureBox.onClick.AddListener(HideSureBox);
        SetButtonStatus(false);
        //SurelySellButton.onClick.AddListener(StartSellingEvent);
        HideSureBox();
    }

    private void OnDestroy()
    {
        SellItemEvent -= ShowSureBox;
    }

    private void SellItem()
    {
        SellItemEvent?.Invoke(inventoryItemData);
    }
    private void StartSellingEvent()
    {
        if (CheckItemAvailability())
        {
            SellItemEventSureBox?.Invoke(inventoryItemData, GetItemIfAvailable());
        }
    }

    public void SetButtonStatus(bool status)
    {
        useButton.gameObject.SetActive(status);
        sellButton.gameObject.SetActive(status);
    }
    protected override void OnInventoryItemButtonPressed(InventoryItemData itemID)
    {
        playerInventoryController.soundService.PlaySound(Sound.Open);
        SetButtonStatus(true);
        base.OnInventoryItemButtonPressed(itemID);
    }

    public void SetController(PlayerInventoryController playerInventoryController)
    {
        this.playerInventoryController = playerInventoryController;
    }

    public override TextMeshProUGUI GetSureBoxText(InventoryItemData inventoryItemData)
    {
        sureBoxText.text ="SELL: "+ inventoryItemData.item.name;
        return sureBoxText;
    }

    public void StartSureBoxClosingProcess()
    {
        StartCoroutine(DelayPlayerInventorySellButton());
    }

    public IEnumerator DelayPlayerInventorySellButton()
    {
        SetButtonStatus(false);
        HideSureBoxButton();
        yield return new WaitForSeconds(2f);
        HideSureBox();
    }

    public override void Show()
    {
        playerInventoryController.soundService.PlaySound(Sound.Open);
        base.Show();
    }

}
