using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WaveWonMenu : MonoBehaviour
{
    public GameObject waveWonMenu;
    [SerializeField] private float UIOffset;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text pointsFromWave;
    [SerializeField] private TMP_Text pointsFromHealth;
    [SerializeField] private GameObject waveWonItems;
    [SerializeField] private GameObject armouryMenu;

    private WaveSystem waveSystem;
    private EnemySpawner enemySpawner;
    private MainMenu mainMenu;
    private float currentDisplayedScore;

    // Start is called before the first frame update
    void Start()
    {
        waveWonMenu.SetActive(false);

        waveSystem = GameManager.Instance.GetComponent<WaveSystem>();
        enemySpawner = GameManager.Instance.GetComponent<EnemySpawner>();

        mainMenu = GetComponent<MainMenu>();

        currentDisplayedScore = Player.PlayerInstance.score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        waveWonMenu.SetActive(true);
        SetWaveWonMenuPosition();
        SetScoreParts();
        StartCoroutine(CountUpScore());
    }

    private void SetWaveWonMenuPosition()
    {
        waveWonMenu.transform.position = Camera.main.transform.position + Camera.main.transform.forward * UIOffset;
        waveWonMenu.transform.rotation = Camera.main.transform.rotation;
    }

    public void NextWave()
    {
        AudioManager.instance.Play("SelectUI");
        waveSystem.StartNextWave();
        waveWonMenu.SetActive(false);

        Player.PlayerInstance.scoreWave = 0;
        Player.PlayerInstance.scoreHealth = 0;
    }

    public void ArmouryButton()
    {
        AudioManager.instance.Play("SelectUI");
        armouryMenu.SetActive(true);
        waveWonItems.SetActive(false);
    }

    public void LoadMenu()
    {
        AudioManager.instance.Play("SelectUI");
        Time.timeScale = 1f;

        GameManager.Instance.mainMenuButtonPressed = true;
        enemySpawner.needToSpawn = false;

        waveWonMenu.SetActive(false);
        mainMenu.Show();

        waveSystem.ResetGame();
        Player.PlayerInstance.ResetGame();

        Player.PlayerInstance.scoreWave = 0;
        Player.PlayerInstance.scoreHealth = 0;
    }

    IEnumerator CountUpScore()
    {
        while (currentDisplayedScore < Player.PlayerInstance.score)
        {
            currentDisplayedScore += 10 * Time.deltaTime;
            currentDisplayedScore = Mathf.Clamp(currentDisplayedScore, 0f, Player.PlayerInstance.score);
            scoreText.text = "Score: " + Mathf.RoundToInt(currentDisplayedScore).ToString();
            yield return null;
        }
    }

    private void SetScoreParts()
    {
        pointsFromWave.text = $"Points from wave: {Player.PlayerInstance.scoreWave}";
        pointsFromHealth.text = $"Points from health: {Player.PlayerInstance.scoreHealth}";
    }
}
