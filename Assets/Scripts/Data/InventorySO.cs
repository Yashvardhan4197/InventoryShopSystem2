using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newInventory",menuName ="ScriptableObjects/newInventory")]
public class InventorySO : ScriptableObject
{

    public List<InventoryItemData> inventoryItemData_1;
    public List<StartingElements> startingElements_1;
    
    public void Initialize_1()
    {
        inventoryItemData_1 = new List<InventoryItemData>();
    }

   

    public void AddItem_1(ItemSO item,int quantity)
    {
        foreach (var itemData in inventoryItemData_1)
        {
            if (itemData.itemID != -1 && itemData.item == item)
            {
                if (itemData.item.isStackable == true)
                {
                    itemData.ChangeQuantity(quantity);
                }
                return;
            }
        }
        InventoryItemData newItem=new InventoryItemData(item,quantity);
        inventoryItemData_1.Add(newItem);
        
    }

    public void AddItem_1(ItemSO item)
    {
        foreach (var itemData in inventoryItemData_1)
        {
            if (itemData.itemID != -1 && itemData.item == item)
            {
                if (itemData.item.isStackable == true)
                {
                    itemData.ChangeQuantity(itemData.quantity+1);
                }
                return;
            }
        }
        InventoryItemData newItem = new InventoryItemData(item,1);
        inventoryItemData_1.Add(newItem);
    }

    
    public List<InventoryItemData> GetInventoryItemData_1()
    {
        List<InventoryItemData>returnValue= new List<InventoryItemData>();
        List<InventoryItemData>toDelete= new List<InventoryItemData>();
        foreach (var item in inventoryItemData_1)
        {
            if (!item.isEmpty())
            {
                returnValue.Add(item);

            }
            else
            {
                toDelete.Add(item);
            }
        }
        foreach (var item in toDelete)
        {
            inventoryItemData_1.Remove(item);
        }
        return returnValue;
    }


}


[Serializable]
public class InventoryItemData
{
    public ItemSO item;
    public int quantity;
    public int itemID=-1;
    public InventoryItemData(ItemSO item,int quantity)
    {
        this.item = item;
        this.quantity = quantity;
        if (item != null)
        {
            this.itemID = item.id;
        }
        else
        {
            itemID = -1;
        }
    }
    public bool isEmpty()
    {
        if (item == null || quantity == 0)
        {
            return true;
        }
        return false;
    }

    public void ChangeQuantity(int quantity)
    {
        this.quantity = quantity;
        
    }

    public void SetItemID()
    {
        if (item != null)
        {
            itemID=item.id;
        }
    }
    public void ResetItemSlot()
    {
        item = null;
        itemID = -1;
        quantity = -1;

    }

    public static InventoryItemData GetEmptyItem()
    {
        InventoryItemData newItem=new InventoryItemData(null,0);
        return newItem;
    }
}

[Serializable]
public class StartingElements
{
    public ItemSO item;
    public int quantity;

    public StartingElements(ItemSO item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}
