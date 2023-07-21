using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text currentScoreText;
    public TMP_Text finalScoreText;
    public TMP_Text recordScoreText;

    private GameDataManager _gameDataManager;
    private Dictionary<string, string> _sceneNameToLevelScoreName;
    private string _currentLevelScoreName;
    private int _recordScore;
    private int _currentScore;

    private void Start()
    {
        _sceneNameToLevelScoreName = new Dictionary<string, string>()
        {
            {"Level_01", "ScoreLevel01"},
            {"Level_02", "ScoreLevel02"},
            {"Level_03", "ScoreLevel03"},
        };
        
        _gameDataManager = FindObjectOfType<GameDataManager>();
        _currentLevelScoreName = _sceneNameToLevelScoreName[SceneManager.GetActiveScene().name];
        _recordScore = _gameDataManager.RecordScoreData.recordScores[_currentLevelScoreName].StatValue;

        currentScoreText.text = $"Score: {_currentScore}";
        Player.OnPlayerDeath += ShowLevelOver;
        Player.OnCoinCollected += UpdateCurrentScore;
    }
    
    private void SaveRecordScore()
    {
        if (_currentScore > _recordScore)
        {
            _recordScore = _currentScore;
            _gameDataManager.UpdateLocalScoreAndLeadboardData(_recordScore, _currentLevelScoreName);
        }
    }

    private void UpdateCurrentScore()
    {
        AudioManager.OnCoinCollect?.Invoke();
        _currentScore++;
        currentScoreText.text = $"Score: {_currentScore}";
    }
    
    private void ShowLevelOver()
    {
        AudioManager.OnGameOver?.Invoke();
        SaveRecordScore();
        
        finalScoreText.text = $"Score: {_currentScore}";
        recordScoreText.text = $"Record: {_recordScore}";
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        _gameDataManager.AddCoinsToInventoryPLayFabData(_currentScore);
        _gameDataManager.UpdatePlayFabLeaderboard(_currentLevelScoreName);
        Player.OnPlayerDeath -= ShowLevelOver;
        Player.OnCoinCollected -= UpdateCurrentScore;
    }
    
    public void OnRestartButton()
    {
        AudioManager.OnButtonClick?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMenuButton()
    {
        AudioManager.OnButtonClick?.Invoke();
        SceneManager.LoadScene("MainMenu");
    }
}
