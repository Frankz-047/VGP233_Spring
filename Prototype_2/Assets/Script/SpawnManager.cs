using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> animalPrefabs;
    private float spawnPosZ = -9.0f, xRange = 19.0f;
    private float startDelay = 2.0f;
    private float spawnInterval = 1.5f;
    //public int animalIndex;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
    }


    private void SpawnRandomAnimal()
    {
        int animalIndex = Random.Range(0, 3);
        Vector3 spawnPos = new Vector3(Random.Range(-xRange, xRange), 0.0f, spawnPosZ);
        Instantiate(animalPrefabs[animalIndex], spawnPos, transform.rotation);
    }
}
