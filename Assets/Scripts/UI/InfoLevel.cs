using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoLevel : MonoBehaviour
{
    [SerializeField] private TMP_Text _level;
    [SerializeField] private GameObject _panelLevel;
    [SerializeField] private Start _startButton;
    [SerializeField] private MainMenuButton _mainMenuButton;

    private void OnEnable()
    {
        _startButton.Started += OnEnablePanel;
        _mainMenuButton.Opened += OnDisablePanel;
    }

    private void OnDisable()
    {
        _startButton.Started -= OnEnablePanel;
        _mainMenuButton.Opened -= OnDisablePanel;
    }

    public void ShowLevel(int level)
    {
        _level.text = (level + 1).ToString();
    }

    private void OnEnablePanel()
    {
        _panelLevel.SetActive(true);
    }

    private void OnDisablePanel()
    {
        _panelLevel.SetActive(false);
    }
}
