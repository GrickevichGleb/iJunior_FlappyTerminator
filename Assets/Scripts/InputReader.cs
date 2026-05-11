using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode _fireKey = KeyCode.X;

    public event Action Jump;
    public event Action Fire;

    private void Update()
    {
        if(Input.GetKeyDown(_jumpKey))
            Jump?.Invoke();
        
        if(Input.GetKeyDown(_fireKey))
            Fire.Invoke();
    }
}
