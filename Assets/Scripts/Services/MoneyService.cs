
public class MoneyService
{
    private int moneyAmount;
    public int MoneyAmount { get { return moneyAmount; } }
    private MoneyManagerUI playerManagerUI;

    public MoneyService(MoneyManagerUI moneyManagerUI)
    {
        playerManagerUI = moneyManagerUI;
        SetMoneyAmount(100);
    }

    public void SetMoneyAmount(int moneyAmount)
    {
        this.moneyAmount = moneyAmount;
        playerManagerUI.UpdateMoneyAmount(moneyAmount);
    }

}
