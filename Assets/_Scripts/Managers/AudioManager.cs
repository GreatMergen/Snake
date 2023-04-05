using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class AudioManager : MonoBehaviour
{
  public AudioSource coinSource, deathSource;
  [SerializeField] private AudioClip coinClip, deathClip;
  private void Start()
  {
     Events.OnGameOver.AddListener(PlayDeathSound);
     Events.OnFoodTake.AddListener(PlayCoinSound);
  }

  private void OnDisable()
  {
      Events.OnGameOver.RemoveListener(PlayDeathSound);
      Events.OnFoodTake.RemoveListener(PlayCoinSound);
  }
  private void PlayDeathSound() => deathSource.PlayOneShot(deathClip);
  private void PlayCoinSound() =>coinSource.PlayOneShot(coinClip);
}
