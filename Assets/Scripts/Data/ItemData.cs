using UnityEngine;

[CreateAssetMenu(fileName ="newItem",menuName ="ScriptableObjects/Item/Item_Object")]
public class ItemData : ScriptableObject
{

    public Sprite Image;
    public string ItemName;
    public int id;
    [TextArea]
    public string Description;
    [TextArea]
    public string Rarity;
    public int MoneyAmount;

    public void SetName(string name)
    {
        this.ItemName = name;
    }
}
