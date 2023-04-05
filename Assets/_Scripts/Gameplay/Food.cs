using System;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class Food : MonoBehaviour,ICollideble
{ 
        [SerializeField] private GameObject appleTakeEffect;
        public Vector2 camBorder;
      
        private void ChangeFoodPosition(SnakeController snakeController)
        {
                var nextFoodPos = NextFoodPos();
        
                for (int i = 0; i < snakeController.tailList.Count; i++)
                {
                        if (snakeController.tailList[i].position == nextFoodPos)
                        {
                                ChangeFoodPosition(snakeController);
                                print("Elma kuyrğunujn dışında bir yere spawn oldu");
                                return;
                        }
            
                        transform.position = nextFoodPos;
                }
        }
        private Vector3 NextFoodPos()
        {
                var x = (int) Random.Range(-camBorder.x, camBorder.x);
                var y = (int) Random.Range(-camBorder.y, camBorder.y);
                var nextFoodPos = new Vector3(x, y, 0);
                return nextFoodPos;
        }
        public void Collide(SnakeController snakeController)
        {
                Events.OnFoodTake.Invoke();
                Camera.main.transform.DOShakePosition(1, .05f);
                Instantiate(appleTakeEffect, transform.position,Quaternion.identity);
                ChangeFoodPosition(snakeController);
        }
}
