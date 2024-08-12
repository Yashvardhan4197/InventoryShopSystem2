using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIInventoryDescription : MonoBehaviour
{
    private int id;
    [SerializeField] Image Image;
    [SerializeField] TextMeshProUGUI Title;
    [SerializeField] TextMeshProUGUI Description;
    [SerializeField] Button UseItemButton;
    [SerializeField] ShopInventoryController inventoryController;

    private void Awake()
    {
        ResetDescription();
        UseItemButton.onClick.AddListener(UseItem);
    }
    public void SetDescription(int id, Sprite Image, string Title, string Desc)
    {
        this.id = id;
        this.Description.gameObject.SetActive(true);
        this.Image.gameObject.SetActive(true);
        this.Title.gameObject.SetActive(true);


        this.Image.sprite = Image;
        this.Title.text = Title;
        this.Description.text = Desc;
    }
    public void ResetDescription()
    {
        Description.gameObject.SetActive(false);
        Image.gameObject.SetActive(false);
        Title.gameObject.SetActive(false);
    }

    public void UseItem()
    {
        foreach (var item in inventoryController.ShopinventorySO.GetInventoryItemData())
        {
            if (item.Value.uniqueID == id)
            {
                inventoryController.Buyitem(item.Value.itemID);
            }
        }
        /*
        foreach (var item in inventoryController.inventorySO.GetInventoryItemData())
        {
            if (item.Key == id)
            {
                int changedValue = item.Value.quantity;
                changedValue--;
                item.Value.ChangeQuantity(changedValue);
                Debug.Log("Hello Again" + item.Value.quantity);
                if (changedValue <= 0)
                {
                    item.Value.ResetItemSlot();
                    inventoryController.inventoryPage.UpdateInventory(id, inventoryController.inventoryPage.DefaultItem.defaultSprite, 0);
                }
                else
                {
                    inventoryController.inventoryPage.UpdateInventory(id, item.Value.item.image, item.Value.quantity);
                }
                return;
            }
        }
        */
        //InventoryController.Instance.UseItemButtonPressed(id);
    }
}
