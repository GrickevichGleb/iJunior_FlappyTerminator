using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private int _score = 0;

    public event Action<int> ScoreChanged;
    
    public void Increase(int value)
    {
        _score += value;
        ScoreChanged?.Invoke(_score);
    }

    public void ResetScore()
    {
        _score = 0;
        ScoreChanged?.Invoke(_score);
    }
}
