using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogicScript : MonoBehaviour
{
    [SerializeField]
    private int money;
    public int Money
    {
        get { return money; }
        set { SetMoney(value); }
    }

    [SerializeField] private TextMeshProUGUI cashDisplay;

    private void Start()
    {
        SetMoney(money);
    }

    private void SetMoney(int amount)
    {
        money = amount;

        cashDisplay.text = "$ " + money.ToString();
    }
}
