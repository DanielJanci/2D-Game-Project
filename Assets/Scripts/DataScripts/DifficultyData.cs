using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DifficultyData
{
    public float secondsToMaxDifficulty;
    public float minObstacleSpeed;
    public float maxObstacleSpeed;
    public float startIntervalBetweenSpawns;
    public float endIntervalBetweenSpawns;
    public float playerSpeed;
}
