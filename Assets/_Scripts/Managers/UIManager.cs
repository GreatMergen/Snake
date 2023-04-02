using System;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TextMeshPro scoreText;
    private void Awake()
    {
        Instance = this;
    }

   

    public void ScoreTextUpdate()
    {
        scoreText.text = LevelManager.Instance.score.ToString();
    }
}
