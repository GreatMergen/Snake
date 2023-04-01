using Unity.Mathematics;
using UnityEngine;


public class SnakeEvents : MonoBehaviour
{
    [SerializeField] private GameObject tail;
    
    public void AteApple()
    {
        Instantiate(tail, transform.position - transform.forward * -5, quaternion.identity);
    }
}
