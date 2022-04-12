using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Menu _menu;
    [SerializeField] private Menu _mainMenu;

    public event UnityAction Opened;

    private void OnEnable()
    {
        _mainMenuButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _mainMenuButton.onClick.RemoveListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        Opened?.Invoke();
        _menu.ClosePanel();
        _mainMenu.OpenPanel();
    }
}
