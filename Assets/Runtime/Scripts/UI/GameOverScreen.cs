using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;
    
    [Header("Elements")]
    [SerializeField] private Image medalImage;
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    [Space]
    [Header("Containers")]
    [SerializeField] private CanvasGroup gameOverContainer;
    [SerializeField] private CanvasGroup statsContainer;
    [SerializeField] private CanvasGroup buttonsContainer;

    [Space] 
    [Header("Game Over Tween")] 
    [SerializeField] private Transform gameOverTweenStart;
    [SerializeField] private float gameOverTweenTimeSeconds = 0.2f;
    
    [Space]
    [Header("Stats Tween")]
    [SerializeField] private Transform statsTweenStart;
    [SerializeField] private float statsTweenTimeSeconds = 0.5f;
    [SerializeField] private float statsTweenDelaySeconds = 0.3f;

    [Space] 
    [Header("Buttons Tween")] 
    [SerializeField] private float buttonsTweenTimeSeconds = 0.5f;
    [SerializeField] private float buttonsTweenDelaySeconds = 0.5f;

    private MedalRewardCalculator _medalRewardCalculator;

    private void Awake()
    {
        _medalRewardCalculator = GetComponent<MedalRewardCalculator>();
    }

    private void OnEnable()
    {
        UpdateUI();
        StartCoroutine(ShowCoroutine());
    }

    private void UpdateUI()
    {
        currentScoreText.text = gameMode.Score.ToString();
        highScoreText.text = gameMode.BestScore.ToString();

        var medal = _medalRewardCalculator.GetMedalForScore(gameMode.Score);
        if (medal != null)
        {
            medalImage.sprite = medal.MedalSprite;
        }
        else
        {
            medalImage.gameObject.SetActive(false);
        }
    }

    public void OnRetryButtonClicked()
    {
        gameMode.ReloadGame();
    }

    public void OnQuitButtonClicked()
    {
        gameMode.QuitGame();
    }

    private IEnumerator ShowCoroutine()
    {
        gameOverContainer.alpha = 0;
        gameOverContainer.interactable = false;
        
        statsContainer.alpha = 0;
        statsContainer.interactable = false;

        buttonsContainer.alpha = 0;
        buttonsContainer.interactable = false;

        yield return StartCoroutine(
            AnimateCanvasGroup(
                gameOverContainer,
                gameOverTweenStart.position,
                gameOverContainer.transform.position,
                gameOverTweenTimeSeconds
            )
        );

        yield return new WaitForSeconds(statsTweenDelaySeconds);
        
        yield return StartCoroutine(
            AnimateCanvasGroup(
                statsContainer,
                statsTweenStart.position,
                statsContainer.transform.position,
                statsTweenTimeSeconds
            )
        );

        yield return new WaitForSeconds(buttonsTweenDelaySeconds);
        
        yield return StartCoroutine(
            AnimateCanvasGroup(
                buttonsContainer,
                buttonsContainer.transform.position,
                buttonsContainer.transform.position,
                buttonsTweenTimeSeconds
            )
        );
    }

    private IEnumerator AnimateCanvasGroup(CanvasGroup group, Vector3 from, Vector3 to, float time)
    {
        group.alpha = 0;
        group.interactable = false;
        
        Tween fadeTween = group.DOFade(1, time);
        group.transform.position = from;
        group.transform.DOMove(to, time);

        yield return fadeTween.WaitForKill();
        group.interactable = true;
    }
}
