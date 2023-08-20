using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LeaderboardUI : MonoBehaviour
{
    public GameObject rowPreFab;
    public GameObject rowsParentLevel01;
    public GameObject rowsParentLevel02;
    public GameObject rowsParentLevel03;
    public Button defaultLevelButton;
    
    private Dictionary<string, Transform> _rowsParents;
    
    private GameDataManager _gameDataManager;
    
    private void Start()
    {
        _rowsParents = new Dictionary<string, Transform>()
        {
            { "ScoreLevel01", rowsParentLevel01.transform },
            { "ScoreLevel02", rowsParentLevel02.transform },
            { "ScoreLevel03", rowsParentLevel03.transform },
        };
        
        _gameDataManager = FindObjectOfType<GameDataManager>();
        MainMenuUI.OnLeaderboardClicked += LoadAllLeaderboards;
        MainMenuUI.OnLeaderboardClicked += ShowDefaultLeaderboard;
        MainMenuUI.OnLeaderboardClicked += defaultLevelButton.Select;
        
    }

    private IEnumerator GetLoadedLeaderboard(string level)
    {
        yield return new WaitUntil(() => _gameDataManager.LeaderboardData != null);
        yield return new WaitUntil(() => _gameDataManager.LeaderboardData.isDataLoaded[level]);
        GetLeaderboard(level);
    }

    private void LoadAllLeaderboards()
    {
        StartCoroutine(GetLoadedLeaderboard("ScoreLevel01"));
        StartCoroutine(GetLoadedLeaderboard("ScoreLevel02"));
        StartCoroutine(GetLoadedLeaderboard("ScoreLevel03"));
    }
    private void ShowDefaultLeaderboard()
    {
        rowsParentLevel01.SetActive(true);
        rowsParentLevel02.SetActive(false);
        rowsParentLevel03.SetActive(false);
    }
    public void OnLevel01Button()
    {
        AudioManager.OnButtonClick?.Invoke();
        rowsParentLevel01.SetActive(true);
        rowsParentLevel02.SetActive(false);
        rowsParentLevel03.SetActive(false);
    }
    
    public void OnLevel02Button()
    {
        AudioManager.OnButtonClick?.Invoke();
        rowsParentLevel01.SetActive(false);
        rowsParentLevel02.SetActive(true);
        rowsParentLevel03.SetActive(false);    
    }
    
    public void OnLevel03Button()
    {
        AudioManager.OnButtonClick?.Invoke();
        rowsParentLevel01.SetActive(false);
        rowsParentLevel02.SetActive(false);
        rowsParentLevel03.SetActive(true);
    }

    
    private void GetLeaderboard(string level)
    {
        foreach (Transform row in _rowsParents[level])
        {
            Destroy(row.gameObject);
        }

        foreach (var entry in _gameDataManager.LeaderboardData.leaderboard[level])
        {
            GameObject newGameObject = Instantiate(rowPreFab, _rowsParents[level]);
            TMP_Text[] texts = newGameObject.GetComponentsInChildren<TMP_Text>();
            
            if (entry.PlayFabId == _gameDataManager.UserData.userPlayFabId)
            {
                if (entry.DisplayName == null)
                {
                    entry.DisplayName = _gameDataManager.UserData.userDisplayName;
                }
                texts[0].color = Color.cyan;
                texts[1].color = Color.cyan;
                texts[2].color = Color.cyan;
            }
            texts[0].text = (entry.Position + 1).ToString();
            texts[1].text = entry.DisplayName;
            texts[2].text = entry.StatValue.ToString();
        }
    }
    
    private void OnDestroy()
    {
        MainMenuUI.OnLeaderboardClicked -= LoadAllLeaderboards;
        MainMenuUI.OnLeaderboardClicked -= ShowDefaultLeaderboard;
        MainMenuUI.OnLeaderboardClicked -= defaultLevelButton.Select;
    }

    public void OnReturnButton()
    {
        AudioManager.OnButtonClick?.Invoke();
        //gameObject.SetActive(false);
    }
}
