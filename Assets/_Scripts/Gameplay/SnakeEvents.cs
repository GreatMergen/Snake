using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class SnakeEvents : MonoBehaviour
{
    [SerializeField] private GameObject tail;

    public List<GameObject> tailList;

    public void AteApple()
    {
        
      var _tail =  Instantiate(tail, TailPos(), quaternion.identity);
      tailList.Add(_tail);
      
    }

    private Vector2 TailPos()
    {
        if (tailList.Count == 0)
        {
            return new Vector3(transform.position.x - 1.1f, transform.position.y, transform.position.z);
        }
        else
        {
            Vector3 lastTail = tailList[^1].transform.position;
            return new Vector3(lastTail.x - 1.1f, lastTail.y, lastTail.z);
        }
    }
}
