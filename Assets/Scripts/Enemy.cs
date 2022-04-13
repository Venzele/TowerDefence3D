using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speedMovement, _health;
    [SerializeField] private int _reward, _damage;
    [SerializeField] private float _stepUpdateSpeedMovement, _stepUpdateHealth, _stepUpdateReward, _stepUpdateDamage;
    [SerializeField] private SphereCollider _sphereCollider;

    private Way _way;
    private Base _base;
    private Bank _bank;
    private Spawner _spawner;
    private Vector3[] _points;
    private float _speedRotation = 400;
    private int _currentPoint;

    public bool IsDead { get; private set; }
    public float SizeCollider { get; private set; }

    private void OnEnable()
    {
        IsDead = false;
        SizeCollider = _sphereCollider.radius;
    }

    private void OnValidate()
    {
        if (_speedMovement < 0)
            _speedMovement = 0;

        if (_health <= 0)
            _health = 1;

        if (_reward < 0)
            _reward = 0;

        if (_damage < 0)
            _damage = 0;

        if (_stepUpdateSpeedMovement < 0)
            _stepUpdateSpeedMovement = 0;

        if (_stepUpdateHealth < 0)
            _stepUpdateHealth = 0;

        if (_stepUpdateReward < 0)
            _stepUpdateReward = 0;

        if (_stepUpdateDamage < 0)
            _stepUpdateDamage = 0;
    }

    private void Update()
    {
        Vector3 target = _points[_currentPoint];
        Vector3 direction = target - transform.position;

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);

            if (_currentPoint > 0)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _speedRotation * Time.deltaTime);
        }

        transform.position = Vector3.MoveTowards(transform.position, target, _speedMovement * Time.deltaTime);

        if (transform.position == target)
        {
            _currentPoint++;

            if(_currentPoint >= _points.Length)
            {
                _base.TakeDamage(_damage);
                IsDead = true;
                Destroy(gameObject);
                _spawner.CountDeadEnemy(this);
            }
        }
    }

    public void SetWay()
    {
        _points = new Vector3[_way.FindPoints().Count];

        for (int i = 0; i < _way.FindPoints().Count; i++)
        {
            _points[i] = _way.FindPoints()[i];
        }
    }

    public void Init(Way way, Base mainBase, Bank bank, Spawner spawner)
    {
        _way = way;
        _base = mainBase;
        _bank = bank;
        _spawner = spawner;
    }

    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            _health -= damage;

            if (_health <= 0 && IsDead == false)
            {
                _bank.AddCoins(_reward);
                IsDead = true;
                Destroy(gameObject);
                _spawner.CountDeadEnemy(this);
            }
        }
    }

    public void ChangeStats(int level)
    {
        _speedMovement += level * _stepUpdateSpeedMovement;
        _health += Mathf.Floor(level * _stepUpdateHealth);
        _reward += Mathf.FloorToInt(level * _stepUpdateReward);
        _damage += Mathf.FloorToInt(level * _stepUpdateDamage);
    }
}
