using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnable : MonoBehaviour
{
    public event Action<Spawnable> RequestRelease;

    public virtual void Reset() { }
    
    protected virtual void Release()
    {
        RequestRelease?.Invoke(this);
    }
}