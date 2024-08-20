
using UnityEngine.Events;


public abstract class InventoryController
{
     
    public SoundService soundService;
    public MoneyService moneyService;

    protected static UnityAction<InventoryItemData, int> SoldItemEvent;
    protected static UnityAction<InventoryItemData, int> BoughtItemEvent;

    public abstract void Init(SoundService soundService, MoneyService moneyService);
    
    protected abstract void Initialize();
    
    protected abstract void InitializeStartingItems();
        
    protected void TriggerSoldItemEvent(InventoryItemData inventoryItemData,int amountToSell)
    {
        SoldItemEvent?.Invoke(inventoryItemData, amountToSell);
    }

    protected void TriggerBoughtItemEvent(InventoryItemData inventoryItemData,int amountToSell)
    {
        BoughtItemEvent?.Invoke(inventoryItemData, amountToSell);
    }
    
    
}