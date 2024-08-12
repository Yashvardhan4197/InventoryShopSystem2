using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "newInventory", menuName = "ScriptableObjects/newInventory")]
public class InventorySO : ScriptableObject
{
    [SerializeField] List<InventoryItemData> inventoryItems;

    List<InventoryItemData> currentInventoryItems;

    public List<InventoryItemData> InventoryItems => currentInventoryItems;

    public event UnityAction<InventoryItemData> OnNewInventoryItemAdded;

    public void Initialize()
    {
        currentInventoryItems = new List<InventoryItemData>(inventoryItems);
    }

    public void UpdateQuantity(InventoryItemData itemData, int newValue)
    {
        bool isItemInInventory = false;

        foreach (InventoryItemData data in currentInventoryItems)
        {
            if (data.Item == itemData.Item)
            {
                itemData.UpdateQuantity(newValue);
                isItemInInventory = true;
                break;
            }
        }

        if (!isItemInInventory)
        {
            currentInventoryItems.Add(itemData);
            itemData.UpdateQuantity(newValue, false);
            OnNewInventoryItemAdded?.Invoke(itemData);
        }
    }
}

[Serializable]
public class InventoryItemData
{
    [SerializeField] ItemSO item;
    [SerializeField] int quantity;

    public ItemSO Item => item;
    public int Quantity => quantity;

    public event UnityAction<int, int> OnQuantityUpdated;

    public void UpdateQuantity(int newValue, bool throwUpdateEvent = true)
    {
        if (throwUpdateEvent)
            OnQuantityUpdated?.Invoke(quantity, newValue);

        quantity = newValue;
    }
}
