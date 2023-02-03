using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private GameObject waveWonItems;
    [SerializeField] private GameObject armouryMenu;

    [SerializeField] private ShopItem sword2Object;
    [SerializeField] private ShopItem sword3Object;
    [SerializeField] private ShopItem sword4Object;

    [SerializeField] private GameObject sword2;
    [SerializeField] private GameObject sword3;
    [SerializeField] private GameObject sword4;

    [SerializeField] private GameObject lock2;
    [SerializeField] private GameObject lock3;
    [SerializeField] private GameObject lock4;

    [SerializeField] private Image background2;
    [SerializeField] private Image background3;
    [SerializeField] private Image background4;

    [SerializeField] private Color inactiveColor;

    [SerializeField] private TMP_Text scoretext;

    private void Start()
    {
        sword2.SetActive(PlayerPrefs.GetInt("Sword2Unlocked") == 1);
        sword3.SetActive(PlayerPrefs.GetInt("Sword3Unlocked") == 1);
        sword4.SetActive(PlayerPrefs.GetInt("Sword4Unlocked") == 1);

        lock2.SetActive(PlayerPrefs.GetInt("Sword2Unlocked") != 1);
        lock3.SetActive(PlayerPrefs.GetInt("Sword3Unlocked") != 1);
        lock4.SetActive(PlayerPrefs.GetInt("Sword4Unlocked") != 1);
    }

    private void Update()
    {
        if (armouryMenu.activeInHierarchy)
        {
            scoretext.text = Player.PlayerInstance.score.ToString();
        }

        if (Player.PlayerInstance.score >= sword2Object.itemCost)
        {
            lock2.SetActive(false);
        }

        if (Player.PlayerInstance.score >= sword3Object.itemCost)
        {
            lock3.SetActive(false);
        }

        if (Player.PlayerInstance.score >= sword4Object.itemCost)
        {
            lock4.SetActive(false);
        }

        if (PlayerPrefs.GetInt("Sword2Unlocked") == 1)
        {
            background2.color = inactiveColor;
        }

        if (PlayerPrefs.GetInt("Sword3Unlocked") == 1)
        {
            background3.color = inactiveColor;
        }

        if (PlayerPrefs.GetInt("Sword4Unlocked") == 1)
        {
            background4.color = inactiveColor;
        }
    }

    public void Sword2Button()
    {
        if (Player.PlayerInstance.score >= sword2Object.itemCost)
        {
            if (PlayerPrefs.GetInt("Sword2Unlocked") == 0)
            {
                AudioManager.instance.Play("SelectUI");
                sword2.SetActive(true);
                Player.PlayerInstance.score -= sword2Object.itemCost;
                PlayerPrefs.SetInt("Sword2Unlocked", 1);
            }
        }
        else
        {
            Debug.Log("Not enough points");
        }
    }

    public void Sword3Button()
    {
        if (Player.PlayerInstance.score >= sword3Object.itemCost)
        {
            if (PlayerPrefs.GetInt("Sword3Unlocked") == 0)
            {
                AudioManager.instance.Play("SelectUI");
                sword3.SetActive(true);
                Player.PlayerInstance.score -= sword3Object.itemCost;
                PlayerPrefs.SetInt("Sword3Unlocked", 1);
            }
        }
        else
        {
            Debug.Log("Not enough points");
        }
    }

    public void Sword4Button()
    {
        if (Player.PlayerInstance.score >= sword4Object.itemCost)
        {
            if (PlayerPrefs.GetInt("Sword4Unlocked") == 0)
            {
                AudioManager.instance.Play("SelectUI");
                sword4.SetActive(true);
                Player.PlayerInstance.score -= sword4Object.itemCost;
                PlayerPrefs.SetInt("Sword4Unlocked", 1);
            }
        }
        else
        {
            Debug.Log("Not enough points");
        }
    }

    public void BackButton()
    {
        AudioManager.instance.Play("SelectUI");
        waveWonItems.SetActive(true);
        armouryMenu.SetActive(false);
    }
}
