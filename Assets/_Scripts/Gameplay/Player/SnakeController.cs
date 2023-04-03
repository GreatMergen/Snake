using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject tail,food;
    public List<Transform> tailList = new List<Transform>();
    public Vector2 camBorder;
    private Vector2 _moveDir;
    [SerializeField] private GameObject appleTakeEffect;

    private void Start()
    {
        _moveDir = Vector2.right;
        tailList.Add(transform);
        ChangeFoodPosition();
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
        
        if(Vector2.Dot(_moveDir,tempMoveDir) > -0.9f && tempMoveDir != Vector2.zero)
        {
            _moveDir = tempMoveDir;
        }
        
        /*
        if (Input.GetKeyDown(KeyCode.W) || (Input.GetKeyDown(KeyCode.UpArrow)) && _moveDir != Vector2.down)
        {
            _moveDir = Vector2.up;
        }
        
        if (Input.GetKeyDown(KeyCode.S) || (Input.GetKeyDown(KeyCode.DownArrow)) && _moveDir != Vector2.up)
        {
            _moveDir = Vector2.down;
        }
        
        if (Input.GetKeyDown(KeyCode.D) || (Input.GetKeyDown(KeyCode.RightArrow)) && _moveDir != Vector2.left)
        {
            _moveDir = Vector2.right;
        }
        
        if (Input.GetKeyDown(KeyCode.A) || (Input.GetKeyDown(KeyCode.LeftArrow)) && _moveDir != Vector2.right)
        {
            _moveDir = Vector2.left;
        }
        
         */
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
        StartCoroutine(EnableTailCollider(_tail));  
        tailList.Add(_tail.transform);
        tailList[^1].position = tailList[^2].position;
    }

    private IEnumerator  EnableTailCollider(GameObject _tail)
    {
        yield return new WaitForSeconds(1f);
        _tail.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void ChangeFoodPosition()
    {
        var nextFoodPos = NextFoodPos();
        
        for (int i = 0; i < tailList.Count; i++)
        {
            if (tailList[i].position == nextFoodPos)
            {
              ChangeFoodPosition();
              print("Elma kuyrğunujn dışında bir yere spawn oldu");
              return;
            }
            
            food.transform.position = nextFoodPos;
        }
    }

    private Vector3 NextFoodPos()
    {
        var x = (int) Random.Range(-camBorder.x, camBorder.x);
        var y = (int) Random.Range(-camBorder.y, camBorder.y);
        var nextFoodPos = new Vector3(x, y, 0);
        return nextFoodPos;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Food"))
        {
            Camera.main.transform.DOShakePosition(1, .05f);
            Instantiate(appleTakeEffect, transform.position,Quaternion.identity);
            LevelManager.Instance.AddScore(1);
            Grow();
            ChangeFoodPosition();
        }

        if (col.CompareTag("Tail"))
        {
            LevelManager.Instance.Dead();
            print("s");
        }
        if (col.CompareTag("Wall"))
        {
            LevelManager.Instance.Dead();
        }

      
    }
}
