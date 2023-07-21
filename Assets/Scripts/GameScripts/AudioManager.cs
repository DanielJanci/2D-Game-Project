using System;
using Unity.VisualScripting;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    public AudioSource buttonClickSource;
    public AudioSource coinCollectSource;
    public AudioSource gameOverSource;
    
    public static UnityAction OnButtonClick;
    public static UnityAction OnCoinCollect;
    public static UnityAction OnGameOver;
    
    public static AudioManager instance;

    private GameDataManager _gameDataManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _gameDataManager = FindObjectOfType<GameDataManager>();
        GameDataManager.OnSettingsDataLoaded += SetAudioVolume;
        OnCoinCollect += PlayCoinCollectSound;
        OnButtonClick += PlayButtonClickSound;
        OnGameOver += PlayGameOverSound;
    }

    public float GetVolume()
    {
        return _gameDataManager.SettingsData.volume;
    }

    private void SetAudioVolume()
    {
        buttonClickSource.volume = _gameDataManager.SettingsData.volume;
        coinCollectSource.volume = _gameDataManager.SettingsData.volume;
        gameOverSource.volume = _gameDataManager.SettingsData.volume;
    }
    
    public void UpdateVolume(float newVolume)
    {
        buttonClickSource.volume = newVolume;
        coinCollectSource.volume = newVolume;
        gameOverSource.volume = newVolume;
        _gameDataManager.SettingsData.volume = newVolume;
        _gameDataManager.SaveSettingsData();
    }
    
    private void PlayButtonClickSound()
    {
        buttonClickSource.Play();
    }

    private void PlayCoinCollectSound()
    {
        coinCollectSource.Play();
    }

    private void PlayGameOverSound()
    {
        gameOverSource.Play();
    }
}
