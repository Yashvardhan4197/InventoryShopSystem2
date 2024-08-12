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
        shopinventoryPage.InitializeInventoryPage(shopinventorySO.InventoryItems);

        shopinventoryPage.OnShopBuyButtonClicked += BuyItem;

        playerInventorySO.Initialize();
        playerinventoryPage.InitializeInventoryPage(playerInventorySO.InventoryItems);
    }

    void OnDestroy()
    {
        shopinventoryPage.OnShopBuyButtonClicked -= BuyItem;
    }

    public void BuyItem(InventoryItemData itemData)
    {
        int newQuantity = itemData.Quantity;

        //TODO:Understand this logic
        //shopinventorySO.UpdateQuantity(itemData, itemData.quantity--);
        //playerInventorySO.UpdateQuantity(itemData, itemData.quantity++);

        shopinventorySO.UpdateQuantity(itemData, newQuantity--);
        playerInventorySO.UpdateQuantity(itemData, newQuantity++);
    }

    public void UseItem(InventoryItemData itemData)
    {
        int newQuantity = itemData.Quantity;
        playerInventorySO.UpdateQuantity(itemData, newQuantity--);
    }
}
