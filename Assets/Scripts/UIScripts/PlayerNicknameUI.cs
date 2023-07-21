using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNicknameUI : MonoBehaviour
{
    public TMP_InputField nicknameInput;
    private GameDataManager _gameDataManager;
    
    private void Start()
    {
        _gameDataManager = FindObjectOfType<GameDataManager>();
    }

    public void OnSaveNameButton()
    {
        AudioManager.OnButtonClick?.Invoke();
        if (nicknameInput.text.Length > 0)
        {
            _gameDataManager.UpdatePlayFabUserName(nicknameInput.text);
        }
    }

}
