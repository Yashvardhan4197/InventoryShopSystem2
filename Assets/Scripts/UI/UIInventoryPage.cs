using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField] protected InventorySO inventorySO;
    [SerializeField] UIInventoryItem uiItemPrefab;
    [SerializeField] RectTransform controlPanel;
    [SerializeField] CanvasGroup canvasGroup;

    protected Dictionary<UIInventoryItem, InventoryItemData> uiItemsToItemDataDictionary = new Dictionary<UIInventoryItem, InventoryItemData>();

    protected virtual void Start()
    {
        inventorySO.OnNewInventoryItemAdded += AddUIInventoryItem;
    }


    protected virtual void OnDestroy()
    {
        inventorySO.OnNewInventoryItemAdded += AddUIInventoryItem;

        foreach (UIInventoryItem uiItem in uiItemsToItemDataDictionary.Keys)
        {
            uiItem.OnButtonPressed -= OnInventoryItemButtonPressed;
        }
        uiItemsToItemDataDictionary.Clear();

    }

    public virtual void InitializeInventoryPage(List<InventoryItemData> inventoryItems)
    { 
        foreach (InventoryItemData inventoryItemData in inventoryItems)
        {
            AddUIInventoryItem(inventoryItemData);
        }
    }

    void AddUIInventoryItem(InventoryItemData itemData)
    {
        UIInventoryItem newUIInventoryItem = Instantiate(uiItemPrefab, Vector3.zero, Quaternion.identity);
        newUIInventoryItem.transform.SetParent(controlPanel);
        newUIInventoryItem.Initialize(itemData);
        uiItemsToItemDataDictionary.Add(newUIInventoryItem, itemData);
        newUIInventoryItem.OnButtonPressed += OnInventoryItemButtonPressed;
    }

    protected virtual void OnInventoryItemButtonPressed(UIInventoryItem uiItem)
    {

    }

    public virtual void Show()
    {
        canvasGroup.alpha = 1f;
    }

    public virtual void Hide()
    {
        canvasGroup.alpha = 0f;
    }

}
