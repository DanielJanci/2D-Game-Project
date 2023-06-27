using System;
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
        OnCoinCollect += PlayCoinCollectSound;
        OnButtonClick += PlayButtonClickSound;
        OnGameOver += PlayGameOverSound;
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
