using UnityEngine;

public class Border : MonoBehaviour,ICollideble
{
    [SerializeField] private bool isUpBorder;

    private float _targetValue;
    private Vector3 _targetPos;

    public void Collide(SnakeController snakeController)
    {
        if (isUpBorder)
        {
           _targetValue = -transform.position.y;
           print(_targetValue);
           var position = snakeController.transform.position;
           _targetPos = new Vector3(position.x, _targetValue, position.z);
           snakeController.transform.position = _targetPos;
        }
        else
        {
            _targetValue = -transform.position.x;
            var position = snakeController.transform.position;
            _targetPos = new Vector3(_targetValue, position.y, position.z);
            snakeController.transform.position = _targetPos;
        }
           
    }
}
