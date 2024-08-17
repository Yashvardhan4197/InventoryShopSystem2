using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="newItem",menuName ="ScriptableObjects/Item/Item_Object")]
public class ItemData : ScriptableObject
{

    public Sprite image;
    public string itemName;
    public int id;

    [field: TextArea]
    public string description;
    [field: TextArea]
    public string rarity;
    public int MoneyAmount;
    public void SetName(string name)
    {
        this.itemName = name;
    }
}
