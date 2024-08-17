
using System.Collections;
using UnityEngine;


public class InventoryController : MonoBehaviour
{
    [SerializeField] private PlayerInventoryViewUI playerInventoryPage;
    [SerializeField] private ShopInventoryViewUI  shopInventoryPage;
    [SerializeField] private InventoryModel playerInventoryModel;
    [SerializeField] private InventoryModel shopInventorySO;

    private void Start()
    {
        playerInventoryModel.Initialize();
        shopInventorySO.Initialize();
        InitializeStartingItems();
        playerInventoryPage.InitializeItems(playerInventoryModel.GetInventoryItemData_1());
        shopInventoryPage.InitializeItems(shopInventorySO.GetInventoryItemData_1());
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
        foreach(var item in shopInventorySO.GetStartingItemData())
        {
            shopInventorySO.AddItem(item.item,item.quantity);
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
                GameService.Instance.SoundService.PlaySound(Sound.Open);
                return;
            }
        }
        

    }
    public void Buyitem(InventoryItemData inventoryItemData,int amountToBuy)
    {
        int totalMoneyToUpdate = amountToBuy * inventoryItemData.item.MoneyAmount;
        shopInventoryPage.CalculateAmount(totalMoneyToUpdate);
        if (amountToBuy <= inventoryItemData.quantity&&GameService.Instance.MoneyService.GetMoneyAmount()>=totalMoneyToUpdate)
        {
            
            int changedValue=inventoryItemData.quantity;
            changedValue-=amountToBuy;
            playerInventoryModel.AddItem(inventoryItemData.item, amountToBuy);
            inventoryItemData.ChangeQuantity(changedValue);
            if(changedValue <= 0)
            {
                inventoryItemData.ResetItemSlot();
            }
            shopInventoryPage.UpdateInventory(shopInventorySO.GetInventoryItemData_1());
            playerInventoryPage.UpdateInventory(playerInventoryModel.GetInventoryItemData_1());
            GameService.Instance.MoneyService.SetMoneyAmount(GameService.Instance.MoneyService.GetMoneyAmount() - totalMoneyToUpdate);
            GameService.Instance.SoundService.PlaySound(Sound.Accept);


            if (inventoryItemData.quantity <= 0)
            {
                StartCoroutine(DelayShopSureBoxClosing());

            }

        }
        else
        {
            GameService.Instance.SoundService.PlaySound(Sound.Deny);
        }
    }
    public void SellItemSurely(InventoryItemData inventoryItemData,int amountToSell)
    {
        if(amountToSell<=inventoryItemData.quantity)
        {
            int totalMoneyToUpdate = amountToSell * inventoryItemData.item.MoneyAmount;
            playerInventoryPage.CalculateAmount(totalMoneyToUpdate);
            GameService.Instance.MoneyService.SetMoneyAmount(GameService.Instance.MoneyService.GetMoneyAmount()+totalMoneyToUpdate);
            inventoryItemData.ChangeQuantity(inventoryItemData.quantity - amountToSell);
            playerInventoryPage.UpdateInventory(playerInventoryModel.GetInventoryItemData_1());
            GameService.Instance.SoundService.PlaySound(Sound.Accept);
            if (inventoryItemData.quantity <= 0)
            {
                StartCoroutine(DelayPlayerInventorySellButton());
            }
            shopInventorySO.AddItem(inventoryItemData.item, amountToSell);
            shopInventoryPage.UpdateInventory(shopInventorySO.GetInventoryItemData_1());
        }
        else
        {
            GameService.Instance.SoundService.PlaySound(Sound.Deny);
        }
    }

    private IEnumerator DelayShopSureBoxClosing()
    {
        shopInventoryPage.HideSureBoxButton();
        yield return new WaitForSeconds(2f);
        shopInventoryPage.HideSureBox();

    }
    private IEnumerator DelayPlayerInventorySellButton()
    {
        playerInventoryPage.HideSureBoxButton();
        yield return new WaitForSeconds(2f);
        playerInventoryPage.HideSureBox();
    }
}
