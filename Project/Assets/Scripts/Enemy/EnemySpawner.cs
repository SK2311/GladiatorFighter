using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int timeBetweenSpawning;

    public bool needToSpawn;
    public int enemiesSpawned;
    public float timeToSpawn;

    private GameObject enemyPrefab;
    private Enemy enemy;

    void Update()
    {
        if (needToSpawn)
        {
            //spawn enemy
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        timeToSpawn -= Time.deltaTime;
        //check if the time to spawn is 0
        if (timeToSpawn <= 0f)
        {
            //instantiate an enemy prefab and rotate it towards the player
            enemyPrefab = Instantiate(enemyPrefabs[GetRandomEnemyPrefab()], spawnPoints[GetRandomSpawnPoint()].transform.position, Quaternion.identity);
            enemyPrefab.transform.LookAt(Player.PlayerInstance.transform);
            
            enemy = enemyPrefab.GetComponent<Enemy>();
            enemy.SetEnemyType();

            //reset the timer
            timeToSpawn = timeBetweenSpawning;

            enemiesSpawned++;
        }
    }

    private int GetRandomSpawnPoint()
    {
        //get random spawn point
        var spawnPoint = Random.Range(0, spawnPoints.Length);
        return spawnPoint;
    }

    private int GetRandomEnemyPrefab()
    {
        //get random enemy
        var enemyPrefab = Random.Range(0, enemyPrefabs.Length);
        return enemyPrefab;
    }
}
