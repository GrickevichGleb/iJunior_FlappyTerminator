using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : Spawner
{
    [SerializeField] private float _interval = 1f;
    [SerializeField] private Transform _firingPoint;

    private bool _isSpawning = true;

    private float _lastShotTime;

    protected override void ActionOnGet(Spawnable spawnable)
    {
        base.ActionOnGet(spawnable);

        spawnable.transform.position = _firingPoint.position;
    }

    public void TryFire()
    {
        if(Time.time - _lastShotTime > _interval)
            Fire();
    }

    private void Fire()
    {
        _pool.Get();

        _lastShotTime = Time.time;
    }
    
    private IEnumerator SpawnProjectilesCoroutine()
    {
        var delay = new WaitForSeconds(_interval);
        
        while (_isSpawning)
        {
            yield return delay;

            _pool.Get();
        }
    }
}
