using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public static UnityAction OnPlayerDeath;
    public static UnityAction OnCoinCollected;
    public static UnityAction OnGameStarted;
    public Material playerMaterial;

    private float _speed;
    private float _screenHalfWidth;
    private bool _movementEnabled;
    private GameDataManager _gameDataManager;
    
    private void Start()
    {
        
        _gameDataManager = FindObjectOfType<GameDataManager>();
        playerMaterial.color = _gameDataManager.UserData.currentPlayerColor;
        _speed = _gameDataManager.DifficultyData.playerSpeed;
        
        float playerHalfWidth = transform.localScale.x / 2f;
        _screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize - playerHalfWidth;
        
        OnGameStarted?.Invoke();
        PauseResume.ResumeGame();
        _movementEnabled = true;
    }
    
    private void Update()
    {
        if (_movementEnabled && !PauseResume.IsGamePaused)
        {
            MobileMovement();
        }
    }

    private void MobileMovement()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 nextPosition = Vector3.zero;
            var t = transform;

            if (mousePosition.y < 4)
            {
                if(mousePosition.x > 0)
                {
                    nextPosition = t.position + Vector3.right * (_speed * Time.deltaTime);
                }

                else if (mousePosition.x < 0)
                {
                    nextPosition = t.position + Vector3.right * (-_speed * Time.deltaTime);
                }
            
                if (nextPosition.x <= -_screenHalfWidth)
                {
                    nextPosition = new Vector3(-_screenHalfWidth, t.position.y);
                }
                if (nextPosition.x >= _screenHalfWidth)
                {
                    nextPosition = new Vector3(_screenHalfWidth, t.position.y);
                }
                t.position = Vector3.Lerp(t.position, nextPosition, 50 * Time.deltaTime);   
            }
        }
    }
    
    // private void PCMovement()
    // {
    //     float inputX = Input.GetAxisRaw("Horizontal");
    //     float velocity = inputX * _speed;
    //     var t = transform;
    //     t.Translate(Vector2.right * (velocity * Time.deltaTime));
    //     if (t.position.x <= -_screenHalfWidth)
    //     {
    //         t.position = new Vector3(-_screenHalfWidth, t.position.y);
    //     }
    //     if (t.position.x >= _screenHalfWidth)
    //     {
    //         t.position = new Vector3(_screenHalfWidth, t.position.y);
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        if (triggerCollider.CompareTag("FallingObstacle"))
        {
            _movementEnabled = false;
            OnPlayerDeath?.Invoke();
        }

        if (triggerCollider.CompareTag("FallingCoin"))
        {
            OnCoinCollected?.Invoke();
            LeanPool.Despawn(triggerCollider.gameObject);
        }
    }

}
