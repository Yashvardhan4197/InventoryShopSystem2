using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopInventoryController : MonoBehaviour
{
    [SerializeField] public ShopUIInventoryDescription ShopinventoryDescription;
    [SerializeField] public ShopUIInventoryPage ShopinventoryPage;
    [SerializeField] public InventorySO ShopinventorySO;

    [SerializeField] public Button ShopInventoryOpenButton;
    [SerializeField] public InventoryController playerInventoryController;


    bool isOpened = false;

    private void Start()
    {
        ShopInventoryOpenButton.onClick.AddListener(OpenInventory);
        ShopinventorySO.Initialize();
        ShopinventoryPage.InitializeItems(ShopinventorySO.TotalSlots);
        ShopinventorySO.AddItem(ShopinventorySO.StartingItem1, ShopinventorySO.StartingItemAmount1);
        ShopinventorySO.AddItem(ShopinventorySO.StartingItem2, ShopinventorySO.StartingItemAmount2);
        //inventorySO.AddItem(inventorySO.StartingItem3,inventorySO.StartingItemAmount3);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && isOpened == false)
        {
            OpenInventory();
        }

    }

    public void OpenInventory()
    {
        if (!isOpened)
        {
            ShopinventoryPage.Show();
            foreach (var item in ShopinventorySO.GetInventoryItemData())
            {
                //Debug.Log("item Name: " + item.Value.item.name);
                ShopinventoryPage.UpdateInventory(item.Key, item.Value.item.image, item.Value.quantity);
            }

            isOpened = true;
        }
        else
        {
            ShopinventoryPage.Hide();
            isOpened = false;
        }
    }
    public void Buyitem(int itemID)
    {
        foreach (var item in ShopinventorySO.GetInventoryItemData())
        {
            if (itemID == item.Value.itemID)
            {
                Debug.Log("kuch kuch hota hai");
                int changedValue = item.Value.quantity;
                changedValue--;
                item.Value.ChangeQuantity(changedValue);
                if (changedValue <= 0)
                {
                    item.Value.ResetItemSlot();
                    ShopinventoryPage.UpdateInventory(itemID, ShopinventoryPage.DefaultItem.defaultSprite, 0);
                }
                else
                {
                    ShopinventoryPage.UpdateInventory(itemID, item.Value.item.image, item.Value.quantity);
                }

                playerInventoryController.inventorySO.IncrementItemQuantity(item.Value.item);
                playerInventoryController.UpdateFullInventory();

            }
        }
    }
}
