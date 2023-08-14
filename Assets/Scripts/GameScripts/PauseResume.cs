using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class PauseResume
{
    public static bool IsGamePaused = false;
    public static UnityAction OnGamePaused;
    public static UnityAction OnGameResumed;

    public static void PauseGame()
    {
        IsGamePaused = true;
        OnGamePaused?.Invoke();
    }

    public static void ResumeGame()
    {
        IsGamePaused = false;
        OnGameResumed?.Invoke();
    }
}
