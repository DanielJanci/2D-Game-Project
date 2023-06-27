using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public float speed = 7f;
    public static UnityAction OnPlayerDeath;
    public static UnityAction OnCoinCollected;

    private float _screenHalfWidth;
    private bool _movementEnabled;
    private void Start()
    {
        float playerHalfWidth = transform.localScale.x / 2f;
        _screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize - playerHalfWidth;
        _movementEnabled = true;
    }

    private void Update()
    {
        if (_movementEnabled && !PauseResume.IsGamePaused)
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float velocity = inputX * speed;
            var t = transform;
            t.Translate(Vector2.right * (velocity * Time.deltaTime));
            if (t.position.x <= -_screenHalfWidth)
            {
                t.position = new Vector3(-_screenHalfWidth, t.position.y);
            }
            if (t.position.x >= _screenHalfWidth)
            {
                t.position = new Vector3(_screenHalfWidth, t.position.y);
            }
        }
    }

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
