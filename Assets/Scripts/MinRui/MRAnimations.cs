using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;

public class MRAnimations : MonoBehaviour
{
    /*
     Written By: Min Rui
     Description: Any primetween related script
     */
    public CanvasGroup _fadeScreen;
    public enum CycleMode
    {
        Yoyo
    }

    [Header("Fade Settings")]
    [Range(0.1f, 3.0f)] public float _fadeInTime = 1.0f;
    [Range(0.1f, 3.0f)] public float _fadeOutTime = 1.0f;
    private Sequence seq;

    public void FadeInCam()
    {
        Tween.Alpha(_fadeScreen, 0f, _fadeInTime, Ease.OutQuart);
    }

    public void FadeOutCam()
    {
        Tween.Alpha(_fadeScreen, 1f, _fadeOutTime, Ease.OutQuart);
    }

    public void fadeInAndOutSequence()
    {
        seq = Sequence.Create();
        seq.ChainCallback(FadeOutCam);
        seq.ChainDelay(_fadeOutTime);
        seq.ChainCallback(FadeInCam);

    }
}
