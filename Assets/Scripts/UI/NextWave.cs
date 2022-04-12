using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextWave : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Player _player;
    [SerializeField] private Button _nextWaveButton;
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
        _resumeButton.Resumed += OnActivateButton;
        _spawner.WaveFinished += OnEnableButton;
        _spawner.WaveStarted += OnDisableButton;
        _nextWaveButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _spawner.AllEnemiesDied -= OnDeactivateButton;
        _player.Destroyed -= OnDeactivateButton;
        _pauseButton.Paused -= OnDeactivateButton;
        _mainMenuButton.Opened -= OnActivateButton;
        _mainMenuButton.Opened -= OnDisableButton;
        _resumeButton.Resumed -= OnActivateButton;
        _spawner.WaveFinished -= OnEnableButton;
        _spawner.WaveStarted -= OnDisableButton;
        _nextWaveButton.onClick.RemoveListener(OnButtonClick);
    }

    private void OnEnableButton()
    {
        _nextWaveButton.gameObject.SetActive(true);
    }

    private void OnDisableButton()
    {
        _nextWaveButton.gameObject.SetActive(false);
    }

    private void OnButtonClick()
    {
        _nextWaveButton.gameObject.SetActive(false);
        _spawner.StartNextWave();
    }

    private void OnActivateButton()
    {
        _nextWaveButton.interactable = true;
    }

    private void OnDeactivateButton()
    {
        _nextWaveButton.interactable = false;
    }
}
