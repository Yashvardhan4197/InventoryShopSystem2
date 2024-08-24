using System;

[Serializable]
public class InventoryItemData
{
    public ItemData item;
    public int quantity;
    public int MaximumQuantity;

    public InventoryItemData(ItemData item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
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
        quantity = -1;

    }
}

