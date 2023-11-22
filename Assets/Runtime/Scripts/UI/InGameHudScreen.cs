using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameHudScreen : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void LateUpdate()
    {
        scoreText.text = gameMode.Score.ToString();
    }
}
