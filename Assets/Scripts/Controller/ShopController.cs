using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [SerializeField] UIShopInventoryPage shopinventoryPage;
    [SerializeField] UIPlayerInventoryPage playerinventoryPage;
    [SerializeField] InventorySO shopinventorySO, playerInventorySO;

    void Start()
    {
        shopinventorySO.Initialize();
        playerInventorySO.Initialize();

        shopinventoryPage.InitializeInventoryPage(shopinventorySO.CurrentInventoryItems);
        shopinventoryPage.OnShopBuyButtonClicked += BuyItem;
        playerinventoryPage.InitializeInventoryPage(playerInventorySO.CurrentInventoryItems);
    }

    void OnDestroy()
    {
        shopinventoryPage.OnShopBuyButtonClicked -= BuyItem;
    }

    public void BuyItem(InventoryItemData itemData, int quantity)
    {
        shopinventorySO.UpdateQuantity(itemData, -1 * quantity);
        playerInventorySO.UpdateQuantity(itemData, quantity);
    }

    public void UseItem(InventoryItemData itemData, int quantity)
    { 
        playerInventorySO.UpdateQuantity(itemData, -1 * quantity);
    }
}
