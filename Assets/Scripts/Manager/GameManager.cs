using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericMonoSingleton<GameManager>
{
    private int moneyAmount;
    [SerializeField] UIPlayerManager playerManagerUI;

    private void Start()
    {
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
