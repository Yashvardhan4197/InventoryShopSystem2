using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField] public InventoryItem DefaultItem;
    [SerializeField] public UIInventoryDescription InventoryDescription;
    [SerializeField] public RectTransform controlPanel;
    [SerializeField]public InventoryController inventoryController;

    //public Dictionary<int,InventoryItem> ItemList = new Dictionary<int,InventoryItem>();
    
   public List<InventoryItem> ItemList=new List<InventoryItem>();

 
    public void InitializeItems(List<InventoryItemData>inventoryItemDatas)
    {
        Debug.Log("Hello again");
        foreach (InventoryItemData item in inventoryItemDatas)
        {
            
            InventoryItem newItem = Instantiate(DefaultItem, Vector3.zero, Quaternion.identity);
            newItem.transform.SetParent(controlPanel);
            newItem.inventoryItemData =item;
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

    public void OnInventoryItemButtonPressed(InventoryItemData itemID)
    {
        foreach (var item in inventoryController.inventorySO.GetInventoryItemData_1())
        {
            if (item == itemID)
            {
                inventoryController.inventoryDescription.SetDescription(item);
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
       foreach(InventoryItemData item in inventoryItemDatas)
        {
            int i = 0;
            foreach(InventoryItem item1 in ItemList)
                if (item == item1.inventoryItemData)
                {
                    item1.SetData(item);
                    i = 1;
                    break;
                }
            if(i == 0)
            {
                AddNewItem(item);
            }
        }

       foreach(InventoryItem item in ItemList)
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
