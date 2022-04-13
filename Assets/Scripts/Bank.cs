using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bank : MonoBehaviour
{
    [SerializeField] private int _startMoney;
    [SerializeField] private MainMenuButton _mainMenuButton;

    public int StartMoney => _startMoney;

    public int Money { get; private set; }

    public event UnityAction<int> MoneyChanged;

    private void OnEnable()
    {
        Money = _startMoney;
        _mainMenuButton.Opened += ResetMoney;
    }

    private void OnDisable()
    {
        _mainMenuButton.Opened -= ResetMoney;
    }

    private void OnValidate()
    {
        if (_startMoney < 0)
            _startMoney = 0;
    }

    public void ResetMoney()
    {
        Money = _startMoney;
    }

    public void AddCoins(int money)
    {
        if (money > 0)
        {
            Money += money;
            MoneyChanged?.Invoke(Money);
        }
    }

    public void BuyTower(int price)
    {
        if (Money >= price)
        {
            Money -= price;
            MoneyChanged?.Invoke(Money);
        }
    }
}
