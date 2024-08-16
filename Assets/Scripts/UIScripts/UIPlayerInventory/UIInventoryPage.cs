using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField] public InventoryItem DefaultItem;
    [SerializeField] public RectTransform controlPanel;
    
    private Dictionary<InventoryItem,InventoryItemData>UIItemList = new Dictionary<InventoryItem,InventoryItemData>();
    protected InventoryItemData inventoryItemData;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Image Image;
    [SerializeField] TextMeshProUGUI Title;
    [SerializeField] TextMeshProUGUI Description;
    [SerializeField] TextMeshProUGUI Rarity;
    [SerializeField] TextMeshProUGUI priceText;

    public UnityAction<InventoryItemData> UseItemEvent;
    public UnityAction<InventoryItemData> BuyItemEvent;
    public UnityAction<InventoryItemData> SellItemEvent;

    [SerializeField] protected CanvasGroup SureBox;
    [SerializeField] protected Button CloseSureBox;
    [SerializeField] protected Button SurelySellButton;
    [SerializeField] protected TextMeshProUGUI SureBoxText;
    [SerializeField] protected TMP_InputField moneyText;
    [SerializeField] protected TextMeshProUGUI calculatedAmount;

    private void Awake()
    {
        ResetDescription();
        Hide();
    }
    protected void BuyItem()
    {
        if (GameManager.Instance.GetMoneyAmount() > inventoryItemData.item.MoneyAmount) {
            BuyItemEvent?.Invoke(inventoryItemData);
            
        }
        else
        {
            SoundManager.Instance.PlaySound(Sound.Deny);
        }
    }

    protected void SellItem()
    {
        SellItemEvent?.Invoke(inventoryItemData);
    }
    private void SetDescription(InventoryItemData currentItem)
    {
        inventoryItemData = currentItem;
        this.Description.gameObject.SetActive(true);
        this.Image.gameObject.SetActive(true);
        this.Title.gameObject.SetActive(true);
        Rarity.gameObject.SetActive(true);
        priceText.gameObject.SetActive(true);
        this.Rarity.text = "Rarity: " + currentItem.item.rarity;

        this.Image.sprite = currentItem.item.image;
        this.Title.text = currentItem.item.name;
        this.Description.text = currentItem.item.description;
        this.priceText.text ="Price: "+ currentItem.item.MoneyAmount.ToString();
    }

    private void ResetDescription()
    {
        Description.gameObject.SetActive(false);
        Image.gameObject.SetActive(false);
        Title.gameObject.SetActive(false);
        Rarity.gameObject.SetActive(false);
        priceText.gameObject.SetActive(false);
    }

    protected void UseItem()
    {
        UseItemEvent?.Invoke(inventoryItemData);


    }


    public void InitializeItems(List<InventoryItemData>inventoryItemDatas)
    {
        foreach (InventoryItemData item in inventoryItemDatas)
        {
            AddNewItem(item);
        }
        UpdateInventory(inventoryItemDatas);
    }
    
   

    private void OnInventoryItemButtonPressed(InventoryItemData itemID)
    {
        foreach (var item in UIItemList)
        {
            if (item.Key.inventoryItemData == itemID)
            {
                SetDescription(item.Key.inventoryItemData);
                SoundManager.Instance.PlaySound(Sound.Open);
            }
        }
        
    }
    public void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable=true;
        canvasGroup.blocksRaycasts=true;
        ResetDescription();
        SoundManager.Instance.PlaySound(Sound.Open);
    }  
    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = false;
        
    }

    

    public void UpdateInventory(List<InventoryItemData> inventoryItemDatas)
    {
       foreach(InventoryItemData item in inventoryItemDatas)
        {
            int i = 0;
            foreach(var item1 in UIItemList)
                if (item == item1.Key.inventoryItemData)
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
            if (!inventoryItemDatas.Contains(item.Key.inventoryItemData))
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
    private void AddNewItem(InventoryItemData item)
    {
        InventoryItem newItem = Instantiate(DefaultItem, Vector3.zero, Quaternion.identity);
        newItem.transform.SetParent(controlPanel);
        newItem.inventoryItemData = item;
        UIItemList.Add(newItem,item);
        newItem.SetData(item);
        newItem.OnButtonPressed += OnInventoryItemButtonPressed;
    }

    protected void ShowSureBox()
    {
        SureBox.alpha = 1;
        SureBox.interactable = true;
        SureBox.blocksRaycasts = true;
        SurelySellButton.gameObject.SetActive(true);
    }

    
    public void HideSureBox()
    {
        SureBox.alpha = 0;
        SureBox.interactable = true;
        SureBox.blocksRaycasts = false;

    }
    public void CalculateAmount(int amount)
    {
        calculatedAmount.text = amount.ToString();
    }
    public void HideSureBoxButton()
    {
        SurelySellButton.gameObject.SetActive(false);
    }
}
