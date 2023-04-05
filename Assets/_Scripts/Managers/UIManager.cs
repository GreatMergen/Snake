using System;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TextMeshPro scoreText;
    [SerializeField] private GameObject startTexts;
    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        LevelManager.Instance.OnDeath += EnableStartTexts;
    }

    private void OnDisable()
    {
        LevelManager.Instance.OnDeath -= EnableStartTexts;
    }

    public void EnableStartTexts()
    {
        startTexts.SetActive(!startTexts.activeSelf);
    }
    public void ScoreTextUpdate()
    {
        scoreText.text = LevelManager.Instance.score.ToString();
    }
}
