using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerInventoryViewUI : InventoryViewUI
{
    [SerializeField] private Button UseButton;
    [SerializeField] private Button SellButton;
    public UnityAction<InventoryItemData> SellItemEvent;
    public UnityAction<InventoryItemData,int> SellItemEventSureBox;
    private void Start()
    {
        UseButton.onClick.AddListener(UseItem);
        SellButton.onClick.AddListener(SellItem);
        SurelySellButton.onClick.AddListener(StartSellingEvent);
        SellItemEvent += ShowSureBox;
        CloseSureBox.onClick.AddListener(HideSureBox);
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
        SureBoxText.text ="SELL: "+ inventoryItemData.item.name;
        return SureBoxText;
    }
    void StartSellingEvent()
    {
        if (CheckItemAvailability())
        {
            SellItemEventSureBox?.Invoke(inventoryItemData,GetItemIfAvailable());
        }
    }
}
