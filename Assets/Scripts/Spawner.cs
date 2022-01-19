using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Way _way;
    [SerializeField] private float _delayUnderWaves;

    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private float _timeAfterLastSpawn;
    private float _timeAfterLastWave;
    private int _spawned;
    private bool _isEndWave = false;

    private void Update()
    {
        if (_currentWave == null)
            return;

        if (_isEndWave)
            _timeAfterLastWave += Time.deltaTime;
        else
            _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _currentWave.Delay)
        {
            InstantiateEnemy();
            _spawned++;
            _timeAfterLastSpawn = 0;
        }

        if (_currentWave.Count <= _spawned)
        {
            _isEndWave = true;
            _currentWaveNumber++;
            SetWave(_currentWaveNumber);
            _spawned = 0;
            //_currentWave = null;
        }

        if (_timeAfterLastWave >= _delayUnderWaves)
        {
            _isEndWave = false;
            _timeAfterLastWave = 0;
        }
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.Template, transform.position, transform.rotation, transform).GetComponent<Enemy>();
        enemy.Init(_way);
        enemy.SetWay();
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
    }

    private void SetPointSpawn()
    {
        Vector3 pointSpawn = _way.StartRoad.transform.position + new Vector3(0, -2, 0);
        transform.position = pointSpawn;
    }

    public void StartSpawn()
    {
        SetWave(_currentWaveNumber);
        SetPointSpawn();
    }
}

[System.Serializable]
public class Wave
{
    public Enemy Template;
    public float Delay;
    public int Count;
}
