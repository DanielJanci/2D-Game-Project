using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;
using Lean.Pool;
using Random = UnityEngine.Random;

public class StretchingObject : MonoBehaviour, IPoolable
{
    public float maxScaleX = 3f;
    public float scaleDuration = 1f;
    
    private Sequence _stretchingSequence;
    private Vector3 _originalScale;

    private void Awake()
    {
        _originalScale = transform.localScale;
    }
    
    private void Update()
    {
        if (PauseResume.IsGamePaused)
        {
            _stretchingSequence.Pause();
        }
        else
        {
            _stretchingSequence.Play();
        }
    }

    public void OnSpawn()
    {
        Transform t = transform;
        Vector3 localScale = new Vector3(_originalScale.x, _originalScale.y, _originalScale.z);
        t.localScale = localScale;
        float newScaleX = Random.Range(_originalScale.x, maxScaleX);
        
        _stretchingSequence = DOTween.Sequence(this);
        _stretchingSequence.Append(t.DOScaleX(newScaleX, scaleDuration).SetEase(Ease.Linear));
        _stretchingSequence.Append(t.DOScaleX(_originalScale.x, scaleDuration).SetEase(Ease.Linear));
        _stretchingSequence.SetLoops(-1);
    }

    public void OnDespawn()
    {
        DOTween.Kill(this);
    }
}
