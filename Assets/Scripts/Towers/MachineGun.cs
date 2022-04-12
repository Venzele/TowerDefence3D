using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Tower
{
    [SerializeField] private Bullet _bullet;

    private void FixedUpdate()
    {
        if (IsSetTower)
            FollowTheTarget();
    }

    public override void Shoot()
    {
        TimeAfterShoot += Time.deltaTime;

        if (TimeAfterShoot > TimeCooldown)
        {
            Instantiate(_bullet, ShootPoint.position, ShootPoint.rotation, transform);
            TimeAfterShoot = 0;
        }
    }
}
