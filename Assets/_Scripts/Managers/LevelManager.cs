using System;
using UnityEngine;

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
}