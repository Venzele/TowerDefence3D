using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Player _player;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Menu _menu;
    [SerializeField] private Start _startButton;
    [SerializeField] private MainMenuButton _mainMenuButton;
    [SerializeField] private Resume _resumeButton;

    public event UnityAction Paused;

    private void OnEnable()
    {
        _spawner.AllEnemiesDied += OnDeactivateButton;
        _player.Destroyed += OnDeactivateButton;
        _mainMenuButton.Opened += OnActivateButton;
        _mainMenuButton.Opened += OnDisableButton;
        _startButton.Started += OnEnableButton;
        _resumeButton.Resumed += OnActivateButton;
        _pauseButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _spawner.AllEnemiesDied -= OnDeactivateButton;
        _player.Destroyed -= OnDeactivateButton;
        _mainMenuButton.Opened -= OnActivateButton;
        _mainMenuButton.Opened -= OnDisableButton;
        _startButton.Started -= OnEnableButton;
        _resumeButton.Resumed -= OnActivateButton;
        _pauseButton.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _menu.OpenPanel();
        _menu.SetLabel("Pause");
        Paused?.Invoke();
        Time.timeScale = 0;
        _pauseButton.interactable = false;
    }

    private void OnEnableButton()
    {
        _pauseButton.gameObject.SetActive(true);
    }

    private void OnDisableButton()
    {
        _pauseButton.gameObject.SetActive(false);
    }

    private void OnActivateButton()
    {
        _pauseButton.interactable = true;
    }
    private void OnDeactivateButton()
    {
        _pauseButton.interactable = false;
    }
}
