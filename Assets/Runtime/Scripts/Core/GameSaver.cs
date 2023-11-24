using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameData
{
    public int HighestScore { get; set; }
}

public class GameSaver : MonoBehaviour
{
    private const string HighestScoreKey = "HighestScore";
    public SaveGameData CurrentSave { get; private set; }

    private bool IsLoaded => CurrentSave != null;

    public void SaveGame(SaveGameData saveData)
    {
        CurrentSave = saveData;
        PlayerPrefs.SetInt(HighestScoreKey, CurrentSave.HighestScore);
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        if (IsLoaded) return;

        CurrentSave = new SaveGameData()
        {
            HighestScore = PlayerPrefs.GetInt(HighestScoreKey, 0)
        };
    }
}
