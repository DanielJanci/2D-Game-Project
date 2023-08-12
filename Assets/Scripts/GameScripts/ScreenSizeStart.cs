using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSizeStart : MonoBehaviour
{
    public CanvasScaler canvasScaler;
    void Start()
    {
        // int windowHeight = Screen.currentResolution.height;
        // double windowWidth = (double)windowHeight * windowHeight / Screen.currentResolution.width;
        // Screen.SetResolution((int)Math.Round(windowWidth), windowHeight, false);
        Screen.fullScreen = true;
        CalculateScreenSizes();
    }

    private void CalculateScreenSizes()
    {
        double wantedRatio = (double)1920 / 1080;
        double currentRatio = (double)Screen.currentResolution.height / Screen.currentResolution.width;
        Debug.Log(canvasScaler.matchWidthOrHeight);
        if (currentRatio < wantedRatio)
        {
            canvasScaler.matchWidthOrHeight = 1;
        }
        else if (currentRatio > wantedRatio){
            canvasScaler.matchWidthOrHeight = 0;
        }
        else
        {
            return;
        }
        Debug.Log(wantedRatio);
        Debug.Log(currentRatio);
        Debug.Log(canvasScaler.matchWidthOrHeight);

    }
    
}
