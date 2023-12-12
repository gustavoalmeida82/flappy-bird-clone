using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameMode : MonoBehaviour
{
    [SerializeField] private int scoreIncrement = 1;
    [SerializeField] private ScreenController screenController;
    [SerializeField] private EndlessPipeGenerator pipeGenerator;
    [SerializeField] private PlayerController player;
    [SerializeField] private GameSaver gameSaver;
    
    [Header("Player Movement Parameters")] 
    [SerializeField] private PlayerMovementParameters gameRunningParameters;
    [SerializeField] private PlayerMovementParameters gameOverParameters;
    [SerializeField] private PlayerMovementParameters gameWaitingParameters;

    [Header("Audio")] 
    [SerializeField] private AudioService audioService;
    [SerializeField] private AudioClip fallAudio;
    [SerializeField] private float fallAudioDelay = 0.3f;
    
    [Header("Fade Effect")]
    [SerializeField] private FadeScreen fadeScreen;
    [SerializeField] private float fadeTime = 0.5f;

    public int Score { get; private set; } = 0;
    public int BestScore => gameSaver.CurrentSave.HighestScore < Score ? Score : gameSaver.CurrentSave.HighestScore;

    private void Awake()
    {
        WaitGameStart();
    }

    private void WaitGameStart()
    {
        gameSaver.LoadGame();
        player.MovementParameters = gameWaitingParameters;
        AudioUtility.AudioService = audioService;

        StartCoroutine(fadeScreen.FadeOut(fadeTime, Color.black));
        screenController.DisableAllScreens();
        screenController.OpenWaitGameStartScreen();
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
        gameSaver.SaveGame(new SaveGameData
        {
            HighestScore = BestScore
        });
        StartCoroutine(fadeScreen.Flash());
        StartCoroutine(GameOverCor());
    }

    private IEnumerator GameOverCor()
    {
        screenController.OpenGameOverScreen();
        yield return new WaitForSeconds(fallAudioDelay);
        if (!player.IsGrounded)
        {
            AudioUtility.PlayAudioCue(fallAudio);
        }
    }
    
    public void ScoreUp()
    {
        Score += scoreIncrement;
    }

    public void PauseGame()
    {
        screenController.OpenPauseScreen();
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        screenController.OpenInHudScreen();
        Time.timeScale = 1;
    }

    public void ReloadGame()
    {
        StartCoroutine(ReloadGameCor());
    }

    public IEnumerator ReloadGameCor()
    {
        yield return fadeScreen.FadeIn(fadeTime, Color.black);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
