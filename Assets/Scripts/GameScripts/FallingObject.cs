using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;

public class FallingObject : MonoBehaviour, IPoolable
{
    public float minSpeed = 5f;
    public float maxSpeed = 10f;

    private float _despawnHeight;
    private float _currentSpeed;
    private bool _isGameOver;
    private DifficultyManager _difficultyManager;

    private void Start()
     {
         Player.OnPlayerDeath += GameOver;
         _isGameOver = false;
         _difficultyManager = FindObjectOfType<DifficultyManager>(); 
         _despawnHeight = -Camera.main.orthographicSize - transform.localScale.x;
     }


    private void Update()
    {
        if (!PauseResume.IsGamePaused)
        {
            _currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, _difficultyManager.GetDifficultyPercent());
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
