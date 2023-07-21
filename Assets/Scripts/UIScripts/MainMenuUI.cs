using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MainMenuUI : MonoBehaviour
{
    public GameObject settingsCanvas;
    public GameObject leaderboardCanvas;
    public GameObject shopCanvas;
    public GameObject levelSelectorCanvas;
    public GameObject nicknameInputCanvas;
    public GameObject startGameCanvas;
    public GameObject inventoryCanvas;
    
    public static UnityAction OnShopClicked;
    public static UnityAction OnLeaderboardClicked;
    public static UnityAction OnStartClicked;
    public static UnityAction OnInventoryClicked;
    public static UnityAction OnSettingsClicked;

    private GameDataManager _gameDataManager;
    private void Start()
    {
        _gameDataManager = FindObjectOfType<GameDataManager>();
        StartCoroutine(ShowStartCanvas());
        StartCoroutine(ShowNicknameCanvas());
    }

    private IEnumerator ShowStartCanvas()
    {
        yield return new WaitUntil(() => _gameDataManager.UserData != null);
        yield return new WaitUntil(() => _gameDataManager.UserData.isLoggedIn && _gameDataManager.UserData.userDisplayName != null);
        nicknameInputCanvas.SetActive(false);
        startGameCanvas.SetActive(true); 
    }

    private IEnumerator ShowNicknameCanvas()
    {
        yield return new WaitUntil(() => _gameDataManager.UserData != null);
        yield return new WaitUntil(() => _gameDataManager.UserData.isLoggedIn && _gameDataManager.UserData.userDisplayName == null);
        nicknameInputCanvas.SetActive(true);
        startGameCanvas.SetActive(false);
    }

    public void OnStartButton()
    {
        AudioManager.OnButtonClick?.Invoke();
        levelSelectorCanvas.SetActive(true);
        OnStartClicked?.Invoke();
    }
    
    public void OnShopButton()
    { 
       AudioManager.OnButtonClick?.Invoke();
       shopCanvas.SetActive(true);
       OnShopClicked?.Invoke();
    }
    
    public void OnLeaderBoardButton()
    {
        AudioManager.OnButtonClick?.Invoke();
        leaderboardCanvas.SetActive(true);
        OnLeaderboardClicked?.Invoke();
    }
    
    public void OnSettingsButton()
    {
        AudioManager.OnButtonClick?.Invoke();
        settingsCanvas.SetActive(true);
        OnSettingsClicked?.Invoke();
    }
    
    public void OnInventoryButton()
    {
        AudioManager.OnButtonClick?.Invoke();
        inventoryCanvas.SetActive(true);
        OnInventoryClicked?.Invoke();
    }
    
    public void OnQuitButton()
    {
        AudioManager.OnButtonClick?.Invoke();
        Application.Quit();
    }
}
