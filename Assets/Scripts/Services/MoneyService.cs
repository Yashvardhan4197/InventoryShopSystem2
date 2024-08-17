
public class MoneyService
{
    private int moneyAmount;
    private MoneyManagerUI playerManagerUI;


    public MoneyService(MoneyManagerUI moneyManagerUI)
    {
        playerManagerUI = moneyManagerUI;
        SetMoneyAmount(100);
    }
    public int GetMoneyAmount()
    {
        return moneyAmount;
    }
    public void SetMoneyAmount(int moneyAmount)
    {
        this.moneyAmount = moneyAmount;
        playerManagerUI.UpdateMoneyAmount(moneyAmount);
    }

}
