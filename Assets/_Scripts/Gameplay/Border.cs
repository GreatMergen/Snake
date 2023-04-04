using UnityEngine;

public class Border : MonoBehaviour,ICollideble
{
    [SerializeField] private bool isUpBorder;

    public void Collide(SnakeController snakeController)
    {
        if (isUpBorder)
        {
            
        }
           
    }
}
