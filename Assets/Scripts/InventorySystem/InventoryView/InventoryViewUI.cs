using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class InventoryViewUI : MonoBehaviour
{
    protected InventoryController inventoryController;

    [SerializeField] private InventoryItem defaultItem;
    [SerializeField] private RectTransform controlPanel;

    private Dictionary<InventoryItem,InventoryItemData>UIItemList = new Dictionary<InventoryItem,InventoryItemData>();
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI rarity;
    [SerializeField] private TextMeshProUGUI priceText;

    protected InventoryItemData inventoryItemData;
    [SerializeField] protected CanvasGroup sureBox;
    [SerializeField] protected Button closeSureBox;
    [SerializeField] protected Button surelySellButton;
    [SerializeField] protected TextMeshProUGUI sureBoxText;
    [SerializeField] protected TMP_InputField moneyText;
    [SerializeField] protected TextMeshProUGUI calculatedAmount;

    public UnityAction<InventoryItemData> UseItemEvent;

    private void Awake()
    {
        ResetDescription();
        Hide();
    }

    private void SetDescription(InventoryItemData currentItem)
    {
        inventoryItemData = currentItem;
        this.description.gameObject.SetActive(true);
        this.image.gameObject.SetActive(true);
        this.title.gameObject.SetActive(true);
        rarity.gameObject.SetActive(true);
        priceText.gameObject.SetActive(true);
        this.rarity.text = "Rarity: " + currentItem.item.Rarity;

        this.image.sprite = currentItem.item.Image;
        this.title.text = currentItem.item.name;
        this.description.text = currentItem.item.Description;
        this.priceText.text ="Price: "+ currentItem.item.MoneyAmount.ToString();
    }

    private void ResetDescription()
    {
        description.gameObject.SetActive(false);
        image.gameObject.SetActive(false);
        title.gameObject.SetActive(false);
        rarity.gameObject.SetActive(false);
        priceText.gameObject.SetActive(false);
    }

    private void AddNewItem(InventoryItemData item)
    {
        InventoryItem newItem = Instantiate(defaultItem, Vector3.zero, Quaternion.identity);
        newItem.transform.SetParent(controlPanel);
        newItem.SetInventoryItemData(item);
        UIItemList.Add(newItem, item);
        newItem.SetData(item);
        newItem.OnButtonPressed += OnInventoryItemButtonPressed;
    }

    protected void UseItem()
    {
        UseItemEvent?.Invoke(inventoryItemData);
    }

    protected virtual void OnInventoryItemButtonPressed(InventoryItemData itemID)
    {
        foreach (var item in UIItemList)
        {
            if (item.Key.GetInventoryItemData() == itemID)
            {
                SetDescription(item.Key.GetInventoryItemData());
            }
        }
    }

    protected bool CheckItemAvailability()
    {
        string temp = moneyText.text.ToString();
        if (int.TryParse(temp, out int amountToSell))
        {
            return true;
        }
        else
        {
            inventoryController.soundService.PlaySound(Sound.Deny);
        }
        return false;
    }
    protected int GetItemIfAvailable()
    {
        int temp = int.Parse(moneyText.text.ToString());
        return temp;

    }

    public void InitializeItems(List<InventoryItemData>inventoryItemDatas)
    {
        foreach (InventoryItemData item in inventoryItemDatas)
        {
            AddNewItem(item);
        }
        UpdateInventory(inventoryItemDatas);
    }

    public virtual void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable=true;
        canvasGroup.blocksRaycasts=true;
        ResetDescription();
    } 

    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = false;
        
    }

    public void UpdateInventory(List<InventoryItemData> inventoryItemDatas)
    {
        foreach (InventoryItemData item in inventoryItemDatas)
        {
            int i = 0;
            foreach(var item1 in UIItemList)
                if (item == item1.Key.GetInventoryItemData())
                {
                    item1.Key.SetData(item);
                    i = 1;
                    break;
                }
            if(i == 0)
            {
                AddNewItem(item);
            }
        }
       var keysToRemove=new List<InventoryItem>();
       foreach(var item in UIItemList)
        {
            if (!inventoryItemDatas.Contains(item.Key.GetInventoryItemData()))
            {
                keysToRemove.Add(item.Key);
            }
        }
       foreach(var key in keysToRemove)
        {
            Destroy(key.gameObject);
            UIItemList.Remove(key);
        }
    }

    public void ShowSureBox(InventoryItemData inventoryItemData)
    {
        int temp = 0;
        sureBox.alpha = 1;
        sureBox.interactable = true;
        sureBox.blocksRaycasts = true;
        surelySellButton.gameObject.SetActive(true);
        calculatedAmount.text = temp.ToString();
        if (inventoryItemData != null) { sureBoxText.text = GetSureBoxText(inventoryItemData).text; }
    }

    public abstract TextMeshProUGUI GetSureBoxText(InventoryItemData inventoryItemData);

    public void SetController(InventoryController inventoryController)
    {
        this.inventoryController = inventoryController;
    }

    public void HideSureBox()
    {
        sureBox.alpha = 0;
        sureBox.interactable = true;
        sureBox.blocksRaycasts = false;

    }

    public void CalculateAmount(int amount)
    {
        calculatedAmount.text = amount.ToString();
    }

    public void HideSureBoxButton()
    {
        surelySellButton.gameObject.SetActive(false);

    }

}
