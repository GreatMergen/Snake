using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SnakeMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector2 _moveDir;

    private void Start()
    {
        _moveDir = Vector2.right;


        StartCoroutine(MoveRoutine());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && _moveDir != Vector2.down)
        {
            _moveDir = Vector2.up;
        }
        if (Input.GetKeyDown(KeyCode.S) && _moveDir != Vector2.up)
        {
            _moveDir = Vector2.down;
        }
        if (Input.GetKeyDown(KeyCode.D) && _moveDir != Vector2.left)
        {
            _moveDir = Vector2.right;
        }
        if (Input.GetKeyDown(KeyCode.A) && _moveDir != Vector2.right)
        {
            _moveDir = Vector2.left;
        }
            
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            transform.position = transform.position + (Vector3)_moveDir;
            yield return new WaitForSeconds(moveSpeed);
        }
    }
}
