using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
public class InventoryItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Amount;
    //[SerializeField] int inventoryitemID;
    [SerializeField] public Image ItemImage;
    [SerializeField] public Button ItemButton;

    [SerializeField] public Sprite defaultSprite;
    public bool isEmpty = false;

    public InventoryItemData inventoryItemData;
    public event UnityAction<InventoryItemData> OnButtonPressed;
    public void SetData(InventoryItemData inventoryItem)
    {
        inventoryItemData = inventoryItem;
        ItemImage.gameObject.SetActive(true);
        ItemImage.sprite = inventoryItem.item.image;
        Amount.text = inventoryItem.quantity.ToString();
        isEmpty = false;
    }



    private void Awake()
    {
        ItemImage.gameObject.SetActive(true);
        isEmpty = true;
        ItemButton.onClick.AddListener(ButtonPressed);
    }
    private void ButtonPressed()
    {
        OnButtonPressed?.Invoke(inventoryItemData);
        //InventoryController.Instance.ButtonPressInfo(inventoryitemID);
    }
}
