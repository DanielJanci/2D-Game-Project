using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseResumeUI : MonoBehaviour
{
    public TMP_Text pauseResumeText;
    
    private void Start()
    {
        pauseResumeText.text = "Pause";
    }

    public void OnPauseResumeButton()
    {
        AudioManager.OnButtonClick?.Invoke();
        if (PauseResume.IsGamePaused)
        {
            PauseResume.ResumeGame();
            pauseResumeText.text = "Pause";
        }
        else
        {
            PauseResume.PauseGame();
            pauseResumeText.text = "Resume";
        }
    }
    
}
