using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;

    public void OnContinueButtonClicked()
    {
        gameMode.ContinueGame();
    }
}
