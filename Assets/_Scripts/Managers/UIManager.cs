using TMPro;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TextMeshPro scoreText;
    [SerializeField] private GameObject startTexts;
    [SerializeField] private TextMeshPro highScoreText; 
    private void Awake() =>  Instance = this;
    private void OnEnable()
    { 
        Events.OnGameStart.AddListener(EnableStartTexts);
        Events.OnGameStart.AddListener(HighScoreTextUpdate);
        Events.OnGameOver.AddListener(GameOverTextUpdate);
        Events.OnGameOver.AddListener(HighScoreTextUpdate);
    }

    private void OnDisable()
    {
        Events.OnGameStart.RemoveListener(EnableStartTexts);
        Events.OnGameStart.RemoveListener(HighScoreTextUpdate);
        Events.OnGameOver.RemoveListener(GameOverTextUpdate);
        Events.OnGameOver.RemoveListener(HighScoreTextUpdate);
       
    }

    private void EnableStartTexts() =>startTexts.SetActive(!startTexts.activeSelf);
    private void GameOverTextUpdate() =>startTexts.SetActive(!startTexts.activeSelf);
    public void ScoreTextUpdate() => scoreText.text = LevelManager.Instance.score.ToString();
    public void HighScoreTextUpdate() => highScoreText.text = SaveManager.Instance.LoadHighScore().ToString();
}
