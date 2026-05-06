using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionHandler : MonoBehaviour
{
    public event Action<IInteractable> Collision; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            Collision?.Invoke(interactable);
        }
    }
}
