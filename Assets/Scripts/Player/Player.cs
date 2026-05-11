using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
public class Player : MonoBehaviour
{
    private InputReader _inputReader;
    private CollisionHandler _collisionHandler;
    
    private PlayerMover _playerMover;
    private ProjectileSpawner _projectileSpawner;

    public event Action HitObstacle;
    
    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _collisionHandler = GetComponent<CollisionHandler>();

        _playerMover = GetComponent<PlayerMover>();
        _projectileSpawner = GetComponent<ProjectileSpawner>();
    }

    private void OnEnable()
    {
        _collisionHandler.Collision += OnCollision;
        
        _inputReader.Jump += OnJump;
        _inputReader.Fire += OnFire;
    }

    private void OnDisable()
    {
        _collisionHandler.Collision -= OnCollision;
        
        _inputReader.Jump -= OnJump;
        _inputReader.Fire -= OnFire;
    }

    public void ResetPlayer()
    {
        transform.rotation = Quaternion.identity;
        transform.position = Vector3.zero;
    }

    private void OnJump()
    {
        _playerMover.TryJump();
    }

    private void OnFire()
    {
        _projectileSpawner.TryFire();
    }
    
    private void OnCollision(IInteractable interactable)
    {
        Debug.Log("Hit collidable object");
        HitObstacle?.Invoke();
    }
}
