using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(Collider2D))]
public class Projectile : Spawnable, IInteractable
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _releaseDelay = 4f;
    [SerializeField] private bool _invertDirection = false;
    
    private Transform _transform;
    private CollisionHandler _collisionHandler;
    private Vector3 _fireDirection;
    
    private void Awake()
    {
        _transform = transform;
        _collisionHandler = GetComponent<CollisionHandler>();
        _fireDirection = Vector3.right;

        if (_invertDirection)
            _fireDirection = Vector3.left;
    }

    private void OnEnable()
    {
        _collisionHandler.Collision += OnCollision;
    }

    private void OnDisable()
    {
        _collisionHandler.Collision -= OnCollision;
    }

    private void Update()
    {
        _transform.Translate(_fireDirection * (_speed * Time.deltaTime));
    }
    
    public override void Reset()
    {
        base.Reset();
        gameObject.SetActive(true);

        StartCoroutine(ReleaseAfterSecondsCoroutine(_releaseDelay));
    }

    protected override void Release()
    {
        StopAllCoroutines();
        base.Release();
    }

    private void OnCollision(IInteractable interactable)
    {
        Release();
    }

    private IEnumerator ReleaseAfterSecondsCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        Release();
    }
}
