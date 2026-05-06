using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _tapForce;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxRotationZ;
    [SerializeField] private float _minRotationZ;

    private Vector3 _startPosition;
    private Rigidbody2D _rigidbody;

    private Quaternion _maxRotation;
    private Quaternion _minRotation;

    private Player _player;
    
    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.HitObstacle += OnHitObstacle;
    }

    private void Start()
    {
        _startPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody2D>();
        
        _maxRotation = Quaternion.Euler(0f, 0f, _maxRotationZ);
        _minRotation = Quaternion.Euler(0f, 0f, _minRotationZ);
    }
    
    private void OnDisable()
    {
        _player.HitObstacle -= OnHitObstacle;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.velocity = new Vector2(_speed, _tapForce);
            transform.rotation = _maxRotation;
        }
        
        transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotationSpeed * Time.deltaTime);
    }
    
    private void OnHitObstacle()
    {
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.isKinematic = true;
    }
}
