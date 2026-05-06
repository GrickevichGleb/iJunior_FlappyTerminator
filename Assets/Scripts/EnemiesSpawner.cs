using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : Spawner
{
    [SerializeField] private float _interval = 1f;
    [SerializeField] private Transform _spawnAreaMarker;

    private bool _isSpawning = true;

    private void Start()
    {
        StartCoroutine(SpawnEnemiesCoroutine());
    }

    protected override void ActionOnGet(Spawnable spawnable)
    {
        base.ActionOnGet(spawnable);

        spawnable.transform.position = GetSpawnPosition();
    }

    private IEnumerator SpawnEnemiesCoroutine()
    {
        var delay = new WaitForSeconds(_interval);
        
        while (_isSpawning)
        {
            yield return delay;

            _pool.Get();
        }
    }

    private Vector3 GetSpawnPosition()
    {
        int offset = Convert.ToInt32(_spawnAreaMarker.localScale.y / 2);
        int posYOffset = UtilsRandom.GetRandomNumber(-offset, offset);

        Vector3 position = transform.position;
        position.y += posYOffset;
        
        return position;
    }
}
