using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject fallingObstacle;
    public GameObject fallingCoin;
    
    private float _screenHeight;
    private float _screenHalfWidth;
    
    private void Start()
    {
        _screenHeight = Camera.main.orthographicSize;
        _screenHalfWidth = Camera.main.aspect * _screenHeight;
        
        Player.OnPlayerDeath += GameOver;
        SpawnManager.OnSpawnObstacle += SpawnObstacle;
        SpawnManager.OnSpawnCoin += SpawnCoin;
    }


    private void GameOver()
    {
        Player.OnPlayerDeath -= GameOver;
        SpawnManager.OnSpawnObstacle -= SpawnObstacle;
        SpawnManager.OnSpawnCoin -= SpawnCoin;
    }

    private void SpawnCoin()
    {
       SpawnGameObject(fallingCoin);
    }


    private void SpawnObstacle()
    {
        SpawnGameObject(fallingObstacle);
    }
    
    private void SpawnGameObject(GameObject currentGameObject)
    {
        Vector3 localScale = currentGameObject.transform.localScale;
        float spawnY = _screenHeight + localScale.y / 2f;
        float spawnX = Random.Range(-_screenHalfWidth + localScale.x / 2f, _screenHalfWidth - localScale.x / 2f);
        Vector2 spawnPosition = new Vector2(spawnX, spawnY);
        LeanPool.Spawn(currentGameObject, spawnPosition, Quaternion.identity);
    }
    
    
}
