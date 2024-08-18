using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerInventoryViewUI : InventoryViewUI
{
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
    public override TextMeshProUGUI GetSureBoxText(InventoryItemData inventoryItemData)
    {
        sureBoxText.text ="SELL: "+ inventoryItemData.item.name;
        return sureBoxText;
    }
    void StartSellingEvent()
    {
        if (CheckItemAvailability())
        {
            SellItemEventSureBox?.Invoke(inventoryItemData,GetItemIfAvailable());
        }
    }

    public void StartSureBoxClosingProcess()
    {
        StartCoroutine(inventoryController.DelayPlayerInventorySellButton());
    }
}
