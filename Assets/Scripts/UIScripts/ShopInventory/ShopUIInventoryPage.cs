using System;
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
   // public Dictionary<int, InventoryItem> ItemList = new Dictionary<int, InventoryItem>();

    public List<InventoryItem> ItemList=new List<InventoryItem>();


    public void Initialize_Items(List<InventoryItemData> itemList)
    {
        

        foreach (InventoryItemData item in itemList)
        {
            InventoryItem newItem = Instantiate(DefaultItem, Vector3.zero, Quaternion.identity);
            newItem.transform.SetParent(controlPanel);
            newItem.SetData(item);
            ItemList.Add(newItem);
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
    public void OnInventoryItemButtonPressed(InventoryItemData inventoryItemData)
    {
        foreach (var item in shopinventoryController.ShopinventorySO.GetInventoryItemData_1())
        {
            if (item == inventoryItemData)
            {
                shopinventoryController.ShopinventoryDescription.SetDescription(inventoryItemData);
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

    public void UpdateInventory(List<InventoryItemData> inventoryItemDatas)
    {
        foreach (InventoryItemData item in inventoryItemDatas)
        {
            int i = 0;
            foreach (InventoryItem item1 in ItemList)
                if (item == item1.inventoryItemData)
                {
                    item1.SetData(item);
                    i = 1;
                    break;
                }
            if (i == 0)
            {
                AddNewItem(item);
            }
        }

        foreach (InventoryItem item in ItemList)
        {
            if (!inventoryItemDatas.Contains(item.inventoryItemData))
            {
                ItemList.Remove(item);
            }
        }
    }

    private void AddNewItem(InventoryItemData item)
    {
        InventoryItem newItem = Instantiate(DefaultItem, Vector3.zero, Quaternion.identity);
        newItem.transform.SetParent(controlPanel);
        newItem.inventoryItemData = item;
        ItemList.Add(newItem);
        newItem.OnButtonPressed += OnInventoryItemButtonPressed;
    }
}
