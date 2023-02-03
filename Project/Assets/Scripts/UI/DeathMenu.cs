using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private float UIOffset;
    [SerializeField] private TMP_Text scoreText;

    private float currentDisplayedScore;
    private bool setPosition = false;
    private bool scoreSet = false;

    private WaveSystem waveSystem;
    private EnemySpawner enemySpawner;
    private MainMenu mainMenu;

    void Start()
    {
        deathMenu.SetActive(false);
        waveSystem = GameManager.Instance.GetComponent<WaveSystem>();
        enemySpawner = GameManager.Instance.GetComponent<EnemySpawner>();
        mainMenu = GetComponent<MainMenu>();
    }

    void Update()
    {
        if (Player.PlayerInstance.playerDied)
        {
            deathMenu.SetActive(true);
            if (!setPosition)
            {
                SetDeathMenuPosition();
                setPosition = true;
            }

            if (!scoreSet)
            {
                currentDisplayedScore = Player.PlayerInstance.score;
                scoreSet = true;
            }

            if (currentDisplayedScore > 0)
            {
                currentDisplayedScore -= 10 * Time.deltaTime;
                scoreText.text = "Score: " + Mathf.RoundToInt(currentDisplayedScore).ToString();
            }
            else if (currentDisplayedScore == 0)
            {
                scoreText.text = "Score: 0";
            }

            Player.PlayerInstance.PlayerDied();
        }
    }

    public void PlayAgainButton()
    {
        AudioManager.instance.Play("SelectUI");

        deathMenu.SetActive(false);
        waveSystem.ResetGame();
        Player.PlayerInstance.ResetGame();
        enemySpawner.needToSpawn = true;
        scoreSet = false;
    }

    public void MainMenuButton()
    {
        AudioManager.instance.Play("SelectUI");

        //Player.PlayerInstance.SaveScore();
        mainMenu.Show();
        deathMenu.SetActive(false);
        waveSystem.ResetGame();
        Player.PlayerInstance.ResetGame();
        scoreSet = false;
    }

    private void SetDeathMenuPosition()
    {
        deathMenu.transform.position = Camera.main.transform.position + Camera.main.transform.forward * UIOffset;
        deathMenu.transform.rotation = Camera.main.transform.rotation;
    }
}
