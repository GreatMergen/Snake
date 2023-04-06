
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SnakeController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject tailPrefab;
    public List<Transform> tailList = new List<Transform>();
    public Vector2 direction = Vector2.right;
    private Vector2 input;
    private ScreenBound _screenBound;
    private void OnEnable()
    { 
        Events.OnFoodTake.AddListener(Grow);
        Events.OnGameStart.AddListener(EnableSnakeController);
    }
    private void OnDisable()
    { 
        Events.OnFoodTake.RemoveListener(Grow);
        Events.OnGameStart.RemoveListener(EnableSnakeController);
    }
    private void Start()
    {
        direction = Vector2.zero;
        tailList.Add(transform);
        _screenBound = new ScreenBound(Camera.main);
    }
    private void Update()
    {
        Movement();
        EdgeTeleport();
    }
    private void FixedUpdate()
    {
        if (input != Vector2.zero) {
            direction = input;
        }
        for (int i = tailList.Count - 1; i > 0; i--) {
            tailList[i].position = tailList[i - 1].position;
        }
        
        float x = Mathf.Round(transform.position.x) + direction.x;
        float y = Mathf.Round(transform.position.y) + direction.y;
        transform.position = new Vector2(x, y);
    }
    private void EnableSnakeController()
    {
        input =Vector2.right;
    }
    private void EdgeTeleport()
    {
        if (transform.position.x > _screenBound.bounds.x) SetPositionX(-_screenBound.bounds.x);
        if (transform.position.x < -_screenBound.bounds.x) SetPositionX(_screenBound.bounds.x);
        if (transform.position.y > _screenBound.bounds.y) SetPositionY(-_screenBound.bounds.y);
        if (transform.position.y < -_screenBound.bounds.y) SetPositionY(_screenBound.bounds.y);
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
        if (direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                input = Vector2.up;
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                input = Vector2.down;
            }
        }
        else if (direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                input = Vector2.right;
            } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                input = Vector2.left;
            }
        }
    }
    private void Grow()
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
