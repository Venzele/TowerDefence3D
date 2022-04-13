using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _range, _age;
    [SerializeField] private Explosion _explosion;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private LayerMask _enemyLayerMask;

    private List<Explosion> _explosions = new List<Explosion>();
    private float _elapsedTime;
    private bool _isAlife = true;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _age)
        {
            _elapsedTime = 0;
            Destroy(gameObject);
        }
    }

    public void Launch(float speed)
    {
        _rigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ground ground) && _isAlife)
        {
            Vector3 explosionPoint = transform.position;
            Trigger(explosionPoint);

            _isAlife = false;
            Destroy(gameObject);
        }
    }

    private void Trigger(Vector3 explosionPoint)
    {
        Explosion newExplosion = Instantiate(_explosion, explosionPoint, Quaternion.identity, transform.parent);
        newExplosion.Show(_range);
        _explosions.Add(newExplosion);

        Collider[] targets = Physics.OverlapSphere(explosionPoint, _range, _enemyLayerMask);

        if (targets.Length > 0)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].TryGetComponent(out Enemy target);
                target.TakeDamage(_damage);
            }
        }
    }
}
