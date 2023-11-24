using System;
using System.Collections;
using System.Collections.Generic;
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

    private MedalRewardCalculator _medalRewardCalculator;

    private void Awake()
    {
        _medalRewardCalculator = GetComponent<MedalRewardCalculator>();
    }

    private void OnEnable()
    {
        UpdateUI();
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
}
