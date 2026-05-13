using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollisionHandler : MonoBehaviour
{
    public event Action<IInteractable> Collision;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Detected collision");
        
        if (other.gameObject.TryGetComponent(out IInteractable interactable))
        {
            Collision?.Invoke(interactable);
        }
    }
}
