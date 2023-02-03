using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Player : MonoBehaviour
{
    public static Player PlayerInstance { get; private set; }

    [SerializeField] private SnapTurnProviderBase snapTurn;

    public int health = 10;
    public int score = 0;
    public int scoreWave = 0;
    public int scoreHealth = 0;
    public bool playerDied = false;
    public bool playerHit = false;
    public Transform target;
    public GameObject quiver;

    public GameObject[] enemies;

    private void Awake()
    {
        //Player singleton
        if (PlayerInstance == null)
        {
            PlayerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        score = GetScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GameIsPaused)
        {
            snapTurn.enabled = false;
        }
        else
        {
            snapTurn.enabled = true;
        }

        if (health <= 0)
        {
            playerDied = true;
        }
    }

    public void SaveScore() //use to save the score between sessions
    {
        PlayerPrefs.SetInt("Score", score);
    }

    public int GetScore() //use to get the score
    {
        return PlayerPrefs.GetInt("Score");
    }

    public void PlayerDied()
    {
        score = 0;
        RemoveEnemies(5f);
    }

    public void RemoveEnemies(float timeToDestroy)
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy, timeToDestroy);
        }
    }

    public void ResetGame()
    {
        health = 20;
        playerDied = false;
    }
}
