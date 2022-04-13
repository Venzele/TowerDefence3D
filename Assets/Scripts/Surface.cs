using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surface : MonoBehaviour
{
    [SerializeField] private Vector3 _startPositionGround;
    [SerializeField] private int _numberRow, _numberColumn;
    [SerializeField] private Ground _ground;
    [SerializeField] private Way _way;
    [SerializeField] private Forest _forest;
    [SerializeField] private Start _startButton;
    [SerializeField] private GameObject _containerGround;
    [SerializeField] private MainMenuButton _mainMenuButton;

    private List<Ground> _grounds = new List<Ground>();
    
    public int NumberRow => _numberRow;
    public int NumberColumn => _numberColumn;
    public Vector3 StartPositionGround => _startPositionGround;

    private void OnEnable()
    {
        _startButton.Started += CreateGround;
        _mainMenuButton.Opened += RemoveGrounds;
    }

    private void OnDisable()
    {
        _startButton.Started -= CreateGround;
        _mainMenuButton.Opened -= RemoveGrounds;
    }

    public Ground FindGround(int index)
    {
        return _grounds[index];
    }

    public int GetMaxGround()
    {
        return _grounds.Count;
    }

    private void CreateGround()
    {
        int positionInColumn = 0;
        int Column = 0;

        for (int i = 0; i < _numberColumn * _numberRow; i++)
        {
            Vector3 nextPositionGround = new Vector3(positionInColumn, 0, Column);
            Column++;

            Ground newGround = Instantiate(_ground, _startPositionGround + nextPositionGround, Quaternion.identity, _containerGround.transform);
            _grounds.Add(newGround);

            if (Column >= _numberRow)
            {
                Column = 0;
                positionInColumn++;
            }
        }

        _way.CreatePath();
        _forest.Create();
    }

    private void RemoveGrounds()
    {
        foreach (var item in _grounds)
        {
            Destroy(item.gameObject);
        }

        _grounds.Clear();
    }
}
