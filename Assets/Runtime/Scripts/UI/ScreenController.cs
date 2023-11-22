using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    [SerializeField] private List<GameObject> screens;

    private GameObject _currentScreen;

    private void Awake()
    {
        foreach (var screen in screens)
        {
            screen.gameObject.SetActive(false);
        }
    }

    public void OpenInHudScreen()
    {
        var inHudScreen = GetScreenWithComponent(typeof(InGameHudScreen));
        OpenScreen(inHudScreen);
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
