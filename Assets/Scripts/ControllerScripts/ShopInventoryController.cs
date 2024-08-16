using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopInventoryController : MonoBehaviour
{
    /*
    [SerializeField] public ShopUIInventoryDescription ShopinventoryDescription;
    [SerializeField] public ShopUIInventoryPage ShopinventoryPage;
    [SerializeField] public InventorySO ShopinventorySO;

    [SerializeField] public Button ShopInventoryOpenButton;
    [SerializeField] public InventoryController playerInventoryController;


    bool isOpened = false;

    private void Start()
    {
        ShopInventoryOpenButton.onClick.AddListener(OpenInventory);
        ShopinventorySO.Initialize_1();
        ShopinventoryPage.Initialize_Items(ShopinventorySO.GetInventoryItemData_1());
        InitializeStartingItems();
    }
    private void InitializeStartingItems()
    {
        foreach (var item in ShopinventorySO.startingElements_1)
        {
            ShopinventorySO.AddItem_1(item.item, item.quantity);
        }
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
            ShopinventoryPage.UpdateInventory(ShopinventorySO.GetInventoryItemData_1());

            isOpened = true;
        }
        else
        {
            ShopinventoryPage.Hide();
            isOpened = false;
        }
    }
    public void Buyitem(InventoryItemData inventoryItemData)
    {
        foreach (var item in ShopinventorySO.GetInventoryItemData_1())
        {
            if (inventoryItemData == item)
            {
                int changedValue = item.quantity;
                changedValue--;
                item.ChangeQuantity(changedValue);
                if (changedValue <= 0)
                {
                    item.ResetItemSlot();
                }
                ShopinventoryPage.UpdateInventory(ShopinventorySO.GetInventoryItemData_1());

                playerInventoryController.inventorySO.AddItem_1(item.item);
                //playerInventoryController.UpdateFullInventory();

            }
        }
    }

   */
}
