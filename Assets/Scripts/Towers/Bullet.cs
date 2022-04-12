using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed, _age;

    private float _elapsedTime;
    private Vector3 _previousStep;

    private void Start()
    {
        _previousStep = transform.position;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        CheckHit();

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _age)
        {
            _elapsedTime = 0;
            Destroy(gameObject);
        }
    }

    private void CheckHit()
    {
        float distance = Vector3.Distance(_previousStep, transform.position);

        if (Physics.Raycast(_previousStep, transform.TransformDirection(Vector3.forward), out RaycastHit hit, distance))
        {
            hit.collider.gameObject.TryGetComponent(out Enemy target);
            
            if (target != null)
            {
                target.TakeDamage(_damage);
                Destroy(gameObject);
            }
        }

        _previousStep = transform.position;
    }
}
