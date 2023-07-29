using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FadingPanel : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    private Tween _fadeTween; 

    public void FadeIn()
    {
        Fade(1f, 0.5f, () =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        });
    }
    
    public void FadeOut()
    {
        Fade(0f, 0.5f, () =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        });
    }

    private void Fade(float endValue, float duration, TweenCallback onEnd)
    {
        _fadeTween?.Kill();

        _fadeTween = canvasGroup.DOFade(endValue, duration);
        _fadeTween.onComplete += onEnd;
    }

}
