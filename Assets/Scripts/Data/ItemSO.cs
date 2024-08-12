using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="newItem",menuName ="ScriptableObjects/Item/Item_Object")]
public class ItemSO : ScriptableObject
{
    [SerializeField] private Sprite image;
    [SerializeField] private string itemName;
    [SerializeField] [field: TextArea] private string description;

    public Sprite Image => image;
    public string ItemName => itemName;
    public string Description => description;

}
