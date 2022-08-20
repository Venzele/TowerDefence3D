using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Way : MonoBehaviour
{
    [SerializeField] private Surface _surface;
    [SerializeField] private MainMenuButton _mainMenuButton;

    private List<Ground> _way = new List<Ground>();
    private Ground _startRoad;

    public Ground StartRoad => _startRoad;

    private void OnEnable()
    {
        _mainMenuButton.Opened += RemovePath;
    }

    private void OnDisable()
    {
        _mainMenuButton.Opened -= RemovePath;
    }

    public void CreatePath()
    {
        int indexGround = Random.Range(1, _surface.NumberRow - 1);
        _startRoad = _surface.Grounds[indexGround];
        _startRoad.ChangeOnRoad();
        _way.Add(_startRoad);
        Ground nextRoad = _startRoad;
        List<int> nextIndexesRoad = new List<int>();

        while (nextRoad.transform.position.x < _startRoad.transform.position.x + _surface.NumberColumn - 1)
        {
            TrySetNextRoad(indexGround, nextIndexesRoad, 1, 1);
            TrySetNextRoad(indexGround, nextIndexesRoad, -1, 0);
            nextIndexesRoad.Add(indexGround + _surface.NumberRow);

            indexGround = nextIndexesRoad[Random.Range(0, nextIndexesRoad.Count)];
            nextRoad = _surface.Grounds[indexGround];
            nextRoad.ChangeOnRoad();
            _way.Add(nextRoad);
            nextIndexesRoad.Clear();
        }
    }

    public List<Vector3> FindPoints()
    {
        List<Vector3> points = new List<Vector3>();

        for (int i = 0; i < _way.Count; i++)
        {
            points.Add(_way[i].transform.position + new Vector3(0, _way[i].HalfHeight, 0));
        }

        return points;
    }

    private void TrySetNextRoad(int indexGround, List<int> nextIndexesRoad, int direction, int extremeRow)
    {
        if ((indexGround + extremeRow) % _surface.NumberRow != 0 && _surface.Grounds[indexGround + 1 * direction].IsFree)
        {
            if (indexGround < _surface.NumberRow || _surface.Grounds[indexGround + 1 * direction - _surface.NumberRow].IsFree)
                nextIndexesRoad.Add(indexGround + 1 * direction);
        }
    }

    private void RemovePath()
    {
        _way.Clear();
    }
}
