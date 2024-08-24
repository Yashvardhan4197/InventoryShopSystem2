
using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventoryController: InventoryController
{
    private ShopInventoryViewUI shopInventoryPage;
    private InventoryModel shopInventoryModel;

    private void SoldItem(InventoryItemData inventoryItemData, int amountToSell)
    {
        shopInventoryModel.AddItem(inventoryItemData.item, amountToSell);
        shopInventoryPage.UpdateInventory(shopInventoryModel.GetInventoryItemData());
    }

    public ShopInventoryController(ShopInventoryViewUI shopInventoryViewUI,InventoryModel inventoryModel,List<InventoryItemData>AllItems)
    {
        this.shopInventoryPage = shopInventoryViewUI;
        this.shopInventoryModel = inventoryModel;
        this.AllItems=AllItems;
    }

    public override void Init(SoundService soundService, MoneyService moneyService)
    {
        this.soundService= soundService;
        this.moneyService= moneyService;
        Initialize();
    }

    public void Buyitem(InventoryItemData inventoryItemData, int amountToBuy)
    {
        int totalMoneyToUpdate = amountToBuy * inventoryItemData.item.MoneyAmount;
        shopInventoryPage.CalculateAmount(totalMoneyToUpdate);
        if (amountToBuy <= inventoryItemData.quantity && moneyService.MoneyAmount >= totalMoneyToUpdate)
        {

            int changedValue = inventoryItemData.quantity;
            changedValue -= amountToBuy;
            TriggerBoughtItemEvent(inventoryItemData, amountToBuy);
            inventoryItemData.ChangeQuantity(changedValue);
            if (changedValue <= 0)
            {
                inventoryItemData.ResetItemSlot();
                shopInventoryPage.SetBuyButtonActive(false);
            }
            shopInventoryPage.UpdateInventory(shopInventoryModel.GetInventoryItemData());
            moneyService.SetMoneyAmount(moneyService.MoneyAmount - totalMoneyToUpdate);
            soundService.PlaySound(Sound.Accept);
            if (inventoryItemData.quantity <= 0)
            {
                shopInventoryPage.StartSureBoxClosingProcess();
            }
        }
        else
        {
            soundService.PlaySound(Sound.Deny);
        }
    }

    protected override void Initialize()
    {
        shopInventoryModel.Initialize();
        InitializeStartingItems();
        shopInventoryPage.SetController(this);
        shopInventoryPage.InitializeItems(shopInventoryModel.GetInventoryItemData());
        shopInventoryPage.BuyItemEventSureBox += Buyitem;
        SoldItemEvent += SoldItem;
    }

    protected override void InitializeStartingItems()
    {
        foreach (var item in AllItems)
        {
            shopInventoryModel.AddItem(item.item, item.quantity);
        }
    }



}
