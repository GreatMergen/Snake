using System;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    private const string SaveKey = "HighScore";
    private void Awake()=>Instance = this;

    private void OnEnable()
    {
        Events.OnGameOver.AddListener(SaveHighScore);
    }

    public void SaveHighScore()
    {
        var currentScore = LevelManager.Instance.score;
        if (PlayerPrefs.HasKey(SaveKey) && currentScore > PlayerPrefs.GetInt(SaveKey))
        {
            PlayerPrefs.SetInt(SaveKey,currentScore);
        }
     
        else if (!PlayerPrefs.HasKey(SaveKey))
        {
            PlayerPrefs.SetInt(SaveKey,currentScore);
        }
    }

    public int LoadHighScore()
    {
        return PlayerPrefs.HasKey(SaveKey) ? PlayerPrefs.GetInt(SaveKey) : 0;
    }
}
