using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float topBound = 13f;
    private float lowerBound = -13f;

    void Update()
    {
        if (transform.position.y > topBound || transform.position.y < lowerBound)
        {
            Destroy(gameObject);
        }
    }
}
