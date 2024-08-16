
using UnityEngine;


public class Inventory_Controller : MonoBehaviour
{
    [SerializeField] UIPlayerInventoryPage playerInventoryPage;
     [SerializeField] ShopUIInventoryPage  shopInventoryPage;
    [SerializeField] InventorySO playerInventorySO,shopInventorySO;

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
                return;
            }
        }
        SoundManager.Instance.PlaySound(Sound.Open);

    }
    public void Buyitem(InventoryItemData inventoryItemData,int amountToBuy)
    {
        int totalMoneyToUpdate = amountToBuy * inventoryItemData.item.MoneyAmount;
        shopInventoryPage.CalculateAmount(totalMoneyToUpdate);
        if (amountToBuy <= inventoryItemData.quantity&&GameManager.Instance.GetMoneyAmount()>=totalMoneyToUpdate)
        {
           
            foreach (var item in shopInventorySO.GetInventoryItemData_1())
            {
                if (inventoryItemData == item)
                {
                    
                    int changedValue = item.quantity;
                    changedValue-=amountToBuy;
                    item.ChangeQuantity(changedValue);
                    if (changedValue <= 0)
                    {
                        item.ResetItemSlot();
                    }
                    shopInventoryPage.UpdateInventory(shopInventorySO.GetInventoryItemData_1());

                    playerInventorySO.AddItem_1(item.item,amountToBuy);
                    playerInventoryPage.UpdateInventory(playerInventorySO.GetInventoryItemData_1());
                    GameManager.Instance.SetMoneyAmount(GameManager.Instance.GetMoneyAmount()-totalMoneyToUpdate);
                    SoundManager.Instance.PlaySound(Sound.Accept);
                    break;
                    //playerInventoryController.UpdateFullInventory();

                }
            }
            if (inventoryItemData.quantity <= 0)
            {
                shopInventoryPage.HideSureBox();
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
                playerInventoryPage.HideSureBox();
            }
        }
    }

}
