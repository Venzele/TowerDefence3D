using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Start _startButton;
    [SerializeField] private MainMenuButton _mainMenuButton;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private InfoLevel _infoLevel;

    private int _currentLevel;

    private void OnEnable()
    {
        _spawner.AllEnemiesDied += OnEnableButton;
        _startButton.Started += OnDisableButton;
        _nextLevelButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _spawner.AllEnemiesDied -= OnEnableButton;
        _startButton.Started -= OnDisableButton;
        _nextLevelButton.onClick.RemoveListener(OnButtonClick);
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
        _nextLevelButton.gameObject.SetActive(true);
        _currentLevel = _spawner.CurrentLevel;
    }

    private void OnDisableButton()
    {
        _nextLevelButton.gameObject.SetActive(false);
    }
}
