using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DifficultyManager: MonoBehaviour
{
    public float secondsToMaxDifficulty = 60;
    public TimeManager timeManager;
    
    public float GetDifficultyPercent()
    {
        return Mathf.Clamp01(timeManager.currentLevelTime / secondsToMaxDifficulty);
    }
}
