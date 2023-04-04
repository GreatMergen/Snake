using System;
using System.Collections;
using UnityEngine;

public class Tail : MonoBehaviour,ICollideble
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void Collide(SnakeController snakeController)
    {
        LevelManager.Instance.Dead();
    }
}
