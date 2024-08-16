using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="newItem",menuName ="ScriptableObjects/Item/Item_Object")]
public class ItemSO : ScriptableObject
{
    [field: SerializeField]public bool isStackable {  get; set; }
    [field: SerializeField] public int MaxStackableSize { get; set; } = 1;
    [field: SerializeField] public Sprite image { get; set; }
    [field: SerializeField] public string itemName { get; set; }
    [field: SerializeField] public int id;//=>GetInstanceID();
    [field: SerializeField] 
    [field: TextArea]
    public string description { get; set; }
    [field: TextArea]
    public string rarity;
    [field: SerializeField]public int MoneyAmount;
    public void SetName(string name)
    {
        this.itemName = name;
    }
}
