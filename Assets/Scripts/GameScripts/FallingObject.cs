using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;

public class FallingObject : MonoBehaviour, IPoolable
{
    private float _minSpeed;
    private float _maxSpeed;
    private float _secondsToMaxDifficulty;
    private float _despawnHeight;
    private float _currentSpeed;
    private bool _isGameOver;
    private TimeManager _timeManager;
    private GameDataManager _gameDataManager;

    private void Start()
     {
         Player.OnPlayerDeath += GameOver;
         _isGameOver = false;
         _timeManager = FindObjectOfType<TimeManager>();
         _gameDataManager = FindObjectOfType<GameDataManager>();
         _minSpeed = _gameDataManager.DifficultyData.minObstacleSpeed;
         _maxSpeed = _gameDataManager.DifficultyData.maxObstacleSpeed;
         _secondsToMaxDifficulty = _gameDataManager.DifficultyData.secondsToMaxDifficulty;
         _despawnHeight = -Camera.main.orthographicSize - transform.localScale.x;
     }


    private void Update()
    {
        if (!PauseResume.IsGamePaused)
        {
            _currentSpeed = Mathf.Lerp(_minSpeed, _maxSpeed, Mathf.Clamp01(_timeManager.currentLevelTime / _secondsToMaxDifficulty));
            transform.Translate(Vector3.down * (_currentSpeed * Time.deltaTime),Space.World);
        }
        
        if (transform.position.y <= _despawnHeight)
        {
            LeanPool.Despawn(gameObject);
        }

        if (_isGameOver)
        {
            LeanPool.Despawn(gameObject);
        }
    }

    private void GameOver()
    {
        Player.OnPlayerDeath -= GameOver;
        _isGameOver = true;
    }

    public void OnSpawn()
    {
        
    }

    public void OnDespawn()
    {
        DOTween.Kill(gameObject);
    }
}
