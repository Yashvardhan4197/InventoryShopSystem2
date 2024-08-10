using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newInventory",menuName ="ScriptableObjects/newInventory")]
public class InventorySO : ScriptableObject
{
    public Dictionary<int,InventoryItemData> inventoryItemData;
    [field: SerializeField]public int TotalSlots { get; private set; } = 10;

    [SerializeField] public ItemSO StartingItem1;
    [SerializeField] public ItemSO StartingItem2;
    [SerializeField] public ItemSO StartingItem3;
    [SerializeField] public int StartingItemAmount1;
    [SerializeField] public int StartingItemAmount2;
    [SerializeField] public int StartingItemAmount3;



    public void Initialize()
    {
        inventoryItemData = new Dictionary<int,InventoryItemData>();
        for(int i=0;i<TotalSlots; i++)
        {
            InventoryItemData newItemData = InventoryItemData.GetEmptyItem();
            inventoryItemData.Add(newItemData.uniqueID,newItemData);
        }
        
    }
    public void AddItem(ItemSO item, int quantity)
    {

        foreach(var itemData in inventoryItemData)
        {
            if (itemData.Value.itemID != -1 && itemData.Value.item==item)
            {
                if (itemData.Value.item.isStackable == true)
                {
                    itemData.Value.ChangeQuantity(quantity);
                }
                return;
            }
        }
        foreach (var itemData in inventoryItemData)
        {       
            if (itemData.Value.isEmpty())
            {
                itemData.Value.item = item;
                if(itemData.Value.quantity>1) { 
                }
                itemData.Value.ChangeQuantity(quantity);
                itemData.Value.SetItemID();
                break;
            }
        }
    }
    public Dictionary<int, InventoryItemData> GetInventoryItemData()
    {
        Dictionary<int, InventoryItemData> returnValue = new Dictionary<int, InventoryItemData>();

        foreach (var item in inventoryItemData)
        {
            if (!item.Value.isEmpty())
            {
                returnValue.Add(item.Key, item.Value);
                
            }
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
    public int uniqueID;
    private static int idcounter = 0;
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
        uniqueID = idcounter++;
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
