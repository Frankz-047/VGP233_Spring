using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float CarSpeed = 10f;
    public float TurnSpeed;
    private float horizontalInput;
    private float forwardInput;

    // Update is called once per frame
    void Update()
    {
        //vehicle movement
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * CarSpeed * forwardInput);
        //transform.Translate(Vector3.right * Time.deltaTime * TurnSpeed * horizontalInput);
        if (forwardInput != 0)
        {
            transform.Rotate(Vector3.up, Time.deltaTime * TurnSpeed * horizontalInput);
        }
    }
}
