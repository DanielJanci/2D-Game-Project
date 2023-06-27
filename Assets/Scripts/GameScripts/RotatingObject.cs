using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;

public class RotatingObject : MonoBehaviour, IPoolable
{
    public float rotationDuration = 3f;

    private Sequence _rotationSequence;
    private Vector3 _originalRotation;

    private void Awake()
    {
        _originalRotation = transform.rotation.eulerAngles;
    }

    private void Update()
    {
        if (PauseResume.IsGamePaused)
        {
            _rotationSequence.Pause();
        }
        else
        {
            _rotationSequence.Play();
        }
    }
    
    public void OnSpawn()
    {
        Transform t = transform;
        _rotationSequence = DOTween.Sequence(this);
        _rotationSequence.Append(t.DORotate(new Vector3(_originalRotation.x, _originalRotation.y, 360f), rotationDuration, RotateMode.FastBeyond360));
        _rotationSequence.SetLoops(-1);
    }

    public void OnDespawn()
    {
        DOTween.Kill(this);
    }
}
