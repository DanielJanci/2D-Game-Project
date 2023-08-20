using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public TMP_Text volumeText;
    public Slider volumeSlider;
    
    private AudioManager _audioManager;
    private GameDataManager _gameDataManager;

    public void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _gameDataManager = FindObjectOfType<GameDataManager>();
        MainMenuUI.OnSettingsClicked += ShowSettingsUI;
    }

    private void ShowSettingsUI()
    {
        volumeText.text = $"VOLUME: {Math.Round( _audioManager.GetVolume() * 100)}%";
        volumeSlider.value =  _audioManager.GetVolume();
        volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(volumeSlider.value); });
    }

    private void ChangeVolume(float sliderValue)
    {
        volumeText.text = $"VOLUME: {Math.Round(sliderValue * 100)}%";
    }

    public void OnDeleteAccountButton()
    {
        AudioManager.OnButtonClick?.Invoke();
    }

    public void OnConfirmDeleteAccountButton()
    {
        AudioManager.OnButtonClick?.Invoke();
        _gameDataManager.DeleteMasterPLayerAccount();
    }

    public void OnCancelDeleteAccountButton()
    {
        AudioManager.OnButtonClick?.Invoke();
    }
    
    private void OnDestroy()
    {
        MainMenuUI.OnSettingsClicked -= ShowSettingsUI;
    }

    public void OnReturnButton()
    {
        _audioManager.UpdateVolume(volumeSlider.value);
        AudioManager.OnButtonClick?.Invoke();
        //gameObject.SetActive(false);
    }

    
}
