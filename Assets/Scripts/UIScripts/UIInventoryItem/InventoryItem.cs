using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
public class InventoryItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Amount;
    [SerializeField] private Image ItemImage;
    [SerializeField] private Button ItemButton;

    [SerializeField] private Sprite defaultSprite;

    private InventoryItemData inventoryItemData;
    public event UnityAction<InventoryItemData> OnButtonPressed;
    public void SetData(InventoryItemData inventoryItem)
    {
        inventoryItemData = inventoryItem;
        ItemImage.gameObject.SetActive(true);
        ItemImage.sprite = inventoryItem.item.image;
        Amount.text = inventoryItem.quantity.ToString();
    }
    public InventoryItemData GetInventoryItemData()
    {
        return inventoryItemData;
    }
    public void SetInventoryItemData(InventoryItemData inventoryItemData)
    {
        this.inventoryItemData=inventoryItemData;
    }

    private void Awake()
    {
        ItemImage.gameObject.SetActive(true);
        ItemButton.onClick.AddListener(ButtonPressed);
    }
    private void ButtonPressed()
    {
        OnButtonPressed?.Invoke(inventoryItemData);
    }
}
