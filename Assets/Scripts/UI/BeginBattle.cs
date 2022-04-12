using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginBattle : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Player _player;
    [SerializeField] private Button _beginBattleButton;
    [SerializeField] private Start _startButton;
    [SerializeField] private MainMenuButton _mainMenuButton;
    [SerializeField] private Pause _pauseButton;
    [SerializeField] private Resume _resumeButton;

    private void OnEnable()
    {
        _spawner.AllEnemiesDied += OnDeactivateButton;
        _player.Destroyed += OnDeactivateButton;
        _pauseButton.Paused += OnDeactivateButton;
        _mainMenuButton.Opened += OnActivateButton;
        _mainMenuButton.Opened += OnDisableButton;
        _startButton.Started += OnEnableButton;
        _resumeButton.Resumed += OnActivateButton;
        _beginBattleButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _spawner.AllEnemiesDied -= OnDeactivateButton;
        _player.Destroyed -= OnDeactivateButton;
        _pauseButton.Paused -= OnDeactivateButton;
        _mainMenuButton.Opened -= OnActivateButton;
        _mainMenuButton.Opened -= OnDisableButton;
        _startButton.Started -= OnEnableButton;
        _resumeButton.Resumed -= OnActivateButton;
        _beginBattleButton.onClick.RemoveListener(OnButtonClick);
    }

    private void OnEnableButton()
    {
        _beginBattleButton.gameObject.SetActive(true);
    }

    private void OnDisableButton()
    {
        _beginBattleButton.gameObject.SetActive(false);
    }

    private void OnButtonClick()
    {
        _beginBattleButton.gameObject.SetActive(false);
        _spawner.StartSpawn();
    }

    private void OnActivateButton()
    {
        _beginBattleButton.interactable = true;
    }

    private void OnDeactivateButton()
    {
        _beginBattleButton.interactable = false;
    }
}
