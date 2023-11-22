using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameMode : MonoBehaviour
{
    [SerializeField] private float scoreIncrement = 1.0f;
    [SerializeField] private ScreenController screenController;
    [SerializeField] private EndlessPipeGenerator pipeGenerator;
    [SerializeField] private PlayerController player;
    
    [Header("Player Movement Parameters")] 
    [SerializeField] private PlayerMovementParameters gameRunningParameters;
    [SerializeField] private PlayerMovementParameters gameOverParameters;

    public float Score { get; private set; } = 0.0f;

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        player.MovementParameters = gameRunningParameters;
        player.Flap();
        pipeGenerator.StartPipeSpawn();
        screenController.OpenInHudScreen();
    }
    
    public void GameOver()
    {
        player.MovementParameters = gameOverParameters;
        StartCoroutine(TEMP_ReloadGame());
    }
    
    public void ScoreUp()
    {
        Score += scoreIncrement;
    }

    private IEnumerator TEMP_ReloadGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
