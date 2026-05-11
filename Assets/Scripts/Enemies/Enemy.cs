using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
public class Enemy : Spawnable
{
    [SerializeField] private int _scoreValue = 5;
    
    private CollisionHandler _collisionHandler;

    public event Action<int> Destroyed;

    private void Awake()
    {
        _collisionHandler = GetComponent<CollisionHandler>();
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

    public override void Reset()
    {
        gameObject.SetActive(true);
    }
}
