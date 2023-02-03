using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private GameObject waveSurvivedMenu;
    [SerializeField] private GameObject canvas;
    [SerializeField] private int pointsPerEnemy;

    private EnemySpawner enemySpawner;
    private WaveWonMenu waveWonMenu;

    public int waveNumber = 1;
    public int enemiesToSpawn = 5;
    public bool firstEnemySpawned = false;

    public int enemiesInScene = 0;

    [SerializeField] private float timeToCompleteWave;
    private bool isWaveTimed = false;
    public bool isWaveActive = false;

    private bool setTime = false;

    private GameObject[] arrows;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = GetComponent<EnemySpawner>();
        waveWonMenu = canvas.GetComponent<WaveWonMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        enemiesInScene = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemySpawner.enemiesSpawned == enemiesToSpawn)
        {
            enemySpawner.needToSpawn = false;
        }

        if (waveNumber % 3 == 0 && !setTime)
        {
            isWaveTimed = true;
            timeToCompleteWave += enemiesToSpawn / 2;
            setTime = true;
        }

        if (isWaveTimed && isWaveActive)
        {            
            timeToCompleteWave -= Time.deltaTime;
        }

        if (enemiesInScene == 0 && firstEnemySpawned && !waveWonMenu.waveWonMenu.activeInHierarchy && !Player.PlayerInstance.playerDied && !GameManager.Instance.mainMenuButtonPressed)
        {
            AudioManager.instance.Play("WaveWon");
            Debug.Log("wave ended");
            Player.PlayerInstance.scoreWave += enemiesToSpawn * pointsPerEnemy;
            Player.PlayerInstance.scoreHealth += Player.PlayerInstance.health;
            Player.PlayerInstance.score += Player.PlayerInstance.scoreWave + Player.PlayerInstance.scoreHealth;
            waveWonMenu.Show();

            arrows = GameObject.FindGameObjectsWithTag("Arrow");
            foreach (GameObject item in arrows)
            {
                Destroy(item);
            }

            isWaveActive = false;
            isWaveTimed = false;
            setTime = false;
        }
        else if (enemiesInScene > 0 && !firstEnemySpawned)
        {
            firstEnemySpawned = true;
        }
    }

    public void StartNextWave()
    {
        waveNumber++;
        enemiesToSpawn += 2;
        firstEnemySpawned = false;

        enemySpawner.enemiesSpawned = 0;
        enemySpawner.needToSpawn = true;
        enemySpawner.timeToSpawn = 0;

        isWaveActive = true;
    }

    public void ResetGame()
    {
        waveNumber = 1;
        enemiesToSpawn = 5;
        firstEnemySpawned = false;

        enemySpawner.enemiesSpawned = 0;
        enemySpawner.timeToSpawn = 0;

        isWaveActive = false;

        arrows = GameObject.FindGameObjectsWithTag("Arrow");
        foreach (GameObject item in arrows)
        {
            Destroy(item);
        }
    }
}
