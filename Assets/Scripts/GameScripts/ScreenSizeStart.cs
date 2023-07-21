using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSizeStart : MonoBehaviour
{
    void Start()
    {
        // int windowHeight = Screen.currentResolution.height;
        // double windowWidth = (double)windowHeight * windowHeight / Screen.currentResolution.width;
        // Screen.SetResolution((int)Math.Round(windowWidth), windowHeight, false);
        Screen.fullScreen = true;
    }
    
}
