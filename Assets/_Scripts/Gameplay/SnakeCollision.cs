using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeCollision : MonoBehaviour
{

    private SnakeEvents _snakeEvents;
    private void Start()
    {
        _snakeEvents = GetComponent<SnakeEvents>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Apple"))
        {
            _snakeEvents.AteApple();
            Destroy(col.gameObject);
        }
    }
}
