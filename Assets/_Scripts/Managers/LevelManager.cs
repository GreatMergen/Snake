using System;
using System.Collections;
using UnityEngine;
using  UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
   public static LevelManager Instance;
   public event Action OnDeath;
   
   public int score;
   private void Awake()
   {
      Instance = this;
   }

   private void OnEnable()
   {
      Food.OnFoodTake += AddScore;
   }

   private void OnDisable()
   {
      Food.OnFoodTake -= AddScore;
   }

   private void Start()
   {
      UIManager.Instance.ScoreTextUpdate();
   }

   private void Update()
   {
      CanRestart();
   }


   private bool CanRestart()
   {
      return Input.anyKeyDown;
   }
   public void AddScore()
   {
      score ++;
      UIManager.Instance.ScoreTextUpdate();
   }

   public void Dead()
   {
     StartCoroutine(DeadSequance());
   }

   private IEnumerator DeadSequance()
   {
      OnDeath?.Invoke();
      Time.timeScale = 0;
      yield return new WaitForSecondsRealtime(1);
      yield return new WaitUntil(CanRestart);
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      Time.timeScale = 1;
   }

  
}