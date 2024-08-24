using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newInventory",menuName ="ScriptableObjects/newInventory")]
public class InventoryModel : ScriptableObject
{
    private List<InventoryItemData> InventoryItemDatas;
    
    public void Initialize()
    {
        InventoryItemDatas = new List<InventoryItemData>();
    }
   
    public void AddItem(ItemData item,int quantity)
    {
        foreach (var itemData in InventoryItemDatas)
        {
            if (itemData.item != null && itemData.item == item)
            {
                itemData.ChangeQuantity(itemData.quantity+quantity);
                return;
            }
        }
        InventoryItemData newItem=new InventoryItemData(item,quantity);
        InventoryItemDatas.Add(newItem);
        
    }

    public List<InventoryItemData> GetInventoryItemData()
    {
        List<InventoryItemData>returnValue= new List<InventoryItemData>();
        List<InventoryItemData>toDelete= new List<InventoryItemData>();
        foreach (var item in InventoryItemDatas)
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
            InventoryItemDatas.Remove(item);
        }
        return returnValue;
    }
}
