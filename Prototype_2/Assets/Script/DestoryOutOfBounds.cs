using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryOutOfBounds : MonoBehaviour
{
    private float topBound = 30.0f;
    private float bottomBoundary = -10.0f;
    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < bottomBoundary || transform.position.z > topBound)
        {
            Destroy(gameObject);
        }
    }
}
