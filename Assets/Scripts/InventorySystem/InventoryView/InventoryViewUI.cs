using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class InventoryViewUI : MonoBehaviour
{
    [SerializeField] private InventoryItem DefaultItem;
    [SerializeField] private RectTransform controlPanel;

    private Dictionary<InventoryItem,InventoryItemData>UIItemList = new Dictionary<InventoryItem,InventoryItemData>();
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image Image;
    [SerializeField] private TextMeshProUGUI Title;
    [SerializeField] private TextMeshProUGUI Description;
    [SerializeField] private TextMeshProUGUI Rarity;
    [SerializeField] private TextMeshProUGUI priceText;

    protected InventoryItemData inventoryItemData;
    [SerializeField] protected CanvasGroup SureBox;
    [SerializeField] protected Button CloseSureBox;
    [SerializeField] protected Button SurelySellButton;
    [SerializeField] protected TextMeshProUGUI SureBoxText;
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
            if (item.Key.GetInventoryItemData() == itemID)
            {
                SetDescription(item.Key.GetInventoryItemData());
                GameService.Instance.SoundService.PlaySound(Sound.Open);
            }
        }
        
    }
    public void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable=true;
        canvasGroup.blocksRaycasts=true;
        ResetDescription();
        GameService.Instance.SoundService.PlaySound(Sound.Open);
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
    private void AddNewItem(InventoryItemData item)
    {
        InventoryItem newItem = Instantiate(DefaultItem, Vector3.zero, Quaternion.identity);
        newItem.transform.SetParent(controlPanel);
        newItem.SetInventoryItemData(item);
        UIItemList.Add(newItem,item);
        newItem.SetData(item);
        newItem.OnButtonPressed += OnInventoryItemButtonPressed;
    }

    public void ShowSureBox(InventoryItemData inventoryItemData)
    {
        int temp = 0;
        SureBox.alpha = 1;
        SureBox.interactable = true;
        SureBox.blocksRaycasts = true;
        SurelySellButton.gameObject.SetActive(true);
        calculatedAmount.text = temp.ToString();
        SureBoxText.text= GetSureBoxText(inventoryItemData).text;
    }
    public abstract TextMeshProUGUI GetSureBoxText(InventoryItemData inventoryItemData);
    
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

    protected bool CheckItemAvailability()
    {
        string temp = moneyText.text.ToString();
        if (int.TryParse(temp, out int amountToSell))
        {
            Debug.Log("Money: " + amountToSell);
            return true;
        }
        else
        {
            Debug.LogError("Invalid number in moneyText." + moneyText.text);
            GameService.Instance.SoundService.PlaySound(Sound.Deny);
        }
        return false;
    }
    protected int GetItemIfAvailable()
    {
        int temp = int.Parse(moneyText.text.ToString());
        return temp;

    }
}
