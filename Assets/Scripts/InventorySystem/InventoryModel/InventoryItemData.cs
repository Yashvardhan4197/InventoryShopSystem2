using System;

[Serializable]
public class InventoryItemData
{
    public ItemData item;
    public int quantity;
    public int itemID = -1;
    public InventoryItemData(ItemData item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
        if (item != null)
        {
            this.itemID = item.id;
        }
        else
        {
            itemID = -1;
        }
    }
    public bool isEmpty()
    {
        if (item == null || quantity == 0)
        {
            return true;
        }
        return false;
    }

    public void ChangeQuantity(int quantity)
    {
        this.quantity = quantity;

    }
    public void ResetItemSlot()
    {
        item = null;
        itemID = -1;
        quantity = -1;

    }
}

