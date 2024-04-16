using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{


    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups;//list of a group of enemies to spawn in
        public int waveQuota; //total number of enemies to spawn
        public float spawnInterval;//interval at which to spawn enemies
        public int spawnCount;//the number of enemies already spawned in this wave
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount;//number of enemies to spawn in this wave
        public int spawnCount;//the number of enemies already spawned in this wave
        public GameObject enemyPrefab;
    }

    public List<Wave> waves; //a list of all the waves in the game
    public int currentWaveCount; //the index of the current wave (Starts at 0)
    Transform player;
    [Header("Spawner Attributes")]
    float spawnTimer; //timer used to determine when to spawn the next enemy
    public float waveInterval;//interval between each wave
    public int enemiesAlive;
    public int maxEnemiesAllowed; //the maximum number of enemies allowed on the map at once
    public bool maxEnemiesReached; //a flag indicating ic the maximum number of enemies has been reached
    public bool wavePreparing;

    [Header("Spawn Positions")]
    public List<Transform> relativeSpawnPoints; //a list to store all the relative spawn points of enemies
    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        CalculateWaveQuota();
    }

    // Update is called once per frame
    void Update()
    {

        if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount >= waves[currentWaveCount].waveQuota)//check if the wave has ended and the next wave should start
        {
            if (!wavePreparing)
            {
                StartCoroutine(BeginNextWave());
            }
        }
        spawnTimer += Time.deltaTime;

        //continually spawn enemies until the wave quota has been met
        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }

    }

    IEnumerator BeginNextWave()
    {
        wavePreparing = true;
        //wit for waveinterval seconds before starting the next wave
        yield return new WaitForSeconds(waveInterval);
        if (currentWaveCount < waves.Count - 1)//checks if there are any waves left to start after the current one
        {
            currentWaveCount++;
            CalculateWaveQuota();
            wavePreparing = false;
        }
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }
        waves[currentWaveCount].waveQuota = currentWaveQuota;
    }

    void SpawnEnemies()
    {
            //spawn each type of enemy until the quota is filled
            foreach(var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                 if (enemiesAlive >= maxEnemiesAllowed)
                  {
                    maxEnemiesReached = true;
                    return;
                  }

                while (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota)
                {
                    Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);
                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                }
                Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);

                enemyGroup.spawnCount++;
                waves[currentWaveCount].spawnCount++;
                enemiesAlive++;
        }
        //reset the maxenemiesreach flag if the number of enemies alive has dropped below the maximum amount
        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }

    //call this when an enemy is killed
    public void OnEnemyKilled()
    {
        enemiesAlive--;
    }
}
