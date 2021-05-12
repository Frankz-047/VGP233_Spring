using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject playerObject;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerObject = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }
        Vector3 direction = (playerObject.transform.position - gameObject.transform.position).normalized;
        enemyRb.AddForce(direction * speed);    
    }


}
