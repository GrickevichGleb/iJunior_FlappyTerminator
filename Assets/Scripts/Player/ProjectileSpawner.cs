using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : Spawner
{
    [SerializeField] private float _interval = 1f;
    [SerializeField] private Transform _firingPoint;
    [SerializeField] private bool _autoFire = false;
    
    private bool _isSpawning = true;

    private float _lastShotTime;

    private Coroutine _autoFireCoroutine;
    
    private void OnEnable()
    {
        if (_autoFire)
            _autoFireCoroutine = StartCoroutine(SpawnProjectilesCoroutine());
    }

    private void OnDisable()
    {
        if(_autoFireCoroutine != null)
            StopCoroutine(_autoFireCoroutine);
    }

    protected override void ActionOnGet(Spawnable spawnable)
    {
        base.ActionOnGet(spawnable);

        Projectile projectile = spawnable as Projectile;
        projectile!.transform.position = _firingPoint.position;
        projectile.SetDirection(_firingPoint.right);
    }

    public void DestroyAllProjectiles()
    {
        DestroyAllObjects();
    }
    
    public void TryFire()
    {
        if(Time.time - _lastShotTime > _interval)
            Fire();
    }

    private void Fire()
    {
        pool.Get();

        _lastShotTime = Time.time;
    }
    
    private IEnumerator SpawnProjectilesCoroutine()
    {
        var delay = new WaitForSeconds(_interval);
        
        while (_isSpawning)
        {
            yield return delay;

            pool.Get();
        }
    }
}
