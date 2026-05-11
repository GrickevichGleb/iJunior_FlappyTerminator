using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Spawnable, IInteractable
{
    [SerializeField] private float _speed = 5f;

    private Transform _transform;
    
    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        _transform.Translate(Vector3.right * (_speed * Time.deltaTime));
    }

    public override void Reset()
    {
        gameObject.SetActive(true);
    }
}
