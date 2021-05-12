using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private bool isPowered;
    private float powerUpTimer;
    public float powerUpStrenght = 15.0f;
    public float speed = 5.0f;
    public GameObject powerUpIndicator;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    private void FixedUpdate()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        if (Input.GetKey(KeyCode.Space))
        {
            playerRb.velocity = Vector3.zero;
        }
    }
    // Update is called once per frame
    void Update()
    {
        powerUpIndicator.transform.position = gameObject.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            isPowered = true;
            powerUpTimer = 5.0f;
            powerUpIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountDownRoutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && isPowered)
        {
            GameObject enemyObject = collision.gameObject;
            Rigidbody enemyRb = enemyObject.GetComponent<Rigidbody>();
            Vector3 direction = (enemyObject.transform.position - gameObject.transform.position).normalized;
            enemyRb.AddForce(direction * powerUpStrenght, ForceMode.Impulse);
        }
    }

    IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(powerUpTimer);
        Debug.Log("You are Powerless Now");
        powerUpIndicator.SetActive(false);
        isPowered = false;
    }
}
