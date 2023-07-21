using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    public TimeManager timeManager;
    
    public static UnityAction OnSpawnObstacle;
    public static UnityAction OnSpawnCoin;

    private float _currentSecondsBetweenSpawns;
    private float _nextSpawnTime;
    private bool _canSpawn;
    private float _secondsBetweenSpawnsStart;
    private float _secondsBetweenSpawnsEnd;
    private float _secondsToMaxDifficulty;
    private GameDataManager _gameDataManager;

    private void Start()
    {
        _gameDataManager = FindObjectOfType<GameDataManager>();
        _secondsBetweenSpawnsStart = _gameDataManager.DifficultyData.startIntervalBetweenSpawns;
        _secondsBetweenSpawnsEnd = _gameDataManager.DifficultyData.endIntervalBetweenSpawns;
        _secondsToMaxDifficulty = _gameDataManager.DifficultyData.secondsToMaxDifficulty;
        _canSpawn = true;
        _currentSecondsBetweenSpawns = Mathf.Lerp(_secondsBetweenSpawnsStart, _secondsBetweenSpawnsEnd, 
            Mathf.Clamp01(timeManager.currentLevelTime / _secondsToMaxDifficulty));
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
                _currentSecondsBetweenSpawns = Mathf.Lerp(_secondsBetweenSpawnsStart, _secondsBetweenSpawnsEnd, 
                    Mathf.Clamp01(timeManager.currentLevelTime / _secondsToMaxDifficulty));
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
            yield return new WaitUntil(() => _canSpawn);
            OnSpawnObstacle?.Invoke();
            _canSpawn = false;
            yield return new WaitUntil(() => _canSpawn);
            OnSpawnObstacle?.Invoke();
            _canSpawn = false;
            yield return new WaitUntil(() => _canSpawn);
            OnSpawnCoin?.Invoke();
            _canSpawn = false;
        }
    }
}
