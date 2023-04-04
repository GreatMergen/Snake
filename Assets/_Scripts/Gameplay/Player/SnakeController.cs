using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject tail;
   
    public List<Transform> tailList = new List<Transform>();
    
    private Vector2 _moveDir;
  

    private void Start()
    {
        _moveDir = Vector2.right;
        tailList.Add(transform);
       
        StartCoroutine(MoveRoutine());
    }

    private void Update()
    {

        var hor = Input.GetAxisRaw("Horizontal");
        var ver = Input.GetAxisRaw("Vertical");

        Vector2 inputDir = new Vector2(hor, ver);
        Vector2 tempMoveDir = Vector2.zero;

        if (inputDir.x >= 1)
        {
            tempMoveDir = Vector2.right;
        }
        else if (inputDir.x <= -1)
        {
            tempMoveDir = Vector2.left;
        }
        else if (inputDir.y >= 1)
        {
            tempMoveDir = Vector2.up;
        }
        else if (inputDir.y <= -1)
        {
            tempMoveDir = Vector2.down;
        }

        if (Vector2.Dot(_moveDir, tempMoveDir) > -0.9f && tempMoveDir != Vector2.zero)
        {
            _moveDir = tempMoveDir;
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
    
   
    
    public void Grow()
    {
        var _tail =  Instantiate(tail);
        tailList.Add(_tail.transform);
        tailList[^1].position = tailList[^2].position;
     
    }

   
    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.TryGetComponent<ICollideble>(out var collideble))
        {
            collideble.Collide(this);
        }
    }
}
