using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{


    [SerializeField]public UIInventoryDescription inventoryDescription;
    [SerializeField]public UIInventoryPage inventoryPage;
    [SerializeField]public InventorySO inventorySO;

    [SerializeField] public Button InventoryOpenButton;


    private bool isOpened = false;
    private void Start()
    {
        InventoryOpenButton.onClick.AddListener(OpenInventory);
        inventorySO.Initialize();
        inventoryPage.InitializeItems(inventorySO.TotalSlots);
        inventorySO.AddItem(inventorySO.StartingItem1, inventorySO.StartingItemAmount1);
        inventorySO.AddItem(inventorySO.StartingItem2, inventorySO.StartingItemAmount2);
        //inventorySO.AddItem(inventorySO.StartingItem3,inventorySO.StartingItemAmount3);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)&&isOpened==false)
        {
            OpenInventory();
        }

    }

    public void ButtonPressInfo(int itemID)
    {
        foreach (var item in inventorySO.GetInventoryItemData())
        {
            if (item.Key == itemID)
            {
                inventoryDescription.SetDescription(item.Key,item.Value.item.image, item.Value.item.name, item.Value.item.description);
            }
        }
    }
    public void OpenInventory()
    {
        if(!isOpened)
        {
            inventoryPage.Show();
            foreach (var item in inventorySO.GetInventoryItemData())
            {
                Debug.Log("item Name: " + item.Value.item.name);
                inventoryPage.UpdateInventory(item.Key, item.Value.item.image, item.Value.quantity);
            }

            isOpened = true;
        }
        else
        {
            inventoryPage.Hide();
            isOpened = false;
        }
    }

    public void UseItemButtonPressed(int id)
    {
        foreach (var item in inventorySO.GetInventoryItemData())
        {
            if (item.Key == id)
            {
                int changedValue=item.Value.quantity;
                changedValue--;
                item.Value.ChangeQuantity(changedValue);
                //Debug.Log("Hello Again" + item.Value.quantity);
                if (changedValue <= 0)
                {
                    item.Value.ResetItemSlot();
                    inventoryPage.UpdateInventory(id,inventoryPage.DefaultItem.defaultSprite,0);
                }
                else
                {
                    inventoryPage.UpdateInventory(id, item.Value.item.image, item.Value.quantity);
                }
                return;
            }
        }
        
    }

    public void UpdateFullInventory()
    {
        foreach (var item in inventorySO.GetInventoryItemData())
        {
            //Debug.Log("ItemsUpdated");
            inventoryPage.UpdateInventory(item.Key, item.Value.item.image, item.Value.quantity);
        }
    }
    

}
