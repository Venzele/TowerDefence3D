using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _speed;
    [SerializeField] private int _reward;

    private Way _way;
    private Vector3[] _points;
    private int _currentPoint;

    public event UnityAction Dying;

    private void Update()
    {
        Vector3 target = _points[_currentPoint];

        transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);

        if (transform.position == target)
        {
            _currentPoint++;

            if(_currentPoint >= _points.Length)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetWay()
    {
        _points = new Vector3[_way.FindPointsWay().Count];

        for (int i = 0; i < _way.FindPointsWay().Count; i++)
        {
            _points[i] = _way.FindPointsWay()[i];
        }
    }

    public void Init(Way way)
    {
        _way = way;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Dying?.Invoke();
            Destroy(gameObject);
        }
    }
}
