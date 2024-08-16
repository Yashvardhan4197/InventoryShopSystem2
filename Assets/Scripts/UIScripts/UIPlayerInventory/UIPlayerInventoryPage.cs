using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPlayerInventoryPage : UIInventoryPage
{
    [SerializeField] Button UseButton;
    [SerializeField] Button SellButton;/*
    [SerializeField] CanvasGroup SureBox;
    [SerializeField] Button CloseSureBox;
    [SerializeField] Button SurelySellButton;
    [SerializeField] TextMeshProUGUI SureBoxText;
    [SerializeField] TMP_InputField moneyText;
    */
    public UnityAction<InventoryItemData,int> SellItemEventSureBox;
    private void Start()
    {
        UseButton.onClick.AddListener(UseItem);
        SellButton.onClick.AddListener(SellItem);
        SellItemEvent += ShowSureBox;
        CloseSureBox.onClick.AddListener(HideSureBox);
        SurelySellButton.onClick.AddListener(StartSellingEvent);
        HideSureBox();
    }
    private void OnDestroy()
    {
        SellItemEvent -= ShowSureBox;
    }

    void ShowSureBox(InventoryItemData inventoryItemData)
    {
        int temp = 0;
        SureBoxText.text ="SELL: "+ inventoryItemData.item.name;
        calculatedAmount.text = temp.ToString();
        ShowSureBox();
    }

    void StartSellingEvent()
    {
        string temp=moneyText.text.ToString();
        if (int.TryParse(temp, out int amountToSell))
        {
            Debug.Log("Money: " + amountToSell);
            SellItemEventSureBox?.Invoke(inventoryItemData, amountToSell);
        }
        else
        {
            Debug.LogError("Invalid number in moneyText."+moneyText.text);
            SoundManager.Instance.PlaySound(Sound.Deny);
        }
    }
}
