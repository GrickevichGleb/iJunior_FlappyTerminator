using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private EnemiesSpawner _enemiesSpawner;
    [SerializeField] private StartGameScreen _startScreen;
    [SerializeField] private EndGameScreen _endScreen;

    private void OnEnable()
    {
        _startScreen.StartButtonClicked += OnStartButtonClicked;
        _endScreen.RestartButtonClicked += OnRestartButtonClicked;
        
        _player.HitObstacle += OnGameOver;
    }

    private void OnDisable()
    {
        _startScreen.StartButtonClicked -= OnStartButtonClicked;
        _endScreen.RestartButtonClicked -= OnRestartButtonClicked;
        
        _player.HitObstacle -= OnGameOver;
    }
    
    private void Start()
    {
        Time.timeScale = 0;
        _startScreen.Open();
    }

    private void OnStartButtonClicked()
    {
        _startScreen.Close();
        StartGame();
    }

    private void OnRestartButtonClicked()
    {
        _endScreen.Close();
        StartGame();
    }

    private void StartGame()
    {
        Time.timeScale = 1f;
        _player.ResetPlayer();
        _enemiesSpawner.ResetSpawner();
    }

    private void OnGameOver()
    {
        Time.timeScale = 0f;
        _endScreen.Open();
    }
}
