using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject tailPrefab;
    public List<Transform> tailList = new List<Transform>();
    private Vector2 _moveDir;
    private ScreenBound _screenBound;
    
    private void Start()
    {
        _moveDir = Vector2.right;
        tailList.Add(transform);
        StartCoroutine(MoveRoutine());
        _screenBound = new ScreenBound(Camera.main);
    }
    private void Update()
    {
        Movement();
        EdgeTeleport();
    }
    private void EdgeTeleport()
    {
        if (transform.position.x > _screenBound.bounds.x)
        {
            SetPositionX(-_screenBound.bounds.x);
        }

        if (transform.position.x < -_screenBound.bounds.x)
        {
            SetPositionX(_screenBound.bounds.x);
        }

        if (transform.position.y > _screenBound.bounds.y)
        {
            SetPositionY(-_screenBound.bounds.y);
        }

        if (transform.position.y < -_screenBound.bounds.y)
        {
            SetPositionY(_screenBound.bounds.y);
        }
    }
    private void SetPositionX(float x)
    {
        var position = transform.position;
        position = new Vector3(x, position.y, position.z);
        transform.position = position;
    }
    private void SetPositionY(float y)
    {
        var position = transform.position;
        position = new Vector3(position.x, y, position.z);
        transform.position = position;
    }
    private void Movement()
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
        var tail =  Instantiate(tailPrefab);
        tailList.Add(tail.transform);
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
