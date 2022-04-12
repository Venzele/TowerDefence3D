using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] private int _damage;

    private Vector3 _laserBeamScale;
    private Vector3 _defaultLaserBeamScale;

    private void Awake()
    {
        _laserBeamScale = transform.localScale;
        _defaultLaserBeamScale = transform.localScale;
    }

    public void GiveDamage(Enemy target)
    {
        target.TakeDamage(_damage);
    }

    public void Stretch(float distance, float modelScale, Transform shootPoint)
    {
        _laserBeamScale.z = distance / modelScale;
        transform.localScale = _laserBeamScale;
        transform.position = shootPoint.position + 0.5f * distance * transform.forward;
    }

    public void ReturnDefault(Transform shootPoint)
    {
        transform.localScale = _defaultLaserBeamScale;
        transform.position = shootPoint.position;
    }
}
