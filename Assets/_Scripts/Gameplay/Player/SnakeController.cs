using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject tail,food;
    public List<Transform> tailList = new List<Transform>();
    public Vector2 camBorder;
    private Vector2 _moveDir;

    private void Start()
    {
        _moveDir = Vector2.right;
        tailList.Add(transform);
        ChangeFoodPosition();
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
        
        if (transform.position.x >= camBorder.x +1 || transform.position.x <= -camBorder.x)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y);
        }

        if (transform.position.y >= camBorder.y + 1 || transform.position.y <= -camBorder.y)
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y);
        }
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            for (int i = tailList.Count-1; i > 0; i--)
            {
                tailList[i].position = tailList[i - 1].position;
            }

            var position = transform.position; 
            position += (Vector3)_moveDir;
            position.x = Mathf.RoundToInt(position.x);
            position.y = Mathf.RoundToInt(position.y);
            transform.position = position;
            
            
            yield return new WaitForSeconds(moveSpeed);
        }
    }
    
   
    
    private void Grow()
    {
        var _tail =  Instantiate(tail);
        tailList.Add(_tail.transform);
        tailList[^1].position = tailList[^2].position;
    }

    private void Dead()
    {
        
    }

    private void ChangeFoodPosition()
    {
        var x = (int) UnityEngine.Random.Range(-camBorder.x, camBorder.x);
        var y = (int) UnityEngine.Random.Range(-camBorder.y, camBorder.y);
        food.transform.position = new Vector3(x, y,0);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Food"))
        {
            Grow();
            ChangeFoodPosition();
        }

      
    }
}
