using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float temp;

    private Vector3 offset = new Vector3(0.0f, 15.0f, -2.0f);
    private Rigidbody playerRb;
    private Animator playerAnim;
    private Camera mainCam;
    private GameObject crossHair;
    

    public int Ammo;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        float hitDist = 0.0f;
        if (playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion playerRotation = Quaternion.LookRotation(targetPoint - transform.position);
            playerRotation.x = 0;
            playerRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, 7.0f * Time.deltaTime);
        }

        if (Input.GetMouseButton(1))
        {
            playerAnim.SetInteger("WeaponType_int", 1);
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(shoot());
            }
        }
        else
        {
            playerAnim.SetInteger("WeaponType_int", 0);
        }
        
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * moveSpeed , Space.World);
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * moveSpeed, Space.World);

        mainCam.transform.position = transform.position + offset;
        temp = playerRb.velocity.magnitude;
        if (verticalInput != 0)
        {
            playerAnim.SetFloat("Speed_f", Mathf.Abs(verticalInput));
        }
        else
        {
            playerAnim.SetFloat("Speed_f", Mathf.Abs(horizontalInput));
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ammo"))
        {
            ++Ammo;
            Destroy(other.gameObject);
        }
    }

    IEnumerator shoot()
    {
        playerAnim.SetBool("Shoot_b", true);
        yield return new WaitForSeconds(0.3f);
        playerAnim.SetBool("Shoot_b", false);
    }

    
}
