using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreValue;
    [SerializeField] private ScoreCounter _playerScore;

    private void OnEnable()
    {
        _playerScore.ScoreChanged += UpdateScore;
    }

    private void OnDisable()
    {
        _playerScore.ScoreChanged -= UpdateScore;
    }

    private void UpdateScore(int score)
    {
        _scoreValue.text = score.ToString();
    }
}
