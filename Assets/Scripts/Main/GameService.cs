using UnityEngine;

public class GameService : MonoBehaviour
{
    [SerializeField] private MoneyManagerUI moneyManagerUI;
    [SerializeField] private AudioSource SoundSFX;
    [SerializeField] private SoundTypes[] soundTypes;

    [SerializeField] private InventoryModel playerInventoryModel;
    [SerializeField] private InventoryModel shopInventoryModel;

    [SerializeField] private PlayerInventoryViewUI playerInventoryView;
    [SerializeField] private ShopInventoryViewUI ShopinventoryView;
    public SoundService SoundService { get; private set; }
    public MoneyService MoneyService {  get; private set; }

    private InventoryController inventoryController;
    private void Start()
    {
        SoundService = new SoundService(SoundSFX,soundTypes);
        MoneyService=new MoneyService(moneyManagerUI);
        inventoryController = new InventoryController(playerInventoryView, ShopinventoryView, playerInventoryModel, shopInventoryModel);
        InjectDependencies();
    }

    private void InjectDependencies()
    {
        inventoryController.Init(SoundService, MoneyService);
    }
}
