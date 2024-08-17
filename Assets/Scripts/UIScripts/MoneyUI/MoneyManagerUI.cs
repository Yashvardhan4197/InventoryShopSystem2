using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManagerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyAmount;
    public void UpdateMoneyAmount(int money)
    {
        moneyAmount.text = money.ToString();
    }
}
