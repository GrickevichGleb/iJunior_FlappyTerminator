using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] private EnemiesSpawner _enemiesSpawner;
    [SerializeField] private ScoreCounter _playerScore;

    private void OnEnable()
    {
        _enemiesSpawner.Spawned += OnSpawned;
    }

    private void OnDisable()
    {
        _enemiesSpawner.Spawned -= OnSpawned;
    }

    public void ResetScore()
    {
        _playerScore.ResetScore();
    }

    private void OnSpawned(Spawnable spawnable)
    {
        spawnable.RequestRelease += OnSpawnableRequestRelease;
        
        if (spawnable.TryGetComponent(out Enemy enemy))
        {
            enemy.Destroyed += OnEnemyDestroyed;
        }
    }

    private void OnSpawnableRequestRelease(Spawnable spawnable)
    {
        spawnable.RequestRelease -= OnSpawnableRequestRelease;
        
        if (spawnable.TryGetComponent(out Enemy enemy))
        {
            enemy.Destroyed -= OnEnemyDestroyed;
        }
    }
    
    private void OnEnemyDestroyed(int scoreValue)
    {
        _playerScore.Increase(scoreValue);
    }
}
