using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private TMP_Text _label;

    public void OpenPanel()
    {
        _menu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ClosePanel()
    {
        _menu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SetLabel(string label)
    {
        _label.text = label;
    }
}
