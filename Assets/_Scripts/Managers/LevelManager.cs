using System;
using System.Collections;
using UnityEngine;
using  UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
   public static LevelManager Instance;

   public int score;
   private void Awake()
   {
      Instance = this;
   }

   private void Start()
   {
      UIManager.Instance.ScoreTextUpdate();
   }

   public void AddScore(int amount)
   {
      score += amount;
      UIManager.Instance.ScoreTextUpdate();
   }

   public void Dead()
   {
     StartCoroutine(DeadSequance());
   }

   private IEnumerator DeadSequance()
   {
      Time.timeScale = 0;
      yield return new WaitForSecondsRealtime(1);
      print("dass");
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      Time.timeScale = 1;
   }
}