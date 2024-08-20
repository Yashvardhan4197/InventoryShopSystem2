using TMPro;
using UnityEngine;

public class MoneyManagerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyAmount;

    public void UpdateMoneyAmount(int money)
    {
        moneyAmount.text = money.ToString();
    }
}
