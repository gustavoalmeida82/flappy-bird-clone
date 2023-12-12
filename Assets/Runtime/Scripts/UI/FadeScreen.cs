using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FadeScreen : MonoBehaviour
{
    private Image _fadeImage;

    private void Awake()
    {
        _fadeImage = GetComponent<Image>();
    }

    public IEnumerator Flash()
    {
        yield return StartCoroutine(FadeIn(0.05f, Color.white));
        yield return StartCoroutine(FadeOut(0.05f, Color.white));
    }

    public IEnumerator FadeIn(float fadeTime, Color fadeColor)
    {
        _fadeImage.enabled = true;
        fadeColor.a = 0;
        _fadeImage.color = fadeColor;
        Tween fadeTween = _fadeImage.DOFade(1, fadeTime);
        yield return fadeTween.WaitForCompletion();
    }

    public IEnumerator FadeOut(float fadeTime, Color fadeColor)
    {
        _fadeImage.enabled = true;
        fadeColor.a = 1;
        _fadeImage.color = fadeColor;
        Tween fadeTween = _fadeImage.DOFade(0, fadeTime);
        yield return fadeTween.WaitForCompletion();
        _fadeImage.enabled = false;
    }
}
