using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject zombiePrefab;
    public GameObject ammoPackPrefab;
    public int totalAmmpPack = 0;
    private float startDelay = 2.0f, repeatRate = 5.0f, repeatRateAmmo = 5.0f;
    public int difficulty;
    private PlayerController playerControllerScript;
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        repeatRate /= (float)startDelay;
        repeatRateAmmo *= difficulty;
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.isGameActive && !IsInvoking())
        {
            InvokeRepeating("SpawnZombie", startDelay, repeatRate);
            InvokeRepeating("SpawnAmmo", startDelay, repeatRateAmmo);
        }
    }

    private void SpawnAmmo()
    {
        if (playerControllerScript.isPlayerLive() && totalAmmpPack <= 10)
        {
            Instantiate(ammoPackPrefab, SpawmPos_Ammo(), ammoPackPrefab.transform.rotation);
            Debug.Log("1");
            ++totalAmmpPack;
        }
    }
    private void SpawnZombie()
    {
        if (playerControllerScript.isPlayerLive())
        {
            Debug.Log("2");
            Instantiate(zombiePrefab, SpawmPos(), zombiePrefab.transform.rotation);
        }
    }

    private Vector3 SpawmPos()
    {
        return new Vector3(Random.Range(-30.0f, 30.0f), 0.5f, 240.0f);
    }

    private Vector3 SpawmPos_Ammo()
    {
        return new Vector3(Random.Range(-30.0f, 30.0f), 0.5f, Random.Range(0.0f, 240.0f));
    }
}
