using System;
using System.Collections;
using UnityEngine;

public class Tail : MonoBehaviour 
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
