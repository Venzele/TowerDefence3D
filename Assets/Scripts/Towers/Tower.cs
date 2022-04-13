using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected float TimeCooldown, Range;
    [SerializeField] protected Transform ShootPoint;
    [SerializeField] protected Transform Turret;
    [SerializeField] protected LayerMask _enemyLayerMask;

    protected List<Color> DefaultColor = new List<Color>();
    protected Renderer[] Renderers;
    protected Enemy Target;
    protected float TimeAfterShoot;
    protected bool IsActiveTower = false;

    private void Awake()
    {
        Renderers = GetComponentsInChildren<Renderer>();

        for (int i = 0; i < Renderers.Length; i++)
        {
            DefaultColor.Add(Renderers[i].material.color);
        }

        TimeAfterShoot = TimeCooldown;
    }

    protected void FollowTheTarget()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, Range, _enemyLayerMask);

        if (targets.Length > 0)
            Target = targets[0].GetComponent<Enemy>();
        else
            Target = null;

        if (Target != null)
        {
            float distance = Vector3.Distance(transform.position, Target.transform.position);

            if (distance <= Range + Target.SizeCollider)
            {
                Vector3 direction = Target.transform.position - Turret.position;
                Vector3 directionXZ = new Vector3(direction.x, 0f, direction.z);

                Turret.rotation = Quaternion.LookRotation(directionXZ, Vector3.up);
                Shoot();
            }
            else
            {
                Target = null;
            }
        }
    }

    public abstract void Shoot();

    public void ChangeColor(Color color)
    {
        if (IsActiveTower)
        {
            for (int i = 0; i < Renderers.Length; i++)
            {
                Renderers[i].material.color = DefaultColor[i];
            }
        }
        else
        {
            foreach (var renderer in Renderers)
            {
                renderer.material.color = color;
            }
        }
    }

    public void Enable()
    {
        IsActiveTower = true;
    }
}
