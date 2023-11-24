using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitGameStartScreen : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;

    public void OnStartGameButtonClicked()
    {
        gameMode.StartGame();
    }
}
