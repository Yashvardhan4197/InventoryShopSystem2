using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIShopInventoryPage : UIInventoryPage
{
    [SerializeField] Image descriptionImage;
    [SerializeField] TextMeshProUGUI descriptionTitle;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] Button buyItemButton;

    InventoryItemData currentSelectedItem;
    public event UnityAction<InventoryItemData, int> OnShopBuyButtonClicked;

    protected override void OnDestroy()
    {
        base.OnDestroy();

        buyItemButton.onClick.RemoveListener(BuyItemButtonPressed);
    }

    protected override void OnInventoryItemButtonPressed(UIInventoryItem uiItem)
    {
        base.OnInventoryItemButtonPressed(uiItem);

        if (uiItemsToItemDataDictionary.TryGetValue(uiItem, out InventoryItemData itemData))
        {
            SetDescription(itemData);
            currentSelectedItem = itemData;
            buyItemButton.interactable = true;
        }
    }

    public override void Show()
    {
        base.Show();
        ResetDescription();
        buyItemButton.interactable = false;
    }

    public void SetDescription(InventoryItemData itemData)
    {
        this.descriptionText.gameObject.SetActive(true);
        this.descriptionImage.gameObject.SetActive(true);
        this.descriptionTitle.gameObject.SetActive(true);


        this.descriptionImage.sprite = itemData.Item.Image;
        this.descriptionTitle.text = itemData.Item.ItemName;
        this.descriptionText.text = itemData.Item.Description;
    }

    public void ResetDescription()
    {
        descriptionText.gameObject.SetActive(false);
        descriptionImage.gameObject.SetActive(false);
        descriptionTitle.gameObject.SetActive(false);
    }

    public void BuyItemButtonPressed()
    {
        if (currentSelectedItem != null)
            OnShopBuyButtonClicked?.Invoke(currentSelectedItem, 1);
    }
}
