using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
        nicknameInput.characterLimit = 8;
    }

    public void OnSaveNameButton()
    {
        AudioManager.OnButtonClick?.Invoke();
        if (Regex.IsMatch(nicknameInput.text, "^[a-zA-Z0-9]*$") && nicknameInput.text.Length > 0)
        {
            _gameDataManager.UpdatePlayFabUserName(nicknameInput.text);
        }
    }

}
