using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnStartButton()
    {
        AudioManager.OnButtonClick?.Invoke();
        SceneManager.LoadScene("LevelSelector");
    }
    
    public void OnQuitButton()
    {
        AudioManager.OnButtonClick?.Invoke();
        Application.Quit();
    }
}
