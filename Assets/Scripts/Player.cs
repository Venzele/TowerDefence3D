using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _startMoney;
    [SerializeField] private int _startHealth;
    [SerializeField] private MainMenuButton _mainMenuButton;
    [SerializeField] private Menu _menu;

    private bool _isAlife;

    public int StartMoney => _startMoney;
    public int StartHealth => _startHealth;
    public int Money { get; private set; }
    public int Health { get; private set; }

    public event UnityAction<int> MoneyChanged;
    public event UnityAction<int> HealthChanged;
    public event UnityAction Destroyed;

    private void OnEnable()
    {
        _isAlife = true;
        Money = _startMoney;
        Health = _startHealth;
        _mainMenuButton.Opened += OnSetStartParameters;
    }

    private void OnDisable()
    {
        _mainMenuButton.Opened -= OnSetStartParameters;
    }

    private void OnValidate()
    {
        if (_startMoney < 0)
            _startMoney = 0;

        if (_startHealth <= 0)
            _startHealth = 1;
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

    public void OnSetStartParameters()
    {
        _isAlife = true;
        Money = _startMoney;
        Health = _startHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            Health -= damage;
            HealthChanged?.Invoke(Health);

            if (Health <= 0 && _isAlife)
            {
                _menu.OpenPanel();
                _menu.SetLabel("You Lose");
                _isAlife = false;
                Destroyed?.Invoke();
            }
        }
    }
}
