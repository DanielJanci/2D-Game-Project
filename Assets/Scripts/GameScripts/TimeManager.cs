using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TimeManager : MonoBehaviour
{
    public float currentLevelTime;
    public float deltaTime;
    private void Start()
    {
        currentLevelTime = Time.timeSinceLevelLoad;
        deltaTime = Time.deltaTime;
    }
    
    private void Update()
    {
        currentLevelTime += deltaTime;

        if (!PauseResume.IsGamePaused)
        {
            deltaTime = Time.deltaTime;
        }
        else
        {
            deltaTime = 0;
        }
    }
}
