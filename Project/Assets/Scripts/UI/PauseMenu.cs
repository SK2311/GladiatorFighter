using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused;

    [SerializeField] private XRController leftHandController;
    [SerializeField] private XRController rightHandController;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject mainMenuObject;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject waveWonMenu;
    [SerializeField] private float UIOffset;

    private bool pauseMenuActive;
    private bool pauseButtonPressed;
    private bool otherMenuActive;
    private WaveSystem waveSystem;
    private EnemySpawner enemySpawner;
    private MainMenu mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);

        pauseMenuActive = false;
        pauseButtonPressed = false;
        GameIsPaused = false;

        waveSystem = GameManager.Instance.GetComponent<WaveSystem>();
        enemySpawner = GameManager.Instance.GetComponent<EnemySpawner>();

        mainMenu = GetComponent<MainMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mainMenuObject.activeInHierarchy || deathMenu.activeInHierarchy || waveWonMenu.activeInHierarchy)
        {
            otherMenuActive = true;
        }
        else
        {
            otherMenuActive = false;
        }

        if (!otherMenuActive)
        {
            PauseMenuToggle();
        }
    }

    private void PauseMenuToggle()
    {
        leftHandController.inputDevice.TryGetFeatureValue(CommonUsages.menuButton, out bool pressed);
        pauseMenuActive = pauseMenu.activeInHierarchy;

        if (pressed != pauseButtonPressed)
        {
            if (pressed)//onButtonDown, else for onButtonUp
            {
                if (!pauseMenuActive)
                {
                    Pause();
                }
                else
                {
                    Resume();
                }
            }
            pauseButtonPressed = pressed;
        }
    }

    private void SetPauseMenuPosition()
    {
        pauseMenu.transform.position = Camera.main.transform.position + Camera.main.transform.forward * UIOffset;
        pauseMenu.transform.rotation = Camera.main.transform.rotation;
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        SetPauseMenuPosition();
    }

    public void Resume()
    {
        AudioManager.instance.Play("SelectUI");
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void LoadMenu()
    {
        GameManager.Instance.mainMenuButtonPressed = true;
        enemySpawner.needToSpawn = false;
        Resume();

        //Player.PlayerInstance.SaveScore();

        mainMenu.Show();
        waveSystem.ResetGame();        

        Player.PlayerInstance.RemoveEnemies(0f);
        Player.PlayerInstance.ResetGame();
    }

    public void QuitGame()
    {
        AudioManager.instance.Play("SelectUI");
        //Player.PlayerInstance.SaveScore();
        Application.Quit();
    }
}
