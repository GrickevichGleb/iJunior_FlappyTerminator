using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _offsetX;
    [SerializeField] private bool _useLateUpdate = false;
    
    private Transform _transform;
    
    private void Start()
    {
        _transform = transform;
    }

    private void Update()
    {
        if (_useLateUpdate)
            return;
        
        AdjustPosition();
    }

    private void LateUpdate()
    {
        if (!_useLateUpdate)
            return;
        
        AdjustPosition();
    }

    private void AdjustPosition()
    {
        Vector3 position = _transform.position;
        position.x = _target.position.x + _offsetX;

        transform.position = position;
    }
}
