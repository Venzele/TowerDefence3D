using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoBalance : MonoBehaviour
{
    [SerializeField] private TMP_Text _money;
    [SerializeField] private TMP_Text _health;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _panelBalance;
    [SerializeField] private Start _startButton;
    [SerializeField] private MainMenuButton _mainMenuButton;

    private void OnEnable()
    {
        _startButton.Started += OnEnablePanel;
        _mainMenuButton.Opened += OnDisablePanel;
        _player.MoneyChanged += OnMoneyChanged;
        _player.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _startButton.Started -= OnEnablePanel;
        _mainMenuButton.Opened -= OnDisablePanel;
        _player.MoneyChanged -= OnMoneyChanged;
        _player.HealthChanged -= OnHealthChanged;
    }

    private void OnMoneyChanged(int money)
    {
        _money.text = money.ToString();
    }

    private void OnHealthChanged(int health)
    {
        _health.text = health.ToString();

        if (health < 0)
            _health.text = 0.ToString();
    }

    private void OnEnablePanel()
    {
        _panelBalance.SetActive(true);
        _money.text = _player.StartMoney.ToString();
        _health.text = _player.StartHealth.ToString();
    }

    private void OnDisablePanel()
    {
        _panelBalance.SetActive(false);
    }
}
