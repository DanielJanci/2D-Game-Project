using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelSelectorUI : MonoBehaviour
{
    public TMP_Text recordScoreLevel01;
    public TMP_Text recordScoreLevel02;
    public TMP_Text recordScoreLevel03;

    private GameDataManager _gameDataManager;
    private Dictionary<string, TMP_Text> _recordScoreTexts;

    private void Start()
    {
        _recordScoreTexts = new Dictionary<string, TMP_Text>()
        {
            { "ScoreLevel01", recordScoreLevel01 },
            { "ScoreLevel02", recordScoreLevel02 },
            { "ScoreLevel03", recordScoreLevel03 },
        };
        
        _gameDataManager = FindObjectOfType<GameDataManager>();
        MainMenuUI.OnStartClicked += LoadAllRecordScores;
    }

    public void OnReturnButton()
    {
        AudioManager.OnButtonClick?.Invoke();
        //gameObject.SetActive(false);
    }

    private IEnumerator GetLoadedRecordScore(string level)
    {
        yield return new WaitUntil(() => _gameDataManager.RecordScoreData != null);
        yield return new WaitUntil(() => _gameDataManager.RecordScoreData.isDataLoaded[level]);
        _recordScoreTexts[level].text = $"Record: {_gameDataManager.RecordScoreData.recordScores[level].StatValue}";
    }
    
    private void LoadAllRecordScores()
    {
        StartCoroutine(GetLoadedRecordScore("ScoreLevel01"));
        StartCoroutine(GetLoadedRecordScore("ScoreLevel02"));
        StartCoroutine(GetLoadedRecordScore("ScoreLevel03"));
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

    private void OnDestroy()
    {
        MainMenuUI.OnStartClicked -= LoadAllRecordScores;
    }
}
