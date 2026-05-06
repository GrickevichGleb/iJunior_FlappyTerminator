using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
public class Enemy : Spawnable
{
    private CollisionHandler _collisionHandler;

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
        }
    }

    public override void Reset()
    {
        gameObject.SetActive(true);
    }
}
