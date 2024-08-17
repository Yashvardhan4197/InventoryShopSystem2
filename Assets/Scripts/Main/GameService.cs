using UnityEngine;

public class GameService:GenericMonoSingleton<GameService>
{
    [SerializeField]private MoneyManagerUI moneyManagerUI;
    [SerializeField]private AudioSource SoundSFX;
    [SerializeField]private SoundTypes[] soundTypes;
    public SoundService SoundService { get; private set; }
    public MoneyService MoneyService {  get; private set; }

    private void Start()
    {
        SoundService = new SoundService(SoundSFX,soundTypes);
        MoneyService=new MoneyService(moneyManagerUI);
    }
}
