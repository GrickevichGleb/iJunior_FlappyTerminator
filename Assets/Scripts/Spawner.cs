using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] protected Spawnable spawnablePref;
    [Space] 
    [SerializeField] protected int poolCapacity = 10;
    [SerializeField] protected int poolMaxSize = 100;

    protected ObjectPool<Spawnable> pool;

    public event Action<Spawnable> Spawned;
    public event Action<Spawner> DestroyAllPoolObjects;

    private void Awake()
    {
        pool = new ObjectPool<Spawnable>(
            createFunc: () => Instantiate(spawnablePref),
            actionOnGet: (spawnable) => ActionOnGet(spawnable),
            actionOnRelease: (spawnable) => spawnable.gameObject.SetActive(false),
            actionOnDestroy: (spawnable) => ActionOnDestroy(spawnable),
            collectionCheck: true,
            defaultCapacity: poolCapacity,
            maxSize: poolMaxSize);
    }
    
    protected virtual void ActionOnGet(Spawnable spawnable)
    {
        spawnable.Reset();
        spawnable.SubscribeRemoteDestroy(this);
        spawnable.RequestRelease += OnRequestRelease;
        
        Spawned?.Invoke(spawnable);
    }

    protected virtual void ActionOnDestroy(Spawnable spawnable)
    {
        Destroy(spawnable.gameObject);
    }
    
    protected virtual void OnRequestRelease(Spawnable spawnable)
    {
        spawnable.RequestRelease -= OnRequestRelease;
        pool.Release(spawnable);
    }

    protected virtual void DestroyAllObjects()
    {
        DestroyAllPoolObjects?.Invoke(this);
    }
}
