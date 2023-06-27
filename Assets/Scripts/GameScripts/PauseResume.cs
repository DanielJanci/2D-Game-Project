using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PauseResume
{
    public static bool IsGamePaused = false;

    public static void PauseGame()
    {
        IsGamePaused = true;
    }

    public static void ResumeGame()
    {
        IsGamePaused = false;
    }
}
