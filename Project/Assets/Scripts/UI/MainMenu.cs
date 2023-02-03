using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private float UIOffset;

    private EnemySpawner enemySpawner;
    private WaveSystem waveSystem;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        StartCoroutine(Wait());

        enemySpawner = GameManager.Instance.gameObject.GetComponent<EnemySpawner>();
        enemySpawner.needToSpawn = false;

        waveSystem = GameManager.Instance.gameObject.GetComponent<WaveSystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartButton();
        }
    }

    public void StartButton()
    {
        AudioManager.instance.Play("SelectUI");
        enemySpawner.needToSpawn = true;
        mainMenu.SetActive(false);
        GameManager.Instance.mainMenuButtonPressed = false;
        waveSystem.isWaveActive = true;
    }

    public void QuitGameButton()
    {
        AudioManager.instance.Play("SelectUI");
        Application.Quit();
    }

    IEnumerator Wait()
    {
        yield return new WaitForEndOfFrame();
        SetMainMenuPosition();
    }

    private void SetMainMenuPosition()
    {
        mainMenu.transform.position = Camera.main.transform.position + Camera.main.transform.forward * UIOffset;
        mainMenu.transform.rotation = Camera.main.transform.rotation;
    }

    public void Show()
    {
        mainMenu.SetActive(true);
        SetMainMenuPosition();
    }
}
