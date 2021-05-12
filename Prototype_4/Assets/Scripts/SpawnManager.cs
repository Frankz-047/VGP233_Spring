using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    private float spawnRange = 5.0f;
    private int waveNumber = 1;
    public int enemyCount;

    // Start is called before the first frame update
    void Start()
    {

        SpawnEnemyWave(1);
        Instantiate(powerUpPrefab, GenerateSpawnPos(), powerUpPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            ++waveNumber;
            SpawnEnemyWave(waveNumber);
        }
    }

    private Vector3 GenerateSpawnPos()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        float spawnPosY = 2.0f;
        return new Vector3(spawnPosX, spawnPosY, spawnPosZ);
    }

    private void SpawnEnemyWave(int numberOfEnemies = 3)
    {
        for (int i = 0; i < numberOfEnemies; ++i)
        {
            Instantiate(enemyPrefab, GenerateSpawnPos(), enemyPrefab.transform.rotation);
        }
        Instantiate(powerUpPrefab, GenerateSpawnPos(), powerUpPrefab.transform.rotation);
    }
}
