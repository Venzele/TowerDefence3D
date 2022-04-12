using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Tower
{
    [SerializeField] private LaserBeam _laserBeam;
    [SerializeField] private Transform _model;

    private float _modelScale;

    private void Start()
    {
        _modelScale = _model.localScale.x;
    }

    private void FixedUpdate()
    {
        if (IsSetTower)
        {
            FollowTheTarget();

            if (Target == null)
                _laserBeam.ReturnDefault(ShootPoint);
        }
    }

    public override void Shoot()
    {
        float distance = Vector3.Distance(ShootPoint.transform.position, Target.transform.position);
        _laserBeam.Stretch(distance, _modelScale, ShootPoint);

        TimeAfterShoot += Time.deltaTime;

        if (TimeAfterShoot > TimeCooldown)
        {
            _laserBeam.GiveDamage(Target);
            TimeAfterShoot = 0;
        }
    }
}
