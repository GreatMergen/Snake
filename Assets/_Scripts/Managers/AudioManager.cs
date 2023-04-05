using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class AudioManager : MonoBehaviour
{
  public AudioSource coinSource, deathSource;
  [SerializeField] private AudioClip coinClip, deathClip;
  public static AudioManager Instance;
  private void Awake()
  {
      Instance = this;
  }

  private void Start()
  {
      LevelManager.Instance.OnDeath += PlayDeathSound;
      Food.OnFoodTake += PlayCoinSound;
  }

  private void OnDisable()
  {
      LevelManager.Instance.OnDeath -= PlayDeathSound;
      Food.OnFoodTake -= PlayCoinSound;
  }

  public void PlayDeathSound()
  {
    deathSource.PlayOneShot(deathClip);
  }

  public void PlayCoinSound()
  {
     // coinSource.pitch = Random.Range(0.5f, 1f);
      coinSource.PlayOneShot(coinClip);
  }
}
