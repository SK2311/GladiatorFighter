using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool mainMenuButtonPressed = false;
    public bool menuActive;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject waveWonMenu;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Update()
    {
        if (mainMenu.activeInHierarchy || pauseMenu.activeInHierarchy || deathMenu.activeInHierarchy || waveWonMenu.activeInHierarchy)
        {
            menuActive = true;
        }
        else
        {
            menuActive = false;
        }
    }
}
