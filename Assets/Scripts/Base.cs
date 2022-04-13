using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Base : MonoBehaviour
{
    [SerializeField] private int _startHealth;
    [SerializeField] private MainMenuButton _mainMenuButton;
    [SerializeField] private Menu _menu;

    private bool _isAlife;

    public int StartHealth => _startHealth;
    public int Health { get; private set; }

    public event UnityAction<int> HealthChanged;
    public event UnityAction Destroyed;

    private void OnEnable()
    {
        _isAlife = true;
        Health = _startHealth;
        _mainMenuButton.Opened += ResetHealth;
    }

    private void OnDisable()
    {
        _mainMenuButton.Opened -= ResetHealth;
    }

    private void OnValidate()
    {
        if (_startHealth <= 0)
            _startHealth = 1;
    }

    public void ResetHealth()
    {
        _isAlife = true;
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
