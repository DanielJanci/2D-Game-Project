using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public TMP_Text recordScoreLevel01;
    public TMP_Text recordScoreLevel02;
    public TMP_Text recordScoreLevel03;
    public DataManager dataManager;
    
    private void Start()
    {
        dataManager.Load();
        recordScoreLevel01.text = $"Record: {dataManager.Data.recordLevel01}";
        recordScoreLevel02.text = $"Record: {dataManager.Data.recordLevel02}";
        recordScoreLevel03.text = $"Record: {dataManager.Data.recordLevel03}";
    }

    public void OnMenuButton()
    {
        AudioManager.OnButtonClick?.Invoke();
        SceneManager.LoadScene("MainMenu");
    }

    public void OnSelectLevel01()
    {
        AudioManager.OnButtonClick?.Invoke();
        SceneManager.LoadScene("Level_01");
    }
    
    public void OnSelectLevel02()
    {
        AudioManager.OnButtonClick?.Invoke();
        SceneManager.LoadScene("Level_02");
    }
    
    public void OnSelectLevel03()
    {
        AudioManager.OnButtonClick?.Invoke();
        SceneManager.LoadScene("Level_03");
    }
}
