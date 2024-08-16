
using System.Collections;
using UnityEngine;


public class Inventory_Controller : MonoBehaviour
{
    [SerializeField] UIPlayerInventoryPage playerInventoryPage;
     [SerializeField] ShopUIInventoryPage  shopInventoryPage;
    [SerializeField] InventorySO playerInventorySO;
     [SerializeField] InventorySO shopInventorySO;

    private void Start()
    {
        playerInventorySO.Initialize_1();
        shopInventorySO.Initialize_1();
        InitializeStartingItems();
        playerInventoryPage.InitializeItems(playerInventorySO.GetInventoryItemData_1());
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
        foreach (var item in playerInventorySO.startingElements_1)
        {
            playerInventorySO.AddItem_1(item.item, item.quantity);
        }
        foreach(var item in shopInventorySO.startingElements_1)
        {
            shopInventorySO.AddItem_1(item.item,item.quantity);
        }
    }

    public void UseItemButtonPressed(InventoryItemData inventoryItemData)
    {
        foreach (var item in playerInventorySO.GetInventoryItemData_1())
        {
            if (item == inventoryItemData)
            {
                int changedValue = item.quantity;
                changedValue--;
                item.ChangeQuantity(changedValue);
                if (changedValue <= 0)
                {
                    item.ResetItemSlot();
                    playerInventoryPage.UpdateInventory(playerInventorySO.GetInventoryItemData_1());
                }
                else
                {
                    playerInventoryPage.UpdateInventory(playerInventorySO.GetInventoryItemData_1());
                }
                SoundManager.Instance.PlaySound(Sound.Open);
                return;
            }
        }
        

    }
    public void Buyitem(InventoryItemData inventoryItemData,int amountToBuy)
    {
        int totalMoneyToUpdate = amountToBuy * inventoryItemData.item.MoneyAmount;
        shopInventoryPage.CalculateAmount(totalMoneyToUpdate);
        if (amountToBuy <= inventoryItemData.quantity&&GameManager.Instance.GetMoneyAmount()>=totalMoneyToUpdate)
        {
            
            int changedValue=inventoryItemData.quantity;
            changedValue-=amountToBuy;
            playerInventorySO.AddItem_1(inventoryItemData.item, amountToBuy);
            inventoryItemData.ChangeQuantity(changedValue);
            if(changedValue <= 0)
            {
                inventoryItemData.ResetItemSlot();
            }
            shopInventoryPage.UpdateInventory(shopInventorySO.GetInventoryItemData_1());
            playerInventoryPage.UpdateInventory(playerInventorySO.GetInventoryItemData_1());
            GameManager.Instance.SetMoneyAmount(GameManager.Instance.GetMoneyAmount() - totalMoneyToUpdate);
            SoundManager.Instance.PlaySound(Sound.Accept);


            if (inventoryItemData.quantity <= 0)
            {
                StartCoroutine(DelayShopSureBoxClosing());

            }

        }
        else
        {
            SoundManager.Instance.PlaySound(Sound.Deny);
        }
    }
    public void SellItemSurely(InventoryItemData inventoryItemData,int amountToSell)
    {
        //GameManager.Instance.money+=inventoryItemData.item.money
        if(amountToSell<=inventoryItemData.quantity)
        {
            int totalMoneyToUpdate = amountToSell * inventoryItemData.item.MoneyAmount;
            playerInventoryPage.CalculateAmount(totalMoneyToUpdate);
            GameManager.Instance.SetMoneyAmount(GameManager.Instance.GetMoneyAmount()+totalMoneyToUpdate);
            inventoryItemData.ChangeQuantity(inventoryItemData.quantity - amountToSell);
            playerInventoryPage.UpdateInventory(playerInventorySO.GetInventoryItemData_1());
            SoundManager.Instance.PlaySound(Sound.Accept);
            if (inventoryItemData.quantity <= 0)
            {
                StartCoroutine(DelayPlayerInventorySellButton());
            }
            shopInventorySO.AddItem_1(inventoryItemData.item, amountToSell);
            shopInventoryPage.UpdateInventory(shopInventorySO.GetInventoryItemData_1());
        }
    }

    IEnumerator DelayShopSureBoxClosing()
    {
        shopInventoryPage.HideSureBoxButton();
        yield return new WaitForSeconds(2f);
        shopInventoryPage.HideSureBox();

    }
    IEnumerator DelayPlayerInventorySellButton()
    {
        playerInventoryPage.HideSureBoxButton();
        yield return new WaitForSeconds(2f);
        playerInventoryPage.HideSureBox();
    }
}
