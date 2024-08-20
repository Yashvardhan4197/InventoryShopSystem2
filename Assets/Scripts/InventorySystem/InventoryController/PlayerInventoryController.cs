
public class PlayerInventoryController: InventoryController
{
    private PlayerInventoryViewUI playerInventoryPage;
    private InventoryModel playerInventoryModel;

    private void BoughtItem(InventoryItemData inventoryItemData, int amountToBuy)
    {
        playerInventoryModel.AddItem(inventoryItemData.item, amountToBuy);
        playerInventoryPage.UpdateInventory(playerInventoryModel.GetInventoryItemData());
    }

    public PlayerInventoryController(PlayerInventoryViewUI playerInventoryPage, InventoryModel playerInventoryModel)
    {
        this.playerInventoryPage = playerInventoryPage;
        this.playerInventoryModel = playerInventoryModel;
    }

    public override void Init(SoundService soundService, MoneyService moneyService)
    {
        this.soundService = soundService;
        this.moneyService = moneyService;
        Initialize();
    }

    public void UseItemButtonPressed(InventoryItemData inventoryItemData)
    {
        foreach (var item in playerInventoryModel.GetInventoryItemData())
        {
            if (item == inventoryItemData)
            {
                int changedValue = item.quantity;
                changedValue--;
                item.ChangeQuantity(changedValue);
                if (changedValue <= 0)
                {
                    item.ResetItemSlot();
                    playerInventoryPage.SetButtonStatus(false);
                    playerInventoryPage.UpdateInventory(playerInventoryModel.GetInventoryItemData());
                }
                else
                {
                    playerInventoryPage.UpdateInventory(playerInventoryModel.GetInventoryItemData());
                }
                soundService.PlaySound(Sound.Open);
                return;
            }
        }

    }

    public void SellItemSurely(InventoryItemData inventoryItemData, int amountToSell)
    {
        if (amountToSell <= inventoryItemData.quantity)
        {
            TriggerSoldItemEvent(inventoryItemData, amountToSell);
            int totalMoneyToUpdate = amountToSell * inventoryItemData.item.MoneyAmount;
            playerInventoryPage.CalculateAmount(totalMoneyToUpdate);
            moneyService.SetMoneyAmount(moneyService.MoneyAmount + totalMoneyToUpdate);
            inventoryItemData.ChangeQuantity(inventoryItemData.quantity - amountToSell);
            playerInventoryPage.UpdateInventory(playerInventoryModel.GetInventoryItemData());
            soundService.PlaySound(Sound.Accept);
            if (inventoryItemData.quantity <= 0)
            {
                playerInventoryPage.StartSureBoxClosingProcess();
            }
        }
        else
        {
            soundService.PlaySound(Sound.Deny);
        }
    }

    protected override void Initialize()
    {
        playerInventoryModel.Initialize();
        InitializeStartingItems();
        playerInventoryPage.SetController(this);
        playerInventoryPage.InitializeItems(playerInventoryModel.GetInventoryItemData());
        playerInventoryPage.UseItemEvent += UseItemButtonPressed;
        playerInventoryPage.SellItemEventSureBox += SellItemSurely;
        BoughtItemEvent += BoughtItem;
    }

    protected override void InitializeStartingItems()
    {
        foreach (var item in playerInventoryModel.GetStartingItemData())
        {
            playerInventoryModel.AddItem(item.item, item.quantity);
        }
    }

    ~PlayerInventoryController()
    {
        playerInventoryPage.UseItemEvent -= UseItemButtonPressed;
        playerInventoryPage.SellItemEventSureBox -= SellItemSurely;
    }

}
