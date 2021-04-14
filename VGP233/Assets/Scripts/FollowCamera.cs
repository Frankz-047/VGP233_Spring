using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject player;
    private Vector3 followOffset = new Vector3(0.0f, 2.0f, -3.0f);

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + followOffset;
    }
}
