using System.Collections;
using UnityEngine;


public class InventoryController
{
     private PlayerInventoryViewUI playerInventoryPage;
     private ShopInventoryViewUI  shopInventoryPage;
     private InventoryModel playerInventoryModel;
     private InventoryModel shopInventoryModel;
     public SoundService soundService;
     public MoneyService moneyService;

    public InventoryController(PlayerInventoryViewUI playerInventoryPage,ShopInventoryViewUI shopInventoryPage,InventoryModel playerInventoryModel,InventoryModel shopInventoryModel)
    {
        this.playerInventoryPage = playerInventoryPage;
        this.shopInventoryPage = shopInventoryPage;
        this.playerInventoryModel = playerInventoryModel;
        this.shopInventoryModel = shopInventoryModel;
    }
    public void Init(SoundService soundService, MoneyService moneyService)
    {
        this.soundService = soundService;
        this.moneyService = moneyService;
        playerInventoryPage.SetController(this);
        shopInventoryPage.SetController(this);
        Initialize();
    } 
    private void Initialize()
    {
        playerInventoryModel.Initialize();
        shopInventoryModel.Initialize();
        InitializeStartingItems();
        playerInventoryPage.InitializeItems(playerInventoryModel.GetInventoryItemData_1());
        shopInventoryPage.InitializeItems(shopInventoryModel.GetInventoryItemData_1());
        playerInventoryPage.UseItemEvent += UseItemButtonPressed;
        shopInventoryPage.BuyItemEventSureBox += Buyitem;
        playerInventoryPage.SellItemEventSureBox += SellItemSurely;
    }
    private void OnDestroy()
    {
        playerInventoryPage.UseItemEvent-= UseItemButtonPressed;
        shopInventoryPage.BuyItemEventSureBox-= Buyitem;
        playerInventoryPage.SellItemEventSureBox-= SellItemSurely;
    }
    private void InitializeStartingItems()
    {
        foreach (var item in playerInventoryModel.GetStartingItemData())
        {
            playerInventoryModel.AddItem(item.item, item.quantity);
        }
        foreach(var item in shopInventoryModel.GetStartingItemData())
        {
            shopInventoryModel.AddItem(item.item,item.quantity);
        }
    }

    public void UseItemButtonPressed(InventoryItemData inventoryItemData)
    {
        foreach (var item in playerInventoryModel.GetInventoryItemData_1())
        {
            if (item == inventoryItemData)
            {
                int changedValue = item.quantity;
                changedValue--;
                item.ChangeQuantity(changedValue);
                if (changedValue <= 0)
                {
                    item.ResetItemSlot();
                    playerInventoryPage.UpdateInventory(playerInventoryModel.GetInventoryItemData_1());
                }
                else
                {
                    playerInventoryPage.UpdateInventory(playerInventoryModel.GetInventoryItemData_1());
                }
                soundService.PlaySound(Sound.Open);
                return;
            }
        }
        

    }
    public void Buyitem(InventoryItemData inventoryItemData,int amountToBuy)
    {
        int totalMoneyToUpdate = amountToBuy * inventoryItemData.item.MoneyAmount;
        shopInventoryPage.CalculateAmount(totalMoneyToUpdate);
        if (amountToBuy <= inventoryItemData.quantity && moneyService.GetMoneyAmount()>=totalMoneyToUpdate)
        {
            
            int changedValue=inventoryItemData.quantity;
            changedValue-=amountToBuy;
            playerInventoryModel.AddItem(inventoryItemData.item, amountToBuy);
            inventoryItemData.ChangeQuantity(changedValue);
            if(changedValue <= 0)
            {
                inventoryItemData.ResetItemSlot();
            }
            shopInventoryPage.UpdateInventory(shopInventoryModel.GetInventoryItemData_1());
            playerInventoryPage.UpdateInventory(playerInventoryModel.GetInventoryItemData_1());
            moneyService.SetMoneyAmount(moneyService.GetMoneyAmount() - totalMoneyToUpdate);
            soundService.PlaySound(Sound.Accept);


            if (inventoryItemData.quantity <= 0)
            {
                playerInventoryPage.StartSureBoxClosingProcess();

            }

        }
        else
        {
            soundService.PlaySound(Sound.Deny);
        }
    }
    public void SellItemSurely(InventoryItemData inventoryItemData,int amountToSell)
    {
        if(amountToSell<=inventoryItemData.quantity)
        {
            int totalMoneyToUpdate = amountToSell * inventoryItemData.item.MoneyAmount;
            playerInventoryPage.CalculateAmount(totalMoneyToUpdate);
            moneyService.SetMoneyAmount(moneyService.GetMoneyAmount()+totalMoneyToUpdate);
            inventoryItemData.ChangeQuantity(inventoryItemData.quantity - amountToSell);
            playerInventoryPage.UpdateInventory(playerInventoryModel.GetInventoryItemData_1());
            soundService.PlaySound(Sound.Accept);
            if (inventoryItemData.quantity <= 0)
            {
                playerInventoryPage.StartSureBoxClosingProcess();
            }
            shopInventoryModel.AddItem(inventoryItemData.item, amountToSell);
            shopInventoryPage.UpdateInventory(shopInventoryModel.GetInventoryItemData_1());
        }
        else
        {
            soundService.PlaySound(Sound.Deny);
        }
    }

    public IEnumerator DelayShopSureBoxClosing()
    {
        shopInventoryPage.HideSureBoxButton();
        yield return new WaitForSeconds(2f);
        shopInventoryPage.HideSureBox();

    }
    public IEnumerator DelayPlayerInventorySellButton()
    {
        playerInventoryPage.HideSureBoxButton();
        yield return new WaitForSeconds(2f);
        playerInventoryPage.HideSureBox();
    }
}
