using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
public class Enemy : Spawnable
{
    [SerializeField] private int _scoreValue = 5;

    private CollisionHandler _collisionHandler;
    private ProjectileSpawner _projectileSpawner;

    public event Action<int> Destroyed;

    private void Awake()
    {
        _collisionHandler = GetComponent<CollisionHandler>();
        _projectileSpawner = GetComponent<ProjectileSpawner>();
    }

    private void OnEnable()
    {
        _collisionHandler.Collision += OnCollision;
    }

    private void OnDisable()
    {
        _collisionHandler.Collision -= OnCollision;
    }

    private void OnCollision(IInteractable interactable)
    {
        if (interactable is Despawner)
        {
            Release();
            return;
        }

        if (interactable is Projectile)
        {
            Debug.Log("Shot down");
            Destroyed?.Invoke(_scoreValue);

            Release();
        }
    }

    protected override void Release()
    {
        _projectileSpawner.DestroyAllProjectiles();
        base.Release();
    }

    public override void Reset()
    {
        gameObject.SetActive(true);
    }
}
