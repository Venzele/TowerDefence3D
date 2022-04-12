using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Start _startButton;
    [SerializeField] private MainMenuButton _mainMenuButton;
    [SerializeField] private Player _player;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private InfoLevel _infoLevel;

    private int _currentLevel;

    private void OnEnable()
    {
        _player.Destroyed += OnEnableButton; 
        _startButton.Started += OnDisableButton;
        _restartButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _player.Destroyed -= OnEnableButton;
        _startButton.Started -= OnDisableButton;
        _restartButton.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        OnDisableButton();
        _mainMenuButton.OnButtonClick();
        _startButton.OnButtonClick();
        _spawner.SetLevel(_currentLevel);
        _infoLevel.ShowLevel(_currentLevel);
    }

    private void OnEnableButton()
    {
        _restartButton.gameObject.SetActive(true);
        _currentLevel = _spawner.CurrentLevel;
    }

    private void OnDisableButton()
    {
        _restartButton.gameObject.SetActive(false);
    }
}
