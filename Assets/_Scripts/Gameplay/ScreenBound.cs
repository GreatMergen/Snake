using UnityEngine;

public class ScreenBound
{
    public Vector2 bounds;
    public ScreenBound(Camera cam)
    {
        bounds = cam.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,cam.transform.position.z));
    }
}
