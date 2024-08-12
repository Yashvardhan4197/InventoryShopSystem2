using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
public class InventoryItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Amount;
    [SerializeField] public int inventoryitemID;
    [SerializeField] public Image ItemImage;
    [SerializeField] public Button ItemButton;

    [SerializeField] public Sprite defaultSprite;
    public bool isEmpty = false;

    public event UnityAction<int> OnButtonPressed;
    public void SetData(int id,Sprite image,int amount)
    {
        inventoryitemID = id;
        ItemImage.gameObject.SetActive(true);
        ItemImage.sprite=image;
        Amount.text = amount.ToString();
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
        OnButtonPressed?.Invoke(inventoryitemID);
        //InventoryController.Instance.ButtonPressInfo(inventoryitemID);
    }
}
