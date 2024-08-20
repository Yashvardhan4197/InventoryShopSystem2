using UnityEngine;

public class GameService : MonoBehaviour
{
    [SerializeField] private MoneyManagerUI moneyManagerUI;
    [SerializeField] private AudioSource soundSFX;
    [SerializeField] private SoundTypes[] soundTypes;

    [SerializeField] private InventoryModel playerInventoryModel;
    [SerializeField] private InventoryModel shopInventoryModel;

    [SerializeField] private PlayerInventoryViewUI playerInventoryView;
    [SerializeField] private ShopInventoryViewUI ShopinventoryView;
    private SoundService soundService;
    private MoneyService moneyService;

    private PlayerInventoryController playerInventoryController;
    private ShopInventoryController shopInventoryController;
    private void Start()
    {
        soundService = new SoundService(soundSFX,soundTypes);
        moneyService=new MoneyService(moneyManagerUI);
        playerInventoryController = new PlayerInventoryController(playerInventoryView, playerInventoryModel);
        shopInventoryController=new ShopInventoryController(ShopinventoryView,shopInventoryModel);
        InjectDependencies();
    }

    private void InjectDependencies()
    {
        playerInventoryController.Init(soundService, moneyService);
        shopInventoryController.Init(soundService, moneyService);
    }
}
