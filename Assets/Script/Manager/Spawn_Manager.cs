using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefaps;
    [SerializeField] private float spawnRange;

    [SerializeField] private GameObject[] powerupPrefaps;

    private int waveNumber = 0  ;
    private int enemyMaxLevel;
    private int enemySpawnAmount = 1;
    private UI_Manager uI_Manager;
    private Scene_Manager scene_Manager;

    void Start()
    {
        uI_Manager = FindObjectOfType<UI_Manager>();
        scene_Manager = FindObjectOfType<Scene_Manager>();
        StartCoroutine(SpawningPowerupCo());
    }

    void Spawn(GameObject gameObjectToSpawn)
    {
        GameObject spawned = (GameObject) Instantiate(gameObjectToSpawn, SpawnPos(), Quaternion.identity);
    }

    Vector3 SpawnPos()
    {
        float spawnX = Random.Range(-spawnRange, spawnRange);
        float spawnZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPos = new Vector3(spawnX, 0, spawnZ);
        return spawnPos;
    }

    void SpawnEnemyWave()
    {
        waveNumber ++;
        uI_Manager.UpdateWaveText(waveNumber);
        if(enemySpawnAmount >= 5)
        {
            if(enemyMaxLevel < enemyPrefaps.Length)
            {
                enemyMaxLevel ++;
            }
            enemySpawnAmount = 0;
        }

        for(int i = 0; i < enemySpawnAmount; i++)
        {
            int randomEnemyLevel;
            randomEnemyLevel = Random.Range(0, enemyMaxLevel + 1);
            Spawn(enemyPrefaps[randomEnemyLevel]);
        }
    }

    void Update()
    {
        if(scene_Manager.IsEndGame())
        {
            return;
        }
        SpawningEnemy();
    }

    void SpawningEnemy()
    {
        int enemyCount;
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if(enemyCount <= 0)
        {
            SpawnEnemyWave();
            enemySpawnAmount++;
        }
    }

    public IEnumerator SpawningPowerupCo()
    {
        int randomTime;
        randomTime = Random.Range(2,5);

        yield return new WaitForSeconds(randomTime);

        SpawnRandomPowerup();
    }

    void SpawnRandomPowerup()
    {
        int randomIndex;
        randomIndex = Random.Range(0,powerupPrefaps.Length);

        Spawn(powerupPrefaps[randomIndex]);
    }
}
