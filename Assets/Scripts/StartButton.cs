using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] private Surface _surface;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Button _startButton;
    [SerializeField] private CanvasGroup _canvasGroup;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(StartGame);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(StartGame);
    }

    private void StartGame()
    {
        _canvasGroup.interactable = false;
        _canvasGroup.alpha = 0;
        _surface.CreateGround();
        _spawner.StartSpawn();
    }
}
