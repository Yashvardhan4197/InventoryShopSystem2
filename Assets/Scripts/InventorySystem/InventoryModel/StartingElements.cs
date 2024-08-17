using System;

[Serializable]
public class StartingElements
{
    public ItemData item;
    public int quantity;

    public StartingElements(ItemData item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}
