using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIInventoryItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Amount;
    [SerializeField] public Image ItemImage;
    [SerializeField] public Button ItemButton;
    [SerializeField] public Sprite defaultSprite;

    InventoryItemData itemData;

    public event UnityAction<UIInventoryItem> OnButtonPressed;

    private void Start()
    {
        ItemImage.gameObject.SetActive(true);
        ItemButton.onClick.AddListener(ButtonPressed);
    }

    private void OnDestroy()
    {
        ItemButton.onClick.RemoveListener(ButtonPressed);
        this.itemData.OnQuantityUpdated -= UpdateQuantity;
    }

    public void Initialize(InventoryItemData itemData)
    {
        ItemImage.gameObject.SetActive(true);
        ItemImage.sprite = itemData.Item.Image;
        Amount.text = itemData.Quantity.ToString();
        itemData.OnQuantityUpdated += UpdateQuantity;
        this.itemData = itemData;
    }

    public void UpdateQuantity(int oldValue, int newValue)
    {
        Amount.text = newValue.ToString();
    }

    private void ButtonPressed()
    {
        OnButtonPressed?.Invoke(this);
    }
}
