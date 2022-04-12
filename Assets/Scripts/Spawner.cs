using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Way _way;
    [SerializeField] private Player _player;
    [SerializeField] private float _delayBetweenWaves;
    [SerializeField] private MainMenuButton _mainMenuButton;
    [SerializeField] private Menu _menu;

    private Coroutine _goSpawn, _holdNextWave;
    private int _numberOfAllEnemies, _numberOfDeadEnemies;

    public int CurrentLevel { get; private set; }

    public event UnityAction AllEnemiesDied;

    private void OnEnable()
    {
        _mainMenuButton.Opened += SpawnRestart;
    }

    private void OnDisable()
    {
        _mainMenuButton.Opened -= SpawnRestart;
    }

    private Wave _currentWave;
    private int _currentWaveNumber = 0, _currentEnemyNumber;
    private float _timeAfterLastWave, _timeAfterLastSpawn;
    private bool _isEndWave = false;

    public event UnityAction WaveFinished;
    public event UnityAction WaveStarted;

    public void StartNextWave()
    {
        _isEndWave = false;
    }

    public void StartSpawn()
    {
        _numberOfAllEnemies = 0;

        for (int i = 0; i < _waves.Count; i++)
        {
            _numberOfAllEnemies += _waves[i].Count;
        }

        _numberOfDeadEnemies = 0;
        SetWave(_currentWaveNumber);
        SetPointSpawn();
        StopSpawn();
        _goSpawn = StartCoroutine(GoSpawn());
    }

    public void SetLevel(int level)
    {
        if (level < 0)
            CurrentLevel = 0;
        else
            CurrentLevel = level;
    }

    public void CountDeadEnemy(Enemy enemy)
    {
        if (enemy.IsDead)
        {
            _numberOfDeadEnemies++;
            
            if (_numberOfDeadEnemies == _numberOfAllEnemies && _player.Health > 0)
            {
                _menu.OpenPanel();
                _menu.SetLabel("Victory!!!");
                RaiseLevel();
                AllEnemiesDied?.Invoke();
            }
        }
    }

    private void RaiseLevel()
    {
        CurrentLevel++;
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.Template, transform.position, transform.rotation, transform).GetComponent<Enemy>();
        enemy.ChangeStats(CurrentLevel);
        enemy.Init(_way, _player, this);
        enemy.SetWay();
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
    }

    private void SetPointSpawn()
    {
        Vector3 pointSpawn = _way.StartRoad.transform.position + new Vector3(0, _way.StartRoad.ScaleSize, 0);
        Vector3 direction = _way.FindPointsWay()[1] - _way.FindPointsWay()[0];

        transform.position = pointSpawn;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }

    private void NextWave()
    {
        WaveStarted?.Invoke();
        StopSpawn();
        _goSpawn = StartCoroutine(GoSpawn());
    }

    private IEnumerator GoSpawn()
    {
        _timeAfterLastSpawn = _currentWave.Delay - 0.1f;
        _currentEnemyNumber = 0;

        while (_currentWave.Count > _currentEnemyNumber)
        {
            _timeAfterLastSpawn += Time.deltaTime;

            if (_timeAfterLastSpawn >= _currentWave.Delay)
            {
                InstantiateEnemy();
                _timeAfterLastSpawn = 0;
                ++_currentEnemyNumber;
            }

            yield return null;
        }

        if (_waves.Count > _currentWaveNumber + 1)
        {
            _isEndWave = true;
            SetWave(++_currentWaveNumber);
            WaveFinished?.Invoke();
            _holdNextWave = StartCoroutine(HoldNextWave());
        }
        else
        {
            _currentWave = null;
        }
    }

    private IEnumerator HoldNextWave()
    {
        while (_isEndWave && _timeAfterLastWave < _delayBetweenWaves)
        {
            _timeAfterLastWave += Time.deltaTime;
            yield return null;
        }

        _timeAfterLastWave = 0;
        NextWave();
    }

    private void SpawnRestart()
    {
        Enemy[] enemies = GetComponentsInChildren<Enemy>();

        foreach (var item in enemies)
        {
            Destroy(item.gameObject);
        }

        _currentWaveNumber = 0;
        StopHoldNextWave();
        StopSpawn();
    }

    private void StopSpawn()
    {
        if (_goSpawn != null)
        {
            StopCoroutine(_goSpawn);
            _goSpawn = null;
        }
    }

    private void StopHoldNextWave()
    {
        if (_holdNextWave != null)
        {
            StopCoroutine(_holdNextWave);
            _holdNextWave = null;
        }
    }
}

[System.Serializable]
public class Wave
{
    public Enemy Template;
    public float Delay;
    public int Count;
}
