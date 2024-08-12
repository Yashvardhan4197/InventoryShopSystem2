using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopUIInventoryPage : MonoBehaviour
{
    [SerializeField] public InventoryItem DefaultItem;
    [SerializeField] public ShopUIInventoryDescription InventoryDescription;
    [SerializeField] public RectTransform controlPanel;
    [SerializeField] public ShopInventoryController shopinventoryController;
    public Dictionary<int, InventoryItem> ItemList = new Dictionary<int, InventoryItem>();

    // public List<InventoryItem> ListOfItems=new List<InventoryItem>();


    public void InitializeItems(int totalElements)
    {
        for (int i = 0; i < totalElements; i++)
        {
            InventoryItem newItem = Instantiate(DefaultItem, Vector3.zero, Quaternion.identity);
            newItem.transform.SetParent(controlPanel);
            newItem.inventoryitemID = shopinventoryController.ShopinventorySO.inventoryItemData[i].uniqueID;
            ItemList.Add(i, newItem);
            newItem.OnButtonPressed += OnInventoryItemButtonPressed;
        }
    }

    /*
    public void InitializeItems(int TotalItems)
    {
        for(int i=0;i<TotalItems;i++)
        {
            InventoryItem newItem=Instantiate(DefaultItem,Vector3.zero,Quaternion.identity);
            newItem.transform.SetParent(controlPanel);
            newItem.inventoryitemID = i;
            ListOfItems.Add(newItem);
            newItem.OnButtonPressed += OnInventoryItemButtonPressed;
        }
    }*/

    public void OnInventoryItemButtonPressed(int itemID)
    {
        foreach (var item in shopinventoryController.ShopinventorySO.GetInventoryItemData())
        {
            if (item.Key == itemID)
            {
                shopinventoryController.ShopinventoryDescription.SetDescription(item.Key, item.Value.item.image, item.Value.item.name, item.Value.item.description);
            }
        }
        //inventoryController.ButtonPressInfo(itemID);
    }
    public void Show()
    {
        gameObject.SetActive(true);
        InventoryDescription.ResetDescription();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    /*
    public void UpdateInventory(int itemID,Sprite itemImage,int amount)
    {
        foreach(var item in ListOfItems) {
            if (item.inventoryitemID==itemID)
            {
                item.SetData(itemID,itemImage, amount);
                break;
            }
        }

    }*/

    public void UpdateInventory(int itemID, Sprite itemImage, int amount)
    {
        foreach (var item in ItemList)
        {
            if (item.Value.inventoryitemID == itemID)
            {
                item.Value.SetData(itemID, itemImage, amount);
                break;
            }
        }
    }
}
