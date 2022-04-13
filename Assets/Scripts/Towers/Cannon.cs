using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Tower
{
    [SerializeField] private Core _core;

    private void FixedUpdate()
    {
        if (IsActiveTower)
            FollowTheTarget();
    }

    public override void Shoot()
    {
        TimeAfterShoot += Time.deltaTime;

        if (TimeAfterShoot > TimeCooldown)
        {
            Core core = Instantiate(_core, ShootPoint.position, ShootPoint.rotation, transform);
            core.Launch(CalculatSpeedCore());
            TimeAfterShoot = 0;
        }
    }

    private float CalculatSpeedCore()
    {
        Vector3 direction = Target.transform.position - ShootPoint.position;
        Vector3 directionXZ = new Vector3(direction.x, 0f, direction.z);

        float axisX = directionXZ.magnitude;
        float axisY = direction.y;
        float gravity = Physics.gravity.y;
        float angle = Vector3.Angle(ShootPoint.transform.forward, Turret.transform.forward);
        float angleInRadians = angle * Mathf.PI / 180;

        float speedAmmoSquared = (gravity * axisX * axisX) / (2 * (axisY - Mathf.Tan(angleInRadians) * axisX) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
        return Mathf.Sqrt(Mathf.Abs(speedAmmoSquared));
    }
}
