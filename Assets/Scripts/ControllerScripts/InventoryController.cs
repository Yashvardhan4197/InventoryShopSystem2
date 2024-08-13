using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{


    [SerializeField]public UIInventoryDescription inventoryDescription;
    [SerializeField]public UIInventoryPage inventoryPage;
    [SerializeField]public InventorySO inventorySO;

    [SerializeField] public Button InventoryOpenButton;
    [SerializeField]public List<InventoryItemData> StartingInventoryItems;

    private bool isOpened = false;
    private void Start()
    {
        InventoryOpenButton.onClick.AddListener(OpenInventory);
        inventorySO.Initialize_1();
        inventoryPage.InitializeItems(inventorySO.GetInventoryItemData_1());
        InitializeStartingItems();
        //inventorySO.AddItem_1(inventorySO.StartingItem1, inventorySO.StartingItemAmount1);
        //inventorySO.AddItem_1(inventorySO.StartingItem2, inventorySO.StartingItemAmount2);
        //inventorySO.AddItem(inventorySO.StartingItem3,inventorySO.StartingItemAmount3);
        inventoryPage.UpdateInventory(inventorySO.GetInventoryItemData_1());
    }

    private void InitializeStartingItems()
    {
        foreach (var item in inventorySO.startingElements_1)
        {
            inventorySO.AddItem_1(item.item,item.quantity);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)&&isOpened==false)
        {
            OpenInventory();
        }

    }

    public void ButtonPressInfo(int itemID)
    {
        foreach (InventoryItemData item in inventorySO.GetInventoryItemData_1())
        {
            if (item.itemID == itemID)
            {
                inventoryDescription.SetDescription(item);
            }
        }
    }
    public void OpenInventory()
    {
        if(!isOpened)
        {
            inventoryPage.Show();
            inventoryPage.UpdateInventory(inventorySO.GetInventoryItemData_1());

            isOpened = true;
        }
        else
        {
            inventoryPage.Hide();
            isOpened = false;
        }
    }

    public void UseItemButtonPressed(InventoryItemData inventoryItemData)
    {
        foreach (var item in inventorySO.GetInventoryItemData_1())
        {
            if (item == inventoryItemData)
            {
                int changedValue=item.quantity;
                changedValue--;
                item.ChangeQuantity(changedValue);
                //Debug.Log("Hello Again" + item.Value.quantity);
                if (changedValue <= 0)
                {
                    item.ResetItemSlot();
                    inventoryPage.UpdateInventory(inventorySO.GetInventoryItemData_1());
                }
                else
                {
                    inventoryPage.UpdateInventory(inventorySO.GetInventoryItemData_1());
                }
                return;
            }
        }
        
    }

    
    

}
