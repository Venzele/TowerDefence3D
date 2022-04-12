using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Start : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Menu _mainMenu;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private InfoLevel _infoLevel;

    public event UnityAction Started;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        Started?.Invoke();
        _mainMenu.ClosePanel();
        _spawner.SetLevel(0);
        _infoLevel.ShowLevel(0);
    }
}
