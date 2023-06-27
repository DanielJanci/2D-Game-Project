using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int currentScore;
    public int recordScore;
    public DataManager dataManager;

    public TMP_Text currentScoreText;
    public TMP_Text finalScoreText;
    public TMP_Text recordScoreText;
    
    private void Start()
    {
        LoadRecordScore();
        currentScore = 0;
        currentScoreText.text = $"Score: {currentScore}";
        Player.OnPlayerDeath += ShowLevelOver;
        Player.OnCoinCollected += UpdateCurrentScore;
    }

    private void LoadRecordScore()
    {
        dataManager.Load();
        if (SceneManager.GetActiveScene().name == "Level_01")
        {
            recordScore = dataManager.Data.recordLevel01;
        }
        else if(SceneManager.GetActiveScene().name == "Level_02")
        {
            recordScore = dataManager.Data.recordLevel02;
        }
        else if(SceneManager.GetActiveScene().name == "Level_03")
        {
            recordScore = dataManager.Data.recordLevel03;
        }
    }
    
    private void SaveRecordScore()
    {
        if (currentScore > recordScore)
        {
            recordScore = currentScore;
            if (SceneManager.GetActiveScene().name == "Level_01")
            {
                dataManager.Data.recordLevel01 = recordScore;
            }
            else if(SceneManager.GetActiveScene().name == "Level_02")
            {
                dataManager.Data.recordLevel02 = recordScore;
            }
            else if(SceneManager.GetActiveScene().name == "Level_03")
            {
                dataManager.Data.recordLevel03 = recordScore;
            }
            dataManager.Save();
        }
        
    }

    private void UpdateCurrentScore()
    {
        AudioManager.OnCoinCollect?.Invoke();
        currentScore++;
        currentScoreText.text = $"Score: {currentScore}";
    }
    
    private void ShowLevelOver()
    {
        AudioManager.OnGameOver?.Invoke();
        SaveRecordScore();
        finalScoreText.text = $"Score: {currentScore}";
        recordScoreText.text = $"Record: {recordScore}";
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        Player.OnPlayerDeath -= ShowLevelOver;
        Player.OnCoinCollected -= UpdateCurrentScore;
    }
    
    public void OnRestartButton()
    {
        AudioManager.OnButtonClick?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void OnLevelSelectButton()
    {
        AudioManager.OnButtonClick?.Invoke();
        SceneManager.LoadScene("LevelSelector");
    }

    public void OnMenuButton()
    {
        AudioManager.OnButtonClick?.Invoke();
        SceneManager.LoadScene("MainMenu");
    }
}
