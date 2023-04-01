using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   [SerializeField] private float speed;
   
   private float _moveX, _moveY;
   private Vector2 _moveDir;

   private void Start()
   {
      _moveDir = Vector2.right;
   }

   private void Update()
   {
      
      if (Input.GetKeyDown(KeyCode.W))
      {
         _moveDir = Vector2.up;
      }
      else if (Input.GetKeyDown(KeyCode.S))
      {
         _moveDir = Vector2.down;
      }
      else if (Input.GetKeyDown(KeyCode.D))
      {
         _moveDir =Vector2.right;
      }
      else if (Input.GetKeyDown(KeyCode.A))
      {
         _moveDir = Vector2.left;
      }


      transform.position +=(Vector3)_moveDir * speed *Time.deltaTime;

      if (transform.position.x >= 10.5f || transform.position.x <= -10.5f)
      {
         transform.position = new Vector3(-transform.position.x, transform.position.y);
      }

      if (transform.position.y is >= 5.5f or <= -5.5f)
      {
         transform.position = new Vector3(transform.position.x, -transform.position.y);
      }
    
   }
 
  
}
