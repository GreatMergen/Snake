
using System.Collections;
using UnityEngine;
using  UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
   public static LevelManager Instance;
   public int score;
   private void Awake() =>Instance = this;
   private void OnEnable()
   {
      Events.OnFoodTake.AddListener(AddScore);
   }
   
   private void OnDisable()
   {
     Events.OnFoodTake.RemoveListener(AddScore);
   }

   private void Start()
   {
      UIManager.Instance.ScoreTextUpdate();
      UIManager.Instance.HighScoreTextUpdate();
   }

   private void Update()
   {
      if (Input.anyKeyDown)
      {
         Events.OnGameStart.Invoke();
         Events.OnGameStart.DisableEvent();
      }
      CanRestart();
      
   }


   private bool CanRestart() 
   {
      return Input.anyKeyDown;
   }
   
   private void AddScore()
   {
      score ++;
      UIManager.Instance.ScoreTextUpdate();
   }

   public void Dead() => StartCoroutine(DeadSequance());

   private IEnumerator DeadSequance()
   {
      Events.OnGameOver.Invoke();
      Time.timeScale = 0;
      yield return new WaitForSecondsRealtime(1);
      yield return new WaitUntil(CanRestart);
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      Time.timeScale = 1;
   }
}