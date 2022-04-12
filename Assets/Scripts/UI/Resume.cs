using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Resume : MonoBehaviour
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Menu _menu;
    [SerializeField] private MainMenuButton _mainMenuButton;
    [SerializeField] private Pause _pauseButton;

    public event UnityAction Resumed;

    private void OnEnable()
    {
        _pauseButton.Paused += OnEnableButton;
        _mainMenuButton.Opened += OnDisableButton;
        _resumeButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _pauseButton.Paused -= OnEnableButton;
        _mainMenuButton.Opened -= OnDisableButton;
        _resumeButton.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        Resumed?.Invoke();
        _menu.ClosePanel();
        OnDisableButton();
    }

    private void OnEnableButton()
    {
        _resumeButton.gameObject.SetActive(true);
    }

    private void OnDisableButton()
    {
        _resumeButton.gameObject.SetActive(false);
    }
}
