using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopUIInventoryPage : UIInventoryPage
{
    [SerializeField] Button BuyItemButton;
    [SerializeField] Button SellItemButton;
    public UnityAction<InventoryItemData,int> BuyItemEventSureBox;
    private void Start()
    {
        BuyItemButton.onClick.AddListener(BuyItem);
        SellItemButton.gameObject.SetActive(false);
        BuyItemEvent += ShowSureBox;
        CloseSureBox.onClick.AddListener(HideSureBox);
        SurelySellButton.onClick.AddListener(StartBuyingEvent);
        Hide();
        HideSureBox();
    }

    private void StartBuyingEvent()
    {
        string temp = moneyText.text.ToString();
        if (int.TryParse(temp, out int amountToSell))
        {
            Debug.Log("Money: " + amountToSell);
            BuyItemEventSureBox?.Invoke(inventoryItemData, amountToSell);
        }
        else
        {
            Debug.LogError("Invalid number in moneyText." + moneyText.text);
            SoundManager.Instance.PlaySound(Sound.Deny);
        }
    }
    private void OnDestroy()
    {
        BuyItemEvent -= ShowSureBox;
    }

    void ShowSureBox(InventoryItemData inventoryItemData)
    {
        int temp = 0;
        SureBoxText.text = "BUY: " + inventoryItemData.item.name;
        calculatedAmount.text = temp.ToString();
        ShowSureBox();
    }

}
