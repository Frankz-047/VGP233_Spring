using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float CarSpeed = 10f;
    public Camera followCamera;
    //private Vector3 CarMove = new Vector3();
    //private Vector3 CamMove = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        //CarMove.z = CarSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //vehicle movement
        transform.Translate(Vector3.forward * Time.deltaTime * CarSpeed);
        //followCamera.transform.z
    }
}
