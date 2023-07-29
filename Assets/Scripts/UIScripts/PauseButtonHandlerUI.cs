using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PauseButtonHandlerUI : MonoBehaviour
{
    public Sprite pauseSprite;
    public Sprite resumeSprite;
    public Button playPauseButton;
    public Button volumeButton;
    public Button homeButton;
    
    private Sequence _volumeButtonMovement;
    private Sequence _homeButtonMovement;
    
    private void Start()
    {
        playPauseButton.image.sprite = pauseSprite;
    }

    public void OnPauseResumeButton()
    {
        AudioManager.OnButtonClick?.Invoke();
        RectTransform volumeRect = volumeButton.transform as RectTransform;
        RectTransform homeRect = homeButton.transform as RectTransform;
        
        if (PauseResume.IsGamePaused)
        {
            PauseResume.ResumeGame();
            playPauseButton.image.sprite = pauseSprite;
            volumeRect.DOLocalMoveY(160, 0.2f).SetRelative();
            homeRect.DOLocalMoveY(320, 0.2f).SetRelative();
        }
        else
        {
            PauseResume.PauseGame();
            playPauseButton.image.sprite = resumeSprite;
            volumeRect.DOLocalMoveY(-160, 0.2f).SetRelative();
            homeRect.DOLocalMoveY(-320, 0.2f).SetRelative();
        }
    }

    public void OnVolumeButton()
    {
        AudioManager.OnButtonClick?.Invoke();
        MainMenuUI.OnSettingsClicked?.Invoke();
    }

    public void OnHomeButton()
    {
        AudioManager.OnButtonClick?.Invoke();
    }

    public void ConfirmEndLevel()
    {
        AudioManager.OnButtonClick?.Invoke();
        Player.OnPlayerDeath?.Invoke();
    }

    public void CancelEndLevel()
    {
        AudioManager.OnButtonClick?.Invoke();
    }
    
}
