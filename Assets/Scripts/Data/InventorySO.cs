using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "newInventory", menuName = "ScriptableObjects/newInventory")]
public class InventorySO : ScriptableObject
{
    [SerializeField] List<InventoryItemData> inventoryItems;

    List<InventoryItemData> currentInventoryItems;

    public List<InventoryItemData> CurrentInventoryItems => currentInventoryItems;

    public event UnityAction<InventoryItemData> OnNewInventoryItemAdded;

    public void Initialize()
    {
        currentInventoryItems = new List<InventoryItemData>();
        foreach (InventoryItemData data in inventoryItems)
        {
            InventoryItemData newItemData = new InventoryItemData(data.Item, data.Quantity);
            currentInventoryItems.Add(newItemData);
        }
    }

    public void UpdateQuantity(InventoryItemData itemData, int newValue)
    {
        bool isItemInInventory = false;

        foreach (InventoryItemData data in currentInventoryItems)
        {
            if (data.Item == itemData.Item)
            {
                data.UpdateQuantity(newValue);
                isItemInInventory = true;
                break;
            }
        }

        if (!isItemInInventory)
        {
            InventoryItemData newItemData = new InventoryItemData(itemData.Item, newValue);
            currentInventoryItems.Add(newItemData);
            OnNewInventoryItemAdded?.Invoke(newItemData);
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

    public InventoryItemData(ItemSO i, int q)
    {
        this.item = i;
        this.quantity = q;
    }

    public void UpdateQuantity(int newValue, bool throwUpdateEvent = true)
    {
        if (throwUpdateEvent)
            OnQuantityUpdated?.Invoke(quantity, quantity + newValue);

        quantity += newValue;
    }
}
