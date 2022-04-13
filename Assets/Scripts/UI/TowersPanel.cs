using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowersPanel : MonoBehaviour
{
    [SerializeField] private ItemData[] _itemDatas;
    [SerializeField] private TowerSetter _towerSetter;
    [SerializeField] private TowerView _itemTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private Base _base;
    [SerializeField] private Bank _bank;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Start _startButton;
    [SerializeField] private MainMenuButton _mainMenuButton;
    [SerializeField] private Pause _pauseButton;
    [SerializeField] private Resume _resumeButton;

    private List<TowerView> _items = new List<TowerView>();

    private void OnEnable()
    {
        _spawner.AllEnemiesDied += OnDeactivatePanel;
        _base.Destroyed += OnDeactivatePanel;
        _bank.MoneyChanged += OnChangeItemColor;
        _pauseButton.Paused += OnDeactivatePanel;
        _startButton.Started += OnEnablePanel;
        _resumeButton.Resumed += OnActivatePanel;
        _mainMenuButton.Opened += OnDisablePanel;
    }

    private void OnDisable()
    {
        _spawner.AllEnemiesDied -= OnDeactivatePanel;
        _base.Destroyed -= OnDeactivatePanel;
        _bank.MoneyChanged -= OnChangeItemColor;
        _pauseButton.Paused -= OnDeactivatePanel;
        _startButton.Started -= OnEnablePanel;
        _resumeButton.Resumed -= OnActivatePanel;
        _mainMenuButton.Opened -= OnDisablePanel;
    }

    private void Start()
    {
        for (int i = 0; i < _itemDatas.Length; i++)
            AddItem(_itemDatas[i]);
    }

    private void AddItem(ItemData itemData)
    {
        TowerView TowerView = Instantiate(_itemTemplate, _container).GetComponent<TowerView>();
        TowerView.Initialize(itemData);
        TowerView.ItemSelected += OnItemSelected;
        TowerView.ItemDisabled += OnItemDisabled;
        _items.Add(TowerView);
    }

    private void OnItemSelected(ItemData itemData)
    {
        if (itemData.Price <= _bank.Money)
        {
            _towerSetter.StartSetTower(itemData);
        }
    }

    private void OnItemDisabled(TowerView TowerView)
    {
        TowerView.ItemSelected -= OnItemSelected;
        TowerView.ItemDisabled -= OnItemDisabled;
    }

    private void OnEnablePanel()
    {
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1;
        OnChangeItemColor(_bank.StartMoney);
    }

    private void OnDisablePanel()
    {
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0;
    }

    private void OnActivatePanel()
    {
        _canvasGroup.blocksRaycasts = true;
    }

    private void OnDeactivatePanel()
    {
        _canvasGroup.blocksRaycasts = false;
    }

    private void OnChangeItemColor(int money)
    {
        for (int i = 0; i < _itemDatas.Length; i++)
        {
            if (money >= _itemDatas[i].Price)
                _items[i].ChangeOnCanBuyColor();
            else
                _items[i].ChangeOnCannotBuyColor();
        }
    }
}
