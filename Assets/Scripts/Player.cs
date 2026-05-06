using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private CollisionHandler _collisionHandler;

    public event Action HitObstacle;
    
    private void Awake()
    {
        
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
        Debug.Log("Hit collidable object");
        HitObstacle?.Invoke();
    }
}
