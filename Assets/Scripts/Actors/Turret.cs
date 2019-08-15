﻿using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Turret : Ally
{
    private protected override Transform CurrentTarget { get; set; }
    [SerializeField] private protected Transform _firingPoint;

    [SerializeField] private float _damage = 1f;
    private protected virtual float _fireRate
    {
        get { return FireRate; }
        set { FireRate = value; }
    }

    [SerializeField]
    public virtual float _fireCoolDown
    {
        get { return FireCoolDown; }
        set { FireCoolDown = value; }
    }

    public float DefaultFireCooldown = 1f;

    private float FireCoolDown;
    public float FireRate;

    public override void Start()
    {
        base.Start();
        _fireCoolDown = 1f;
    }

    public override void Update()
    {
        base.Update();

        if (CurrentTarget != null)
        {
            if (_fireCoolDown <= 0f)
            {
                Fire();
                _fireCoolDown = DefaultFireCooldown/FireRate;
            }
            else
            {
                _fireCoolDown -= Time.deltaTime;
            }

        }
    }

    public virtual void Fire()
    {
        GameObject go = ObjectPooler.Instance.SpawnFromPool(
            PooledObjectType.SpearBullet, _firingPoint.position,
            Quaternion.identity);

        //DealDamage();

        go.GetComponent<Projectile>().SetProjectile(CurrentTarget , _damage);
    }


}
