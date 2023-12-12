using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    [SerializeField] private List<GameObject> screens;

    private GameObject _currentScreen;

    public void DisableAllScreens()
    {
        foreach (var screen in screens)
        {
            screen.gameObject.SetActive(false);
        }
    }

    public void OpenWaitGameStartScreen()
    {
        var waitGameStartScreen = GetScreenWithComponent(typeof(WaitGameStartScreen));
        OpenScreen(waitGameStartScreen);
    }

    public void OpenInHudScreen()
    {
        var inHudScreen = GetScreenWithComponent(typeof(InGameHudScreen));
        OpenScreen(inHudScreen);
    }

    public void OpenGameOverScreen()
    {
        var gameOverScreen = GetScreenWithComponent(typeof(GameOverScreen));
        OpenScreen(gameOverScreen);
    }

    public void OpenPauseScreen()
    {
        var pauseScreen = GetScreenWithComponent(typeof(PauseScreen));
        OpenScreen(pauseScreen);
    }

    private void OpenScreen(GameObject screen)
    {
        CloseCurrentScreen();
        screen.SetActive(true);
        _currentScreen = screen;
    }

    private void CloseCurrentScreen()
    {
        if (_currentScreen == null) return;
        _currentScreen.gameObject.SetActive(false);
    }
    
    //TODO: Usar generics
    private GameObject GetScreenWithComponent(Type type)
    {
        foreach (var screen in screens)
        {
            if (screen.GetComponent(type) != null)
            {
                return screen;
            }
        }

        return null;
    }
}
