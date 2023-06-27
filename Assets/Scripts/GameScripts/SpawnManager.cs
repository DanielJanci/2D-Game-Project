using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    public float secondsBetweenSpawnsStart = 1.5f;
    public float secondsBetweenSpawnsEnd = 0.7f;
    
    public TimeManager timeManager;
    public DifficultyManager difficultyManager;
    
    public static UnityAction OnSpawnObstacle;
    public static UnityAction OnSpawnCoin;

    private float _currentSecondsBetweenSpawns;
    private float _nextSpawnTime;
    private bool _canSpawn;


    private void Start()
    {
        _canSpawn = true;
        _currentSecondsBetweenSpawns = Mathf.Lerp(secondsBetweenSpawnsStart, secondsBetweenSpawnsEnd, 
            difficultyManager.GetDifficultyPercent());
        _nextSpawnTime = _currentSecondsBetweenSpawns;
        StartCoroutine(SpawnOrder());
    }

    private void Update()
    {
        if (!PauseResume.IsGamePaused)
        {
            if (timeManager.currentLevelTime >= _nextSpawnTime)
            {
                _canSpawn = true;
                _currentSecondsBetweenSpawns = Mathf.Lerp(secondsBetweenSpawnsStart, secondsBetweenSpawnsEnd, 
                    difficultyManager.GetDifficultyPercent());
                _nextSpawnTime += _currentSecondsBetweenSpawns;
            }
        }
        else
        {
            _canSpawn = false;
        }
    }


    private IEnumerator SpawnOrder()
    {
        while (true)
        {
            _canSpawn = false;
            yield return new WaitUntil(() => _canSpawn);
            OnSpawnObstacle?.Invoke();
            _canSpawn = false;
            yield return new WaitUntil(() => _canSpawn);
            OnSpawnObstacle?.Invoke();
            _canSpawn = false;
            yield return new WaitUntil(() => _canSpawn);
            OnSpawnCoin?.Invoke();
        }
    }
}
