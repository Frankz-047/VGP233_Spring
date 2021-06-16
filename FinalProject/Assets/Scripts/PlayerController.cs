using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float AimMoveSpeed;
    public float currentMoveSpeed;
    public float temp;
    public GameObject bulletSpawnPoint;
    public GameObject bulletPrefab;
    public TextMeshProUGUI HUD;
    public GameManager gm;
    public float timeSurivived;

    public int hp = 10;
    private bool isAlive = true;
    private Vector3 offset = new Vector3(0.0f, 15.0f, -2.0f);
    private Rigidbody playerRb;
    private Animator playerAnim;
    private Camera mainCam;
    private GameObject crossHair;
    private AudioSource FootStep;

    //private float fireRate = 0.5f;
    //private float fireRateDetect = 0.5f;
    private bool fireReady = true;
    private bool loadReady = true;

    public int Ammo;
    public int maxAmmo = 12;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        FootStep = GetComponent<AudioSource>();
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive && gm.isGameActive)
        {
            AimMoveSpeed = moveSpeed * 0.8f;
            currentMoveSpeed = moveSpeed;
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
                currentMoveSpeed = AimMoveSpeed;
                if (Input.GetMouseButtonDown(0) && fireReady)
                {
                    fireReady = false;
                    StartCoroutine(shoot());
                    UpdateHUD();
                }
                if (Input.GetKeyDown(KeyCode.R) && loadReady)
                {
                    loadReady = false;
                    fireReady = false;
                    StartCoroutine(Reload());
                    UpdateHUD();
                }
            }
            else
            {
                playerAnim.SetInteger("WeaponType_int", 0);
            }

            transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * currentMoveSpeed, Space.World);
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * currentMoveSpeed, Space.World);
            if (transform.position.x >= 30.0f)
            {
                transform.position = new Vector3(30.0f, transform.position.y, transform.position.z);
            }
            if (transform.position.x <= -30.0f)
            {
                transform.position = new Vector3(-30.0f, transform.position.y, transform.position.z);
            }
            if (transform.position.z <= -31.0f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -31.0f);
            }
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
            UpdateHUD();
            timeSurivived += Time.deltaTime;
        }
    }

    private void UpdateHUD()
    {
        HUD.text = "HP: " + hp + "/20\n" +
            "Ammo: " + Ammo + "/6\n"
            + "Total Ammo: " + maxAmmo
            +"\n"+ Mathf.Round(timeSurivived);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ammo"))
        {
            maxAmmo += Random.Range(1,7);
            Destroy(other.gameObject);
        }
    }

    IEnumerator shoot()
    {
        if (Ammo >= 1)
        {
            playerAnim.SetBool("Shoot_b", true);
            yield return new WaitForSeconds(0.3f);
            Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, transform.rotation);
            AudioManager.instance.Play("Shoot");
            --Ammo;
            fireReady = true;
            playerAnim.SetBool("Shoot_b", false);
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
            AudioManager.instance.Play("Click");
            fireReady = true;
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        AudioManager.instance.Play("playerHurt");
        if( hp <= 0)
        {
            Die();
            GameManager.instance.GameOver();
        }
        UpdateHUD();
    }

    public bool isPlayerLive()
    {
        return isAlive;
    }

    private void Die()
    {
        playerAnim.SetBool("Death_b", true);
        PlayerPrefs.SetInt("Score", (int)Mathf.Round(timeSurivived));
        playerRb.freezeRotation = true;
        playerRb.isKinematic = true;
        isAlive = false;
    }

    public int GetHp()
    {
        return hp;
    }

    IEnumerator Reload()
    {
        playerAnim.SetBool("Reload_b", true);
        yield return new WaitForSeconds(1.0f);
        int needToLoad = 6 - Ammo;
        if (maxAmmo <= 0)
        {
            AudioManager.instance.Play("Click");
        }
        else if (maxAmmo >= needToLoad)
        {
            Ammo += needToLoad;
            maxAmmo -= needToLoad;
            AudioManager.instance.Play("Reload");
        }
        else
        {
            Ammo += maxAmmo;
            maxAmmo = 0;
            AudioManager.instance.Play("Reload");
        }
        fireReady = true;
        loadReady = true;
        playerAnim.SetBool("Reload_b", false);
    }

    public void StartGame(int difficulty)
    {
        isAlive = true;
        hp = 20 / difficulty;
        maxAmmo = 12 / difficulty;
        Ammo = 6;
    }
}
