using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surface : MonoBehaviour
{
    [SerializeField] private Vector3 _startPositionGround;
    [SerializeField] private int _numberRow;
    [SerializeField] private int _numberColumn;
    [SerializeField] private Ground _ground;
    [SerializeField] private Way _way;

    private List<Ground> _grounds = new List<Ground>();
    
    public int NumberRow => _numberRow;
    public int NumberColumn => _numberColumn;

    public void CreateGround()
    {
        int positionInColumn = 0;
        int Column = 0;

        for (int i = 0; i < _numberColumn * _numberRow; i++)
        {
            Vector3 nextPositionGround = new Vector3(positionInColumn, 0, Column);
            Column++;

            Ground newGround = Instantiate(_ground, _startPositionGround + nextPositionGround, Quaternion.identity);
            _grounds.Add(newGround);

            if (Column >= _numberRow)
            {
                Column = 0;
                positionInColumn++;
            }
        }

        _way.CreatePath();
    }

    public Ground FindGround(int index)
    {
        return _grounds[index];
    }
}
